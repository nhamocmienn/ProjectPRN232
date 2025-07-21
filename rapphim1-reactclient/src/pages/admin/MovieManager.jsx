// src/pages/admin/MovieManager.jsx
import React, { useEffect, useState } from 'react';
import {
    getMovies,
    deleteMovie
} from '../../api/movies';
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
import MovieCard from '../../components/admin/MovieCard';
import MovieForm from '../../components/admin/MovieForm';
import Navbar from '../../components/Navbar';
import AuthDialog from '../../components/AuthDialog';
import { getProfile } from '../../api/auth';

import { toast } from 'react-toastify';


const MovieManager = () => {
    const [movies, setMovies] = useState([]);
    const [filteredMovies, setFilteredMovies] = useState([]);
    const [searchTerm, setSearchTerm] = useState('');
    const [openForm, setOpenForm] = useState(false);
    const [selectedMovie, setSelectedMovie] = useState(null);
    const [openDialog, setOpenDialog] = useState(false);
    const [profile, setProfile] = useState(null);
    const [openConfirmDialog, setOpenConfirmDialog] = useState(false);
    const [movieToDelete, setMovieToDelete] = useState(null);

    const fetchMovies = async () => {
        const res = await getMovies();
        const activeMovies = res.data.filter(movie => movie.isActive);
        setMovies(activeMovies);
        setFilteredMovies(activeMovies);
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
        fetchMovies();
        const token = localStorage.getItem('token');
        if (token) {
            fetchProfile();
        }
    }, []);

    useEffect(() => {
        const filtered = movies.filter(movie =>
            movie.title.toLowerCase().includes(searchTerm.toLowerCase())
        );
        setFilteredMovies(filtered);
    }, [searchTerm, movies]);

    const handleDelete = async (id) => {
        setMovieToDelete(id);
        setOpenConfirmDialog(true);
    };

    const confirmDelete = async () => {
        if (movieToDelete) {
            try {
                await deleteMovie(movieToDelete);
                toast.success('Đã xóa phim thành công!');
                fetchMovies();
            } catch (error) {
                console.error(error);
                toast.error('Xóa phim thất bại. Vui lòng thử lại!');
            }
        }
        setOpenConfirmDialog(false);
        setMovieToDelete(null);
    };


    const handleEdit = (movie) => {
        setSelectedMovie(movie);
        setOpenForm(true);
    };

    const handleLogout = () => {
        localStorage.removeItem('token');
        setProfile(null);
    };

    const handleSearchChange = (event) => {
        setSearchTerm(event.target.value);
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
                <Typography variant="h4" gutterBottom>Quản lý phim</Typography>
                <Box sx={{ display: 'flex', gap: 2, mb: 2 }}>
                    <Button variant="contained" onClick={() => setOpenForm(true)}>
                        + Thêm phim
                    </Button>
                    <TextField
                        label="Tìm kiếm phim"
                        variant="outlined"
                        value={searchTerm}
                        onChange={handleSearchChange}
                        sx={{ width: '300px' }}
                    />
                </Box>

                <Grid container spacing={2} mt={2}>
                    {filteredMovies.map(movie => (
                        <Grid item xs={12} sm={6} md={4} key={movie.id}>
                            <MovieCard
                                movie={movie}
                                onEdit={() => handleEdit(movie)}
                                onDelete={() => handleDelete(movie.id)}
                            />
                        </Grid>
                    ))}
                </Grid>

                <Dialog open={openForm} onClose={() => {
                    setOpenForm(false);
                    setSelectedMovie(null);
                }} maxWidth="md" fullWidth>
                    <MovieForm
                        movie={selectedMovie}
                        onClose={() => {
                            setOpenForm(false);
                            fetchMovies();
                            setSelectedMovie(null);
                        }}
                    />
                </Dialog>

                <Dialog
                    open={openConfirmDialog}
                    onClose={() => {
                        setOpenConfirmDialog(false);
                        setMovieToDelete(null);
                    }}
                >
                    <DialogTitle>Xác nhận xóa phim</DialogTitle>
                    <DialogContent>
                        <Typography>Bạn có chắc chắn muốn xóa phim này?</Typography>
                    </DialogContent>
                    <DialogActions>
                        <Button onClick={() => {
                            setOpenConfirmDialog(false);
                            setMovieToDelete(null);
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
            </Container>
        </>
    );
};

export default MovieManager;