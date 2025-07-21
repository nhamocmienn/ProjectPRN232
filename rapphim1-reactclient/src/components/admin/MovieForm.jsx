import React, { useState, useEffect } from 'react';
import {
    Box,
    TextField,
    Button,
    Typography,
    Chip,
    Autocomplete,
    Select,
    MenuItem,
    FormControl,
    InputLabel,
    Checkbox,
    ListItemText,
    OutlinedInput,
    Grid,
    Stack
} from '@mui/material';
import { createMovie, updateMovie, getMovie } from '../../api/movies';
import { getGenres } from '../../api/genres';

const MovieForm = ({ movie, onClose }) => {
    const isEdit = Boolean(movie);

    const [form, setForm] = useState({
        title: '',
        releaseDate: '',
        duration: '',
        description: '',
        posterUrl: '',
        landscapeImageUrl: '',
        trailerUrl: '',
        actorNames: [],
        directorNames: [],
        genreIds: [],
        isActive: true,
    });

    const [genreOptions, setGenreOptions] = useState([]);

    useEffect(() => {
        const fetchData = async () => {
            const genresRes = await getGenres();
            setGenreOptions(genresRes.data);

            if (isEdit) {
                const movieRes = await getMovie(movie.id);
                const movieData = movieRes.data;
                const formattedDate = movieData.releaseDate?.split('T')[0];

                setForm({
                    ...movieData,
                    releaseDate: formattedDate || '',
                    genreIds: genresRes.data
                        .filter(g => movieData.genreNames?.includes(g.name))
                        .map(g => g.id)
                });
            }
        };

        fetchData();
    }, [isEdit]);


    const handleChange = e => {
        setForm(prev => ({ ...prev, [e.target.name]: e.target.value }));
    };

    const handleSubmit = async () => {
        const payload = { ...form };
        if (isEdit) await updateMovie(movie.id, payload);
        else await createMovie(payload);
        onClose();
    };

    return (
        <Box
            sx={{
                p: 3,
                pr: 5,
                width: '100%',
                maxWidth: 1000,
                overflow: 'auto',
            }}
        >
            <Typography variant="h5" gutterBottom>
                {isEdit ? 'Chỉnh sửa phim' : 'Thêm phim mới'}
            </Typography>

            <Grid container spacing={3}>
                {/* LEFT COLUMN */}
                <Grid item xs={12} md={7}>
                    <Stack spacing={2}>
                        <TextField label="Tên phim" name="title" value={form.title} onChange={handleChange} fullWidth />
                        <TextField label="Thời lượng (phút)" name="duration" type="number" value={form.duration} onChange={handleChange} fullWidth />
                        <TextField label="Ngày chiếu" name="releaseDate" type="date" value={form.releaseDate} onChange={handleChange} InputLabelProps={{ shrink: true }} fullWidth />
                        <TextField label="Mô tả" name="description" value={form.description} onChange={handleChange} multiline rows={4} fullWidth />

                        <Autocomplete
                            multiple
                            freeSolo
                            value={form.actorNames}
                            onChange={(e, newVal) => setForm(prev => ({ ...prev, actorNames: newVal }))}
                            options={[]}
                            renderTags={(value, getTagProps) =>
                                value.map((option, index) => (
                                    <Chip key={index} variant="outlined" label={option} {...getTagProps({ index })} />
                                ))
                            }
                            renderInput={(params) => (
                                <TextField {...params} label="Diễn viên" fullWidth />
                            )}
                        />

                        <Autocomplete
                            multiple
                            freeSolo
                            value={form.directorNames}
                            onChange={(e, newVal) => setForm(prev => ({ ...prev, directorNames: newVal }))}
                            options={[]}
                            renderTags={(value, getTagProps) =>
                                value.map((option, index) => (
                                    <Chip key={index} variant="outlined" label={option} {...getTagProps({ index })} />
                                ))
                            }
                            renderInput={(params) => (
                                <TextField {...params} label="Đạo diễn" fullWidth />
                            )}
                        />

                        <FormControl fullWidth>
                            <InputLabel>Thể loại</InputLabel>
                            <Select
                                multiple
                                name="genreIds"
                                value={form.genreIds}
                                onChange={(e) => setForm(prev => ({ ...prev, genreIds: e.target.value }))}
                                input={<OutlinedInput label="Thể loại" />}
                                renderValue={(selected) =>
                                    genreOptions
                                        .filter(g => selected.includes(g.id))
                                        .map(g => g.name)
                                        .join(', ')
                                }
                            >
                                {genreOptions.map((genre) => (
                                    <MenuItem key={genre.id} value={genre.id}>
                                        <Checkbox checked={form.genreIds.includes(genre.id)} />
                                        <ListItemText primary={genre.name} />
                                    </MenuItem>
                                ))}
                            </Select>
                        </FormControl>
                    </Stack>
                </Grid>

                {/* RIGHT COLUMN - MEDIA */}
                <Grid item xs={12} md={5}>
                    <Stack spacing={2}>
                        <TextField label="Poster URL" name="posterUrl" value={form.posterUrl} onChange={handleChange} fullWidth />
                        {form.posterUrl && (
                            <Box
                                component="img"
                                src={form.posterUrl}
                                alt="Poster Preview"
                                sx={{ width: '100%', height: 200, objectFit: 'cover', borderRadius: 1 }}
                            />
                        )}

                        <TextField label="Ảnh ngang URL" name="landscapeImageUrl" value={form.landscapeImageUrl} onChange={handleChange} fullWidth />
                        {form.landscapeImageUrl && (
                            <Box
                                component="img"
                                src={form.landscapeImageUrl}
                                alt="Landscape Preview"
                                sx={{ width: '100%', height: 150, objectFit: 'cover', borderRadius: 1 }}
                            />
                        )}

                        <TextField label="Trailer URL" name="trailerUrl" value={form.trailerUrl} onChange={handleChange} fullWidth />
                        {form.trailerUrl && (
                            <>
                                {form.trailerUrl.includes('youtube.com') || form.trailerUrl.includes('youtu.be') ? (
                                    <Box sx={{ position: 'relative', paddingTop: '56.25%' }}>
                                        <iframe
                                            src={convertYouTubeUrl(form.trailerUrl)}
                                            title="YouTube Trailer"
                                            allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"
                                            allowFullScreen
                                            style={{
                                                position: 'absolute',
                                                top: 0,
                                                left: 0,
                                                width: '100%',
                                                height: '100%',
                                                border: 0,
                                                borderRadius: 8,
                                            }}
                                        />
                                    </Box>
                                ) : (
                                    <video
                                        controls
                                        style={{ width: '100%', height: 200, objectFit: 'cover', borderRadius: 8 }}
                                    >
                                        <source src={form.trailerUrl} type="video/mp4" />
                                        Trình duyệt không hỗ trợ video.
                                    </video>
                                )}
                            </>
                        )}
                    </Stack>
                </Grid>

                {/* BUTTONS */}
                <Grid item xs={12} textAlign="right">
                    <Button onClick={onClose} variant="outlined" sx={{ mr: 2 }}>Hủy</Button>
                    <Button onClick={handleSubmit} variant="contained">{isEdit ? 'Cập nhật' : 'Thêm'}</Button>
                </Grid>
            </Grid>
        </Box>
    );
};

// Helper to convert YouTube link to embed format
const convertYouTubeUrl = (url) => {
    try {
        let videoId = '';
        if (url.includes('youtu.be')) {
            videoId = url.split('youtu.be/')[1];
        } else if (url.includes('youtube.com')) {
            const params = new URLSearchParams(new URL(url).search);
            videoId = params.get('v');
        }
        return `https://www.youtube.com/embed/${videoId}`;
    } catch {
        return '';
    }
};

export default MovieForm;
