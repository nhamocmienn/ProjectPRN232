// src/components/admin/SeatMapDialog.jsx
import React, { useEffect, useState } from 'react';
import {
    Button,
    TextField,
    Select,
    MenuItem,
    FormControl,
    InputLabel,
    IconButton,
    Dialog,
    DialogTitle,
    DialogContent,
    Typography,
    Grid ,
    CircularProgress
} from '@mui/material';
import { Edit, Delete } from '@mui/icons-material';
import {
    getSeatsByRoom,
    getSeatTypes,
    createSeat,
    updateSeat,
    deleteSeat
} from '../../api/seats';

import { toast } from 'react-toastify';


const seatTypeColors = {
    'Standard': '#1976d2',
    'VIP': '#d32f2f',
    'Sweetbox': '#f57c00',
    // Thêm các loại khác nếu có
};

const SeatBox = ({ seat, onEdit, onDelete }) => {
    const backgroundColor = seatTypeColors[seat.seatTypeName] || '#757575';
    const isInactive = seat.isActive === false;

    return (
        <div style={{ position: 'relative', margin: 4 }}>
            {/* Khối ghế (hiển thị mờ nếu không active) */}
            <div
                style={{
                    width: 40,
                    height: 40,
                    backgroundColor,
                    color: 'white',
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'center',
                    borderRadius: 4,
                    fontSize: 14,
                    opacity: isInactive ? 0.4 : 1,
                }}
                title={`Hàng ${seat.row} - Ghế ${seat.column} (${seat.seatTypeName})`}
            >
                {seat.row}{seat.column}
            </div>

            {/* Nút chức năng (luôn hiển thị rõ ràng) */}
            <div style={{ position: 'absolute', top: -10, right: -10, display: 'flex', gap: 4 }}>
                <IconButton size="small" onClick={() => onEdit(seat)}>
                    <Edit fontSize="small" />
                </IconButton>
                {seat.isActive && (
                    <IconButton size="small" onClick={() => onDelete(seat.id)}>
                        <Delete fontSize="small" />
                    </IconButton>
                )}
            </div>
        </div>
    );
};



const SeatMapDialog = ({ open, onClose, room }) => {
    const [seats, setSeats] = useState([]);
    const [seatTypes, setSeatTypes] = useState([]);
    const [form, setForm] = useState({ row: '', column: '', seatTypeId: '', id: null });
    const [loading, setLoading] = useState(true);

    const fetchData = async () => {
        setLoading(true);
        try {
            const [seatData, seatTypeData] = await Promise.all([
                getSeatsByRoom(room.id),
                getSeatTypes()
            ]);
            setSeats(seatData);
            setSeatTypes(seatTypeData);
        } catch (err) {
            console.error(err);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        if (room && open) {
            fetchData();
        }
    }, [room, open]);

    const getMaxColumn = () =>
        Math.max(...seats.map(seat => seat.column), 0);

    const groupByRow = () => {
        const result = {};
        seats.forEach(seat => {
            if (!result[seat.row]) result[seat.row] = [];
            result[seat.row].push(seat);
        });
        return result;
    };

    const handleChange = (e) => {
        setForm(prev => ({ ...prev, [e.target.name]: e.target.value }));
    };

    const handleSubmit = async () => {
        try {
            if (form.id) {
                const result = await updateSeat(form.id, { ...form, roomId: room.id, isActive: true });
                toast.success("Cập nhật ghế thành công");
            } else {
                const result = await createSeat({ ...form, roomId: room.id });
                toast.success("Thêm ghế thành công");
            }
            setForm({ row: '', column: '', seatTypeId: '', id: null });
            fetchData();
        } catch (err) {
            const errorMessage = err?.response?.data || 'Đã xảy ra lỗi khi xử lý ghế.';
            toast.error(errorMessage); 
            console.error(err);
        }
    };


    const handleEdit = (seat) => {
        setForm({
            row: seat.row,
            column: seat.column,
            seatTypeId: seat.seatTypeId,
            id: seat.id
        });
    };

    const handleDelete = async (id) => {
        if (window.confirm('Bạn có chắc chắn muốn xóa ghế này?')) {
            try {
                await deleteSeat(id);
                toast.success("Xóa ghế thành công");
                fetchData();
            } catch (err) {
                toast.error("Xóa thất bại");
                console.error(err);
            }
        }
    };


    const renderLegend = () => (
        <div style={{ display: 'flex', gap: 16, marginBottom: 16, flexWrap: 'wrap' }}>
            {Object.entries(seatTypeColors).map(([type, color]) => (
                <div key={type} style={{ display: 'flex', alignItems: 'center', gap: 8 }}>
                    <div style={{ width: 20, height: 20, backgroundColor: color, borderRadius: 4 }}></div>
                    <Typography variant="body2">{type}</Typography>
                </div>
            ))}
        </div>
    );

    return (
        <Dialog open={open} onClose={onClose} fullWidth maxWidth="md">
            <DialogTitle>Sơ đồ ghế - {room?.name}</DialogTitle>
            <DialogContent>
                {loading ? (
                    <CircularProgress />
                ) : (
                    <>
                        {renderLegend()}

                        {/* Form thêm/sửa ghế */}
                        <div style={{ marginBottom: 24 }}>
                            <Grid container spacing={2} alignItems="center">
                                <Grid item xs={2}>
                                    <TextField label="Hàng" name="row" value={form.row} onChange={handleChange} fullWidth />
                                </Grid>
                                <Grid item xs={2}>
                                    <TextField label="Cột" name="column" type="number" value={form.column} onChange={handleChange} fullWidth />
                                </Grid>
                                <Grid item xs={4}>
                                    <FormControl fullWidth>
                                        <InputLabel>Loại ghế</InputLabel>
                                        <Select
                                            name="seatTypeId"
                                            value={form.seatTypeId}
                                            onChange={handleChange}
                                            label="Loại ghế"
                                        >
                                            {seatTypes.map(type => (
                                                <MenuItem key={type.id} value={type.id}>{type.name}</MenuItem>
                                            ))}
                                        </Select>
                                    </FormControl>
                                </Grid>
                                <Grid item xs={4}>
                                    <Button variant="contained" onClick={handleSubmit}>
                                        {form.id ? 'Cập nhật' : 'Thêm'}
                                    </Button>
                                </Grid>
                            </Grid>
                        </div>

                        {/* Hiển thị sơ đồ ghế */}
                        {Object.entries(groupByRow()).map(([row, rowSeats]) => (
                            <div key={row} style={{ display: 'flex', marginBottom: 8 }}>
                                <Typography sx={{ width: 30 }}>{row}</Typography>
                                {Array.from({ length: getMaxColumn() }).map((_, index) => {
                                    const seat = rowSeats.find(s => s.column === index + 1);
                                    return seat ? (
                                        <SeatBox key={seat.id} seat={seat} onEdit={handleEdit} onDelete={handleDelete} />
                                    ) : (
                                            <div key={`empty-${row}-${index}`} style={{ width: 40, height: 40, margin: 4 }} />
                                    );
                                })}
                            </div>
                        ))}
                    </>
                )}
            </DialogContent>
        </Dialog>
    );
};

export default SeatMapDialog;
