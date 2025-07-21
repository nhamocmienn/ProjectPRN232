import React, { useEffect, useState } from 'react';
import {
    Typography, Box, Tabs, Tab, Button, Container, Divider, TextField, Pagination
} from '@mui/material';
import actorApi from '../../api/actorApi';
import directorApi from '../../api/directorApi';
import ActorDirectorForm from '../../components/admin/ActorDirectorForm';
import ActorDirectorTable from '../../components/admin/ActorDirectorTable';

import Navbar from '../../components/Navbar';
import AuthDialog from '../../components/AuthDialog';
import { getProfile } from '../../api/auth';



const handleLogout = () => {
    localStorage.removeItem('token');
    setProfile(null);
};


const ActorDirectorPage = () => {
    const [tab, setTab] = useState(0); // 0 = Actor, 1 = Director
    const [actors, setActors] = useState([]);
    const [directors, setDirectors] = useState([]);
    const [editing, setEditing] = useState(null);
    const [search, setSearch] = useState('');
    const [page, setPage] = useState(1);
    const [openDialog, setOpenDialog] = useState(false);
    const [profile, setProfile] = useState(null);
    const pageSize = 5;

    const loadActors = async () => setActors((await actorApi.getAll()).data);
    const loadDirectors = async () => setDirectors((await directorApi.getAll()).data);

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
        loadActors();
        loadDirectors();
        const token = localStorage.getItem('token');
        if (token) {
            fetchProfile();
        }
    }, []);

;

    const handleEdit = (type, data) => setEditing({ type, data });

    const handleDelete = async (type, id) => {
        if (type === 'actor') await actorApi.delete(id);
        else await directorApi.delete(id);
        await (type === 'actor' ? loadActors() : loadDirectors());
    };

    const handleSave = async (type, data) => {
        if (data.id) {
            if (type === 'actor') await actorApi.update(data.id, data);
            else await directorApi.update(data.id, data);
        } else {
            if (type === 'actor') await actorApi.create(data);
            else await directorApi.create(data);
        }
        setEditing(null);
        await (type === 'actor' ? loadActors() : loadDirectors());
    };

    const list = tab === 0 ? actors : directors;
    const filtered = list.filter(i => i.name.toLowerCase().includes(search.toLowerCase()));
    const pagedData = filtered.slice((page - 1) * pageSize, page * pageSize);
    const totalPages = Math.ceil(filtered.length / pageSize);

    return (
        <>
            <Navbar
                onLoginClick={() => setOpenDialog(true)}
                isAdmin={profile?.role === 'Admin'}
                onLogout={handleLogout}
                profile={profile}
            />
            <Container sx={{ mt: 4 }}>
                <Typography variant="h4" gutterBottom>Quản lý Diễn viên & Đạo diễn</Typography>
                <Tabs value={tab} onChange={(e, newTab) => { setTab(newTab); setSearch(''); setPage(1); }}>
                    <Tab label="Diễn viên" />
                    <Tab label="Đạo diễn" />
                </Tabs>

                <Divider sx={{ my: 2 }} />

                <Box sx={{ display: 'flex', gap: 2, alignItems: 'center', mb: 2 }}>
                    <Button
                        variant="contained"
                        onClick={() => setEditing({
                            type: tab === 0 ? 'actor' : 'director',
                            data: { name: '', biography: '', imageUrl: '' }
                        })}
                    >
                        Thêm {tab === 0 ? 'Diễn viên' : 'Đạo diễn'}
                    </Button>
                    <TextField
                        label="Tìm kiếm theo tên"
                        value={search}
                        onChange={e => { setSearch(e.target.value); setPage(1); }}
                    />
                </Box>

                <ActorDirectorTable
                    type={tab === 0 ? 'actor' : 'director'}
                    data={pagedData}
                    onEdit={handleEdit}
                    onDelete={handleDelete}
                    showImageFixedSize
                />

                {totalPages > 1 && (
                    <Box mt={2} display="flex" justifyContent="center">
                        <Pagination
                            count={totalPages}
                            page={page}
                            onChange={(e, value) => setPage(value)}
                            color="primary"
                        />
                    </Box>
                )}

                {editing && (
                    <ActorDirectorForm
                        type={editing.type}
                        data={editing.data}
                        onSave={handleSave}
                        onCancel={() => setEditing(null)}
                    />
                )}

                <AuthDialog
                    open={openDialog}
                    onClose={() => setOpenDialog(false)}
                    onSuccess={fetchProfile}
                />
            </Container>
        </>
    );
};

export default ActorDirectorPage;
