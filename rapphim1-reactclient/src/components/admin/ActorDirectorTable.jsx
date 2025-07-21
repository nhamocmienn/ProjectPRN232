import React from 'react';
import { Table, TableHead, TableRow, TableCell, TableBody, Button } from '@mui/material';

const ActorDirectorTable = ({ type, data, onEdit, onDelete }) => {
    return (
        <Table>
            <TableHead>
                <TableRow>
                    <TableCell>ID</TableCell>
                    <TableCell>Tên</TableCell>
                    <TableCell>Tiểu sử</TableCell>
                    <TableCell>Trạng thái</TableCell>
                    <TableCell>Hành động</TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
                {data.map((item) => (
                    <TableRow key={item.id}>
                        <TableCell>{item.id}</TableCell>
                        <TableCell>{item.name}</TableCell>
                        <TableCell>{item.biography}</TableCell>
                        <TableCell>
                            <img src={item.imageUrl} alt={item.name} style={{ width: 80, height: 100, objectFit: 'cover' }} />
                        </TableCell>

                        <TableCell>{item.isActive ? '✔️' : '❌'}</TableCell>
                        <TableCell>
                            <Button onClick={() => onEdit(type, item)}>Sửa</Button>
                            <Button onClick={() => onDelete(type, item.id)} color="error">Xoá</Button>
                        </TableCell>
                    </TableRow>
                ))}
            </TableBody>
        </Table>
    );
};

export default ActorDirectorTable;
