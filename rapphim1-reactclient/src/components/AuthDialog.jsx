// src/components/AuthDialog.jsx
import React, { useState } from 'react';
import {
    Dialog, DialogTitle, DialogContent, DialogActions,
    TextField, Button, Tabs, Tab
} from '@mui/material';
import { login, register } from '../api/auth';

const AuthDialog = ({ open, onClose, onSuccess }) => {
    const [tab, setTab] = useState(0); // 0 = Login, 1 = Register
    const [form, setForm] = useState({ email: '', password: '', name: '' });

    const handleChange = (e) => {
        setForm(prev => ({ ...prev, [e.target.name]: e.target.value }));
    };

    const handleSubmit = async () => {
        try {
            if (tab === 0) {
                const token = await login(form.email, form.password);
                localStorage.setItem('token', token);
                onSuccess();
                onClose();
            } else {
                await register(form);
                alert('Đăng ký thành công, hãy đăng nhập!');
                setTab(0);
            }
        } catch (err) {
            alert('Có lỗi xảy ra: ' + err.response?.data?.message || 'Lỗi không xác định');
        }
    };

    return (
        <Dialog open={open} onClose={onClose}>
            <DialogTitle>{tab === 0 ? 'Đăng nhập' : 'Đăng ký'}</DialogTitle>
            <DialogContent>
                <Tabs value={tab} onChange={(_, newVal) => setTab(newVal)}>
                    <Tab label="Đăng nhập" />
                    <Tab label="Đăng ký" />
                </Tabs>
                {tab === 1 && (
                    <TextField
                        margin="dense"
                        label="Tên"
                        name="name"
                        fullWidth
                        value={form.name}
                        onChange={handleChange}
                    />
                )}
                <TextField
                    margin="dense"
                    label="Email"
                    name="email"
                    fullWidth
                    value={form.email}
                    onChange={handleChange}
                />
                <TextField
                    margin="dense"
                    label="Mật khẩu"
                    name="password"
                    type="password"
                    fullWidth
                    value={form.password}
                    onChange={handleChange}
                />
            </DialogContent>
            <DialogActions>
                <Button onClick={onClose}>Hủy</Button>
                <Button onClick={handleSubmit}>{tab === 0 ? 'Đăng nhập' : 'Đăng ký'}</Button>
            </DialogActions>
        </Dialog>
    );
};

export default AuthDialog;
