import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Login from './pages/Login';
import Cadastro from './pages/Cadastro';
import AdminDashboard from './pages/AdminDashboard';
import AdminRelatorios from './pages/AdminRelatorios';
import MedicoDashboard from './pages/MedicoDashboard';
import PacienteDashboard from './pages/PacienteDashboard';
import ProtectedRoute from './components/ProtectedRoute';

<Route path="/cadastro" element={<Cadastro />} />

export default function App() {
  return (
    <Routes>
      {/* Rotas públicas */}
      <Route path="/" element={<Login />} />
      <Route path="/login" element={<Login />} />
      <Route path="/cadastro" element={<Cadastro />} />
      <Route path="/medico" element={<MedicoDashboard />} />

      {/* Rotas protegidas */}
      <Route
        path="/admin"
        element={
          <ProtectedRoute role="Administrador">
            <AdminDashboard />
          </ProtectedRoute>
        }
      />

      <Route
        path="/admin/relatorios"
        element={
          <ProtectedRoute role="Administrador">
            <AdminRelatorios />
          </ProtectedRoute>
        }
      />

      <Route
        path="/profissional"
        element={
          <ProtectedRoute role="Profissional">
            <MedicoDashboard />
          </ProtectedRoute>
        }
      />

      <Route
        path="/paciente"
        element={
          <ProtectedRoute role="Paciente">
            <PacienteDashboard />
          </ProtectedRoute>
        }
      />
    </Routes>
  );
}
