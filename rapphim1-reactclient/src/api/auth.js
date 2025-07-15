// src/api/auth.js
import axiosClient from './axiosClient';

export const login = async (email, password) => {
    const res = await axiosClient.post('/api/auth/login', { email, password });
    return res.data.token;
};

export const register = async (formData) => {
    return axiosClient.post('/api/auth/register', formData);
};

export const getProfile = async () => {
    const res = await axiosClient.get('/api/manageprofile/profile');
    return res.data;
};
