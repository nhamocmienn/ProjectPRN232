import { useNavigate } from 'react-router-dom';
import React, { useState } from 'react';
import {
    AppBar,
    Toolbar,
    Typography,
    Button,
    Menu,
    MenuItem,
    IconButton,
    Avatar,
    Box,
} from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';

const Navbar = ({ onLoginClick, isAdmin, onLogout, profile }) => {
    const [anchorEl, setAnchorEl] = useState(null);
    const [adminMenuAnchor, setAdminMenuAnchor] = useState(null);

    const handleMenuOpen = (event) => setAnchorEl(event.currentTarget);
    const handleMenuClose = () => setAnchorEl(null);

    const handleAdminMenuOpen = (event) => setAdminMenuAnchor(event.currentTarget);
    const handleAdminMenuClose = () => setAdminMenuAnchor(null);

    const navigate = useNavigate();

    const handleLogoutClick = () => {
        handleMenuClose();
        onLogout();
    };

    return (
        <AppBar position="static">
            <Toolbar sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between' }}>
                {/* Logo */}
                <Box sx={{ flex: 1 }}>
                    <Typography variant="h6" sx={{ cursor: 'pointer' }} onClick={() => navigate('/')}>
                        Rạp Phim
                    </Typography>
                </Box>

                {/* Center: Menu */}
                <Box sx={{ flex: 2, display: 'flex', justifyContent: 'center', gap: 2 }}>
                    <Button color="inherit">Phim Hôm Nay</Button>
                    <Button color="inherit">Phim Sắp Chiếu</Button>

                    {isAdmin && (
                        <>
                            <Button
                                color="inherit"
                                startIcon={<MenuIcon />}
                                onClick={handleAdminMenuOpen}
                            >
                                Quản lý
                            </Button>
                            <Menu
                                anchorEl={adminMenuAnchor}
                                open={Boolean(adminMenuAnchor)}
                                onClose={handleAdminMenuClose}
                            >
                                <MenuItem onClick={() => { navigate('/admin/movies'); handleAdminMenuClose(); }}>
                                    Quản lý Phim
                                </MenuItem>
                                <MenuItem onClick={() => { navigate('/admin/rooms'); handleAdminMenuClose(); }}>
                                    Quản lý Phòng
                                </MenuItem>
                                <MenuItem onClick={() => { navigate('/admin/showtimes'); handleAdminMenuClose(); }}>
                                    Quản lý Suất Chiếu
                                </MenuItem>
                                <MenuItem onClick={() => { navigate('/admin/services'); handleAdminMenuClose(); }}>
                                    Quản lý Dịch Vụ
                                </MenuItem>
                                <MenuItem onClick={() => { navigate('/admin/actors-directors'); handleAdminMenuClose(); }}>
                                    Quản lý Diễn viên & Đạo diễn
                                </MenuItem>
                            </Menu>
                        </>
                    )}
                </Box>

                {/* Right: Avatar or Login */}
                <Box sx={{ flex: 1, display: 'flex', justifyContent: 'flex-end', alignItems: 'center', gap: 1 }}>
                    {!profile ? (
                        <Button color="inherit" onClick={onLoginClick}>
                            Đăng nhập
                        </Button>
                    ) : (
                        <>
                            <IconButton onClick={handleMenuOpen} color="inherit">
                                <Avatar>{(profile?.name?.charAt(0) || 'U').toUpperCase()}</Avatar>
                            </IconButton>
                            <Typography variant="body1">{profile.name || 'Người dùng'}</Typography>
                            <Menu
                                anchorEl={anchorEl}
                                open={Boolean(anchorEl)}
                                onClose={handleMenuClose}
                            >
                                <MenuItem onClick={handleMenuClose}>Hồ sơ cá nhân</MenuItem>
                                <MenuItem onClick={handleLogoutClick}>Đăng xuất</MenuItem>
                            </Menu>
                        </>
                    )}
                </Box>
            </Toolbar>
        </AppBar>
    );
};

export default Navbar;
