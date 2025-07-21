// src/components/admin/RoomForm.jsx
import React, { useState, useEffect } from 'react';
import {
    DialogTitle,
    DialogContent,
    TextField,
    DialogActions,
    Button,
    FormControl,
    InputLabel,
    Select,
    MenuItem,
    FormHelperText
} from '@mui/material';
import { createRoom, updateRoom } from '../../api/rooms';

const RoomForm = ({ room, onClose }) => {
    const [formData, setFormData] = useState({
        name: ''
    });
    const [errors, setErrors] = useState({});

    useEffect(() => {
        if (room) {
            setFormData({
                name: room.name || ''
            });
        }
    }, [room]);

    const handleChange = (e) => {
        const { name, value } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: value
        }));

        if (errors[name]) {
            setErrors(prev => ({
                ...prev,
                [name]: ''
            }));
        }
    };

    const validate = () => {
        const newErrors = {};
        if (!formData.name.trim()) newErrors.name = 'Tên phòng là bắt buộc';
        return newErrors;
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const validationErrors = validate();
        if (Object.keys(validationErrors).length > 0) {
            setErrors(validationErrors);
            return;
        }

        try {
            if (room) {
                const updateData = {
                    id: room.id,
                    name: formData.name.trim(),
                    isActive: room.isActive
                };
                console.log('Updating room with data:', updateData);
                await updateRoom(room.id, updateData);
                alert('Phòng đã được cập nhật thành công!');
            } else {
                const createData = {
                    name: formData.name.trim(),
                    isActive: true
                };
                console.log('Creating room with data:', createData);
                await createRoom(createData);
                alert('Phòng đã được tạo thành công!');
            }
            onClose(true);
        } catch (error) {
            console.error('Error saving room:', error);
            // Provide specific error feedback based on the response
            let errorMessage = 'Có lỗi xảy ra khi lưu phòng. Vui lòng thử lại.';
            if (error?.Id) {
                errorMessage = 'ID phòng không khớp với yêu cầu.';
            } else if (error?.Name) {
                errorMessage = error.Name.join(' ');
                setErrors({ name: errorMessage });
            } else if (typeof error === 'string') {
                errorMessage = error;
            }
            alert(errorMessage);
        }
    };

    return (
        <>
            <DialogTitle>{room ? 'Chỉnh sửa phòng' : 'Thêm phòng mới'}</DialogTitle>
            <DialogContent>
                <form onSubmit={handleSubmit}>
                    <TextField
                        fullWidth
                        label="Tên phòng"
                        name="name"
                        value={formData.name}
                        onChange={handleChange}
                        error={!!errors.name}
                        helperText={errors.name}
                        sx={{ mt: 2 }}
                    />
                </form>
            </DialogContent>
            <DialogActions>
                <Button onClick={() => onClose(false)}>Hủy</Button>
                <Button onClick={handleSubmit} variant="contained" color="primary">
                    Lưu
                </Button>
            </DialogActions>
        </>
    );
};

export default RoomForm;