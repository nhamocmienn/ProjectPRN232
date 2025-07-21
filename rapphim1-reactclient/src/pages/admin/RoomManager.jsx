//src/pages/admin/RoomManager.jsx
import React, { useEffect, useState } from 'react';
import {
    getRooms,
    deleteRoom
} from '../../api/rooms';
import {
    Container,
    Typography,
    Grid,
    Button,
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
    TextField,
    Box
} from '@mui/material';
import RoomCard from '../../components/admin/RoomCard';
import RoomForm from '../../components/admin/RoomForm';
import Navbar from '../../components/Navbar';
import AuthDialog from '../../components/AuthDialog';
import { getProfile } from '../../api/auth';
import SeatMapDialog from '../../components/admin/SeatMapDialog';

const RoomManager = () => {
    const [rooms, setRooms] = useState([]);
    const [filteredRooms, setFilteredRooms] = useState([]);
    const [searchTerm, setSearchTerm] = useState('');
    const [openForm, setOpenForm] = useState(false);
    const [selectedRoom, setSelectedRoom] = useState(null);
    const [openDialog, setOpenDialog] = useState(false);
    const [profile, setProfile] = useState(null);
    const [openConfirmDialog, setOpenConfirmDialog] = useState(false);
    const [roomToDelete, setRoomToDelete] = useState(null);

    const fetchRooms = async () => {
        const res = await getRooms();
        const activeRooms = res.data.filter(room => room.isActive);
        setRooms(activeRooms);
        setFilteredRooms(activeRooms);
    };

    const fetchProfile = async () => {
        try {
            const res = await getProfile();
            setProfile(res);
        } catch {
            setProfile(null);
        }
    };

    useEffect(() => {
        fetchRooms();
        const token = localStorage.getItem('token');
        if (token) {
            fetchProfile();
        }
    }, []);

    useEffect(() => {
        const filtered = rooms.filter(room =>
            room.name.toLowerCase().includes(searchTerm.toLowerCase())
        );
        setFilteredRooms(filtered);
    }, [searchTerm, rooms]);

    const handleDelete = async (id) => {
        setRoomToDelete(id);
        setOpenConfirmDialog(true);
    };

    const confirmDelete = async () => {
        if (roomToDelete) {
            await deleteRoom(roomToDelete);
            fetchRooms();
        }
        setOpenConfirmDialog(false);
        setRoomToDelete(null);
    };

    const handleEdit = (room) => {
        setSelectedRoom(room);
        setOpenForm(true);
    };

    const handleLogout = () => {
        localStorage.removeItem('token');
        setProfile(null);
    };

    const handleSearchChange = (event) => {
        setSearchTerm(event.target.value);
    };

    const [openSeatMap, setOpenSeatMap] = useState(false);
    const [roomToShowSeats, setRoomToShowSeats] = useState(null);

    return (
        <>
            <Navbar
                onLoginClick={() => setOpenDialog(true)}
                isAdmin={profile?.role === 'Admin'}
                onLogout={handleLogout}
                profile={profile}
            />
            <Container sx={{ mt: 4 }}>
                <Typography variant="h4" gutterBottom>Quản lý phòng chiếu</Typography>
                <Box sx={{ display: 'flex', gap: 2, mb: 2 }}>
                    <Button variant="contained" onClick={() => setOpenForm(true)}>
                        + Thêm phòng
                    </Button>
                    <TextField
                        label="Tìm kiếm phòng"
                        variant="outlined"
                        value={searchTerm}
                        onChange={handleSearchChange}
                        sx={{ width: '300px' }}
                    />
                </Box>

                <Grid container spacing={2} mt={2}>
                    {filteredRooms.map(room => (
                        <Grid item xs={12} sm={6} md={4} key={room.id}>
                            <RoomCard
                                room={room}
                                onEdit={() => handleEdit(room)}
                                onDelete={() => handleDelete(room.id)}
                                onShowSeats={() => {
                                    setRoomToShowSeats(room);
                                    setOpenSeatMap(true);
                                }}
                            />
                        </Grid>
                    ))}
                </Grid>

                <Dialog open={openForm} onClose={() => {
                    setOpenForm(false);
                    setSelectedRoom(null);
                }} maxWidth="md" fullWidth>
                    <RoomForm
                        room={selectedRoom}
                        onClose={(shouldRefresh) => {
                            setOpenForm(false);
                            if (shouldRefresh) fetchRooms();
                            setSelectedRoom(null);
                        }}
                    />
                </Dialog>

                <Dialog
                    open={openConfirmDialog}
                    onClose={() => {
                        setOpenConfirmDialog(false);
                        setRoomToDelete(null);
                    }}
                >
                    <DialogTitle>Xác nhận xóa phòng</DialogTitle>
                    <DialogContent>
                        <Typography>Bạn có chắc chắn muốn xóa phòng này?</Typography>
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={() => {
                            setOpenConfirmDialog(false);
                            setRoomToDelete(null);
                        }} color="primary">
                            Hủy
                        </Button>
                        <Button onClick={confirmDelete} color="error" variant="contained">
                            Xóa
                        </Button>
                    </DialogActions>
                </Dialog>

                <AuthDialog
                    open={openDialog}
                    onClose={() => setOpenDialog(false)}
                    onSuccess={fetchProfile}
                />

                <SeatMapDialog
                    open={openSeatMap}
                    room={roomToShowSeats}
                    onClose={() => {
                        setOpenSeatMap(false);
                        setRoomToShowSeats(null);
                    }}
                />
            </Container>
        </>
    );
};

export default RoomManager;