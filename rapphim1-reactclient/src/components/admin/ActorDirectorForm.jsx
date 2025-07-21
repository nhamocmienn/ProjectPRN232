import React, { useState } from 'react';
import { Dialog, DialogTitle, DialogContent, TextField, DialogActions, Button } from '@mui/material';

const ActorDirectorForm = ({ type, data, onSave, onCancel }) => {
    const [form, setForm] = useState({ ...data });

    const handleChange = (e) => {
        setForm((prev) => ({ ...prev, [e.target.name]: e.target.value }));
    };

    const handleSubmit = () => onSave(type, form);

    return (
        <Dialog open onClose={onCancel} maxWidth="sm" fullWidth>
            <DialogTitle>{form.id ? 'Cập nhật' : 'Thêm'} {type === 'actor' ? 'Diễn viên' : 'Đạo diễn'}</DialogTitle>
            <DialogContent sx={{ display: 'flex', flexDirection: 'column', gap: 2, mt: 1 }}>
                <TextField label="Tên" name="name" value={form.name} onChange={handleChange} fullWidth />
                <TextField label="Tiểu sử" name="biography" value={form.biography || ''} onChange={handleChange} fullWidth />
                <TextField label="Ảnh URL" name="imageUrl" value={form.imageUrl || ''} onChange={handleChange} fullWidth />
            </DialogContent>
            <DialogActions>
                <Button onClick={onCancel}>Huỷ</Button>
                <Button onClick={handleSubmit} variant="contained">Lưu</Button>
            </DialogActions>
        </Dialog>
    );
};

export default ActorDirectorForm;
