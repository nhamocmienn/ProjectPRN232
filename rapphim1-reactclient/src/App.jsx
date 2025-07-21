// src/App.jsx
import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import MovieManager from './pages/admin/MovieManager';
import RoomManager from './pages/admin/RoomManager';

import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import ServiceManager from './pages/admin/ServiceManager';
import ActorDirectorPage from './pages/Admin/ActorDirectorPage';




function App() {
    return (
        <Router>
            <Routes>
              
                    <Route index element={<Home />} />
                    <Route path="admin/movies" element={<MovieManager />} />
                    <Route path="admin/rooms" element={<RoomManager />} />
                <Route path="admin/services" element={<ServiceManager />} />
                <Route path="/admin/actors-directors" element={<ActorDirectorPage />} />
    
               

                
            </Routes>
            <ToastContainer />
        </Router>
    );
}


export default App;
