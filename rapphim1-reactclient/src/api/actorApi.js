import axiosClient from './axiosClient';

const actorApi = {
    getAll: () => axiosClient.get('/api/admin/actors'),
    getById: (id) => axiosClient.get(`/api/admin/actors/${id}`),
    create: (data) => axiosClient.post('/api/admin/actors', data),
    update: (id, data) => axiosClient.put(`/api/admin/actors/${id}`, data),
    delete: (id) => axiosClient.delete(`/api/admin/actors/${id}`),
};

export default actorApi;
