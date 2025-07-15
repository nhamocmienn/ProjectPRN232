// src/components/AuthDialog.jsx
import React, { useState } from 'react';
import {
    Dialog, DialogTitle, DialogContent, Tabs, Tab, Box, TextField, Button
} from '@mui/material';
import { login, register } from '../api/auth';

const AuthDialog = ({ open, onClose, onSuccess }) => {
    const [tab, setTab] = useState(0);
    const [form, setForm] = useState({ email: '', password: '', name: '', phoneNumber: '' });

    const handleChange = (e) => setForm({ ...form, [e.target.name]: e.target.value });

    const handleLogin = async () => {
        try {
            const token = await login(form.email, form.password);
            localStorage.setItem('token', token);
            onSuccess();
            onClose();
        } catch {
            alert("Sai tài khoản hoặc mật khẩu");
        }
    };

    const handleRegister = async () => {
        try {
            await register(form);
            alert("Đăng ký thành công!");
            setTab(0);
        } catch {
            alert("Lỗi đăng ký");
        }
    };

    return (
        <Dialog open={open} onClose={onClose}>
            <DialogTitle>{tab === 0 ? 'Đăng nhập' : 'Đăng ký'}</DialogTitle>
            <DialogContent>
                <Tabs value={tab} onChange={(_, val) => setTab(val)} centered>
                    <Tab label="Đăng nhập" />
                    <Tab label="Đăng ký" />
                </Tabs>
                <Box mt={2} display="flex" flexDirection="column" gap={2}>
                    <TextField name="email" label="Email" onChange={handleChange} />
                    <TextField name="password" label="Mật khẩu" type="password" onChange={handleChange} />
                    {tab === 1 && (
                        <>
                            <TextField name="name" label="Họ tên" onChange={handleChange} />
                            <TextField name="phoneNumber" label="SĐT" onChange={handleChange} />
                        </>
                    )}
                    <Button variant="contained" onClick={tab === 0 ? handleLogin : handleRegister}>
                        {tab === 0 ? 'Đăng nhập' : 'Đăng ký'}
                    </Button>
                </Box>
            </DialogContent>
        </Dialog>
    );
};

export default AuthDialog;
