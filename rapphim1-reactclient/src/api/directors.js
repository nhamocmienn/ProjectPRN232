import axiosClient from './axiosClient';

export const getDirectors = () => axiosClient.get('/api/admin/directors');
export const createDirector = (data) => axiosClient.post('/api/admin/directors', data);
