import axiosClient from './axiosClient';

export const getActors = () => axiosClient.get('/api/admin/actors');
export const createActor = (data) => axiosClient.post('/api/admin/actors', data);
