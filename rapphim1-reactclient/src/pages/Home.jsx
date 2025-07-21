// src/pages/Home.jsx
import React, { useEffect, useState } from 'react';
import Navbar from '../components/Navbar';
import AuthDialog from '../components/AuthDialog';
import { getProfile } from '../api/auth';

const Home = () => {
    const [openDialog, setOpenDialog] = useState(false);
    const [profile, setProfile] = useState(null);

    const fetchProfile = async () => {
        try {
            const res = await getProfile();
            setProfile(res);
        } catch {
            setProfile(null);
        }
    };

    useEffect(() => {
        const fetchProfile = async () => {
            const token = localStorage.getItem('token');
            if (!token) return;  //  Không gọi nếu chưa login

            try {
                const res = await getProfile();
                setProfile(res);
            } catch {
                setProfile(null);
            }
        };

        fetchProfile();
    }, []);

    const handleLogout = () => {
        localStorage.removeItem('token');
        setProfile(null);
    };

    return (
        <>
            <Navbar
                onLoginClick={() => setOpenDialog(true)}
                isAdmin={profile?.role === 'Admin'}
                onLogout={handleLogout}
                profile={profile}
            />
            <AuthDialog open={openDialog} onClose={() => setOpenDialog(false)} onSuccess={fetchProfile} />
            <h1 style={{ textAlign: 'center', marginTop: 30 }}>🎬 Phim hôm nay</h1>
            {/* Section phim hôm nay... */}
            <h2 style={{ textAlign: 'center', marginTop: 30 }}>🎞️ Phim sắp chiếu</h2>
            {/* Section phim sắp chiếu... */}
        </>
    );
};

export default Home;
