import axiosClient from './axiosClient';

export const getServices = async () => {
    const res = await axiosClient.get('/api/admin/services');
    return res.data;
};

export const createService = async (data) => {
    const res = await axiosClient.post('/api/admin/services', data);
    return res.data;
};

export const updateService = async (id, data) => {
    const res = await axiosClient.put(`/api/admin/services/${id}`, data);
    return res.data;
};

export const deleteService = async (id) => {
    const res = await axiosClient.delete(`/api/admin/services/${id}`);
    return res.data;
};
