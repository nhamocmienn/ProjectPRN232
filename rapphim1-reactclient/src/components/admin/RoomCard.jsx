import React from 'react';
import {
    Card,
    CardContent,
    Typography,
    Button,
    CardActions,
   
} from '@mui/material';
import { MeetingRoom } from '@mui/icons-material';

const RoomCard = ({ room, onEdit, onDelete, onShowSeats }) => {
    return (
        <Card sx={{ height: '100%', display: 'flex', flexDirection: 'column' }}>
            <CardContent>
                <Typography variant="h5" component="div">
                    {room.name}
                </Typography>

             

                <Typography variant="body2" sx={{ mt: 1 }}>
                    Số ghế: {room.seats?.length || 0}
                </Typography>
            </CardContent>

            <CardActions sx={{ mt: 'auto', justifyContent: 'flex-end' }}>
                <Button size="small" onClick={onEdit}>Sửa</Button>
                <Button size="small" color="error" onClick={onDelete}>Xóa</Button>
                <Button size="small" onClick={onShowSeats}>Sơ đồ ghế</Button>
            </CardActions>
        </Card>
    );
};

export default RoomCard;