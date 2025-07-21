// src/App.jsx
import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import MovieManager from './pages/admin/MovieManager';
import RoomManager from './pages/admin/RoomManager';

import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';


function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<Home />} />
                <Route path="/admin/movies" element={<MovieManager />} />
                <Route path="/admin/rooms" element={<RoomManager />} />
                {/* <Route path="/admin" element={<AdminPanel />} /> */}
            </Routes>
            <ToastContainer />
        </Router>
    );
}


export default App;
