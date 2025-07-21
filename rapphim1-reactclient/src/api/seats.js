//src/api/seats.js
import axiosClient from './axiosClient';

export const getSeatsByRoom = async (roomId) => {
    try {
        const response = await axiosClient.get(`/api/admin/seats?roomId=${roomId}`);
        return response.data;
    } catch (error) {
        console.error('Error fetching seats:', error);
        throw error;
    }
};

export const getSeatTypes = async () => {
    const res = await axiosClient.get(`/api/admin/seat-types`);
    return res.data;
};

export const createSeat = async (data) => {
    const res = await axiosClient.post(`/api/admin/seats`, data);
    return res.data;
};

export const updateSeat = async (id, data) => {
    const res = await axiosClient.put(`/api/admin/seats/${id}`, data);
    return res.data;
};

export const deleteSeat = async (id) => {
    const res = await axiosClient.delete(`/api/admin/seats/${id}`);
    return res.data;
};