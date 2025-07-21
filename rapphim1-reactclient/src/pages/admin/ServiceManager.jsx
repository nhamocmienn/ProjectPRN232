import React, { useEffect, useState } from 'react';
import {
    getServices,
    createService,
    updateService,
    deleteService
} from '../../api/services';
import {
    Container,
    Typography,
    Button,
    TextField,
    Dialog,
    DialogTitle,
    DialogContent,
    DialogActions,
    Grid,
    Card,
    CardContent,
    CardMedia,
    Box
} from '@mui/material';
import { toast } from 'react-toastify';

import Navbar from '../../components/Navbar';
import AuthDialog from '../../components/AuthDialog';
import { getProfile } from '../../api/auth';

const ServiceManager = () => {
    const [services, setServices] = useState([]);
    const [open, setOpen] = useState(false);
    const [editingService, setEditingService] = useState(null);
    const [formData, setFormData] = useState({ name: '', price: '', imageUrl: '' });

    const [profile, setProfile] = useState(null);
    const [openDialog, setOpenDialog] = useState(false);

    const fetchProfile = async () => {
        try {
            const res = await getProfile();
            setProfile(res);
        } catch {
            setProfile(null);
        }
    };

    const handleLogout = () => {
        localStorage.removeItem('token');
        setProfile(null);
    };

    useEffect(() => {
        fetchServices();
        const token = localStorage.getItem('token');
        if (token) {
            fetchProfile();
        }
    }, []);

    const fetchServices = async () => {
        try {
            const res = await getServices();
            setServices(res.filter(s => s.isActive));
        } catch {
            toast.error('Không thể tải danh sách dịch vụ');
        }
    };

    const handleOpenForm = (service = null) => {
        setEditingService(service);
        setFormData(service || { name: '', price: '', imageUrl: '' });
        setOpen(true);
    };

    const handleCloseForm = () => {
        setOpen(false);
        setEditingService(null);
        setFormData({ name: '', price: '', imageUrl: '' });
    };

    const handleSubmit = async () => {
        try {
            if (editingService) {
                await updateService(editingService.id, formData);
                toast.success('Cập nhật dịch vụ thành công');
            } else {
                await createService(formData);
                toast.success('Thêm dịch vụ thành công');
            }
            handleCloseForm();
            fetchServices();
        } catch (e) {
            toast.error('Có lỗi xảy ra');
        }
    };

    const handleDelete = async (id) => {
        try {
            await deleteService(id);
            toast.success('Đã xóa dịch vụ');
            fetchServices();
        } catch {
            toast.error('Xóa thất bại');
        }
    };

    return (
        <>
            <Navbar
                onLoginClick={() => setOpenDialog(true)}
                isAdmin={profile?.role === 'Admin'}
                onLogout={handleLogout}
                profile={profile}
            />
            <Container sx={{ mt: 4 }}>
                <Typography variant="h4" gutterBottom>Quản lý Dịch Vụ</Typography>
                <Button variant="contained" onClick={() => handleOpenForm()}>
                    + Thêm Dịch Vụ
                </Button>

                <Grid container spacing={2} mt={2}>
                    {services.map(service => (
                        <Grid item xs={12} sm={6} md={4} key={service.id}>
                            <Card>
                                {service.imageUrl && (
                                    <CardMedia
                                        component="img"
                                        height="140"
                                        image={service.imageUrl}
                                        alt={service.name}
                                    />
                                )}
                                <CardContent>
                                    <Typography variant="h6">{service.name}</Typography>
                                    <Typography variant="body1">Giá: {service.price}đ</Typography>
                                    <Box mt={2} display="flex" gap={1}>
                                        <Button variant="outlined" onClick={() => handleOpenForm(service)}>Sửa</Button>
                                        <Button variant="outlined" color="error" onClick={() => handleDelete(service.id)}>Xóa</Button>
                                    </Box>
                                </CardContent>
                            </Card>
                        </Grid>
                    ))}
                </Grid>

                <Dialog open={open} onClose={handleCloseForm}>
                    <DialogTitle>{editingService ? 'Cập nhật dịch vụ' : 'Thêm dịch vụ mới'}</DialogTitle>
                    <DialogContent sx={{ display: 'flex', flexDirection: 'column', gap: 2, mt: 1 }}>
                        <TextField
                            label="Tên dịch vụ"
                            value={formData.name}
                            onChange={e => setFormData({ ...formData, name: e.target.value })}
                        />
                        <TextField
                            label="Giá"
                            type="number"
                            value={formData.price}
                            onChange={e => setFormData({ ...formData, price: parseFloat(e.target.value) })}
                        />
                        <TextField
                            label="Image URL"
                            value={formData.imageUrl}
                            onChange={e => setFormData({ ...formData, imageUrl: e.target.value })}
                        />
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={handleCloseForm}>Hủy</Button>
                        <Button onClick={handleSubmit} variant="contained">Lưu</Button>
                    </DialogActions>
                </Dialog>

                <AuthDialog
                    open={openDialog}
                    onClose={() => setOpenDialog(false)}
                    onSuccess={fetchProfile}
                />
            </Container>
        </>
    );
};

export default ServiceManager;
