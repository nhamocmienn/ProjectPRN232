// src/api/rooms.js
import axiosClient from './axiosClient';

export const getRooms = async () => {
    console.log('Current token:', localStorage.getItem('token'));
    try {
        const response = await axiosClient.get('/api/admin/rooms');
        console.log('Rooms response:', response);
        return response;
    } catch (error) {
        console.error('Error fetching rooms:', error);
        throw error;
    }
};

export const getRoom = async (id) => {
    try {
        const response = await axiosClient.get(`/api/admin/rooms/${id}`);
        return response;
    } catch (error) {
        console.error('Error fetching room:', error);
        throw error;
    }
};

export const createRoom = async (data) => {
    console.log('Creating room with token:', localStorage.getItem('token'));
    try {
        const response = await axiosClient.post('/api/admin/rooms', {
            name: data.name,
            isActive: data.isActive
        });
        return response;
    } catch (error) {
        console.error('Error creating room:', error);
        throw error;
    }
};

export const updateRoom = async (id, data) => {
    try {
        const response = await axiosClient.put(`/api/admin/rooms/${id}`, {
            id: id,
            name: data.name,
            isActive: data.isActive,
            seats: [],
            showtimes: []
        });
        console.log('Update room response:', response);
        return response;
    } catch (error) {
        console.error('Error updating room:', error);
        console.error('Error response:', error.response?.data);
        console.error('Error status:', error.response?.status);
        throw error.response?.data?.errors || error.message;
    }
};

export const deleteRoom = async (id) => {
    try {
        const response = await axiosClient.delete(`/api/admin/rooms/${id}`);
        return response;
    } catch (error) {
        console.error('Error deleting room:', error);
        throw error;
    }
};