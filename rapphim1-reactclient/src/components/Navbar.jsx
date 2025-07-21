// src/components/Navbar.jsx
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



const Navbar = ({ onLoginClick, isAdmin, onLogout, profile }) => {
    const [anchorEl, setAnchorEl] = useState(null);

    const handleMenuOpen = (event) => setAnchorEl(event.currentTarget);
    const handleMenuClose = () => setAnchorEl(null);
    const navigate = useNavigate();
    const handleLogoutClick = () => {
        handleMenuClose();
        onLogout();
    };

    return (
        <AppBar position="static">
            <Toolbar sx={{ display: 'flex', alignItems: 'center', justifyContent: 'space-between' }}>
                {/* Left: Logo */}
                <Box sx={{ flex: 1 }}>
                    <Typography variant="h6">Rạp Phim</Typography>
                </Box>

                {/* Center: Menu buttons */}
                <Box sx={{ flex: 2, display: 'flex', justifyContent: 'center', gap: 2 }}>
                    <Button color="inherit">Phim Hôm Nay</Button>
                    <Button color="inherit">Phim Sắp Chiếu</Button>
                    {isAdmin && (
                        <>
                            <Button color="inherit" onClick={() => navigate('/admin/movies')}>
                                Quản lý Phim
                            </Button>

                            <Button color="inherit" onClick={() => navigate('/admin/rooms')}>
                                Quản lý Phòng
                            </Button> 
                            <Button color="inherit">Quản lý Suất Chiếu</Button>

                            <Button color="inherit" onClick={() => navigate('/admin/services')}>
                                Quản lý Dịch Vụ
                            </Button>
                          
                        </>
                    )}
                </Box>

                {/* Right: Avatar / Đăng nhập */}
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
