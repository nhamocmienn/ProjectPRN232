// src/components/admin/MovieCard.jsx
import React from 'react';
import {
    Card,
    CardContent,
    CardMedia,
    Typography,
    CardActions,
    Button,
    Stack
} from '@mui/material';

const MovieCard = ({ movie, onEdit, onDelete }) => {
    return (
        <Card sx={{ height: '100%', display: 'flex', flexDirection: 'column', borderRadius: 3, boxShadow: 3 }}>
            <CardMedia
                component="img"
                height="200"
                image={movie.posterUrl}
                alt={movie.title}
            />
            <CardContent>
                <Typography variant="h6" gutterBottom>
                    {movie.title}
                </Typography>
                <Typography variant="body2" color="text.secondary">
                    Thời lượng: {movie.duration} phút
                </Typography>
                <Typography variant="body2" color="text.secondary">
                    Ngày chiếu: {new Date(movie.releaseDate).toLocaleDateString()}
                </Typography>
            </CardContent>
            <CardActions sx={{ justifyContent: 'space-between', mt: 'auto', px: 2, pb: 2 }}>
                <Stack direction="row" spacing={1}>
                    <Button size="small" variant="outlined" onClick={onEdit}>Sửa</Button>
                    <Button size="small" color="error" variant="outlined" onClick={onDelete}>Xóa</Button>
                </Stack>
            </CardActions>
        </Card>
    );
};

export default MovieCard;
