import axiosClient from './axiosClient';

export const getGenres = () => axiosClient.get('/api/genres'); // đổi path nếu khác
