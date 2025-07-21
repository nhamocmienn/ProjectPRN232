import axiosClient from './axiosClient';

const directorApi = {
    getAll: () => axiosClient.get('/api/admin/directors'),
    getById: (id) => axiosClient.get(`/api/admin/directors/${id}`),
    create: (data) => axiosClient.post('/api/admin/directors', data),
    update: (id, data) => axiosClient.put(`/api/admin/directors/${id}`, data),
    delete: (id) => axiosClient.delete(`/api/admin/directors/${id}`),
};

export default directorApi;
