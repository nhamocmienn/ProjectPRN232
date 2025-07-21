import axiosClient from './axiosClient';

export const getMovies = () => axiosClient.get('/api/admin/movies');
export const getMovie = (id) => axiosClient.get(`/api/admin/movies/${id}`);
export const createMovie = (data) => axiosClient.post('/api/admin/movies', data);
export const updateMovie = (id, data) => axiosClient.put(`/api/admin/movies/${id}`, data);
export const deleteMovie = (id) => axiosClient.delete(`/api/admin/movies/${id}`);
