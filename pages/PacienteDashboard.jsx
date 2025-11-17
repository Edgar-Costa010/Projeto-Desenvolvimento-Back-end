// src/pages/PacienteDashboard.jsx
import React, { useEffect, useState } from 'react';
import api from '../api/api';
import Layout from '../components/Layout';
import ModalAgendarConsulta from '../components/ModalAgendarConsulta';
import ModalEditarPaciente from '../components/ModalEditarPaciente';

export default function PacienteDashboard() {
  const [consultas, setConsultas] = useState([]);
  const [profissionais, setProfissionais] = useState([]);
  const [paciente, setPaciente] = useState(null);
  const [showAgendar, setShowAgendar] = useState(false);
  const [showEditar, setShowEditar] = useState(false);
  const [loading, setLoading] = useState(false);

  // Puxa o ID diretamente do usuário logado
  const userId = localStorage.getItem('id');

  const carregar = async () => {
    if (!userId) {
      alert('Sessão expirada. Faça login novamente.');
      return;
    }
    setLoading(true);
    try {
      const [rConsultas, rProfissionais, rPaciente] = await Promise.all([
        api.get(`Consultas/paciente/${userId}`),
        api.get('Profissionais/public'),
        api.get(`Pacientes/${userId}`),
      ]);

      setConsultas(rConsultas.data || []);
      setProfissionais(rProfissionais.data || []);
      setPaciente(rPaciente.data);
    } catch (err) {
      console.error('Erro ao carregar dados:', err);
      if (err.response?.status === 401) alert('Usuário não autenticado.');
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    carregar();
  }, [userId]);

  const onAgendar = async ({ ProfissionalId, dataHoraISO, observacoes }) => {
    if (!userId) return alert('Usuário não autenticado.');
    try {
      await api.post('Consultas', {
        data: dataHoraISO,
        observacoes: observacoes || '',
        pacienteId: userId,
        ProfissionalId,
      });
      setShowAgendar(false);
      await carregar();
      alert('Consulta agendada com sucesso.');
    } catch (err) {
      console.error(err);
      alert(err?.response?.data?.mensagem || 'Erro ao agendar consulta.');
    }
  };

  const cancelar = async (id) => {
    if (!window.confirm('Deseja cancelar esta consulta?')) return;
    try {
      await api.delete(`/Consultas/${id}`);
      await carregar();
    } catch (err) {
      console.error(err);
      alert('Erro ao cancelar consulta.');
    }
  };

  const onEditarPaciente = async (dados) => {
    try {
      await api.put(`Pacientes/${userId}`, dados);
      setShowEditar(false);
      await carregar();
      alert('Dados atualizados.');
    } catch (err) {
      console.error(err);
      alert('Erro ao atualizar dados.');
    }
  };

  const formatDate = (d) => {
    if (!d) return '';
    const date = new Date(d);
    return isNaN(date.getTime()) ? d : date.toLocaleString();
  };

  return (
    <Layout>
      <h2>Painel do Paciente</h2>

      <div style={{ display: 'flex', gap: 8, marginBottom: 12 }}>
        <button className="btn btn-primary" onClick={() => setShowAgendar(true)}>
          + Agendar Nova Consulta
        </button>
        <button className="btn" onClick={() => setShowEditar(true)}>
          Editar meus dados
        </button>
      </div>

      <div className="card">
        <h3>Minhas Consultas</h3>
        <table className="table">
          <thead>
            <tr>
              <th>Profissional</th>
              <th>Data/Hora</th>
              <th>Status</th>
              <th>Observações</th>
              <th>Ações</th>
            </tr>
          </thead>
          <tbody>
            {loading && (
              <tr>
                <td colSpan={5}>Carregando...</td>
              </tr>
            )}
            {!loading && consultas.length === 0 && (
              <tr>
                <td colSpan={5}>Nenhuma consulta</td>
              </tr>
            )}
            {consultas.map((c) => {
              const prof = c.profissional || c.Profissional;
              const profNome = prof
                ? `${prof.nome || prof.Nome} — ${prof.especialidade || prof.Especialidade || ''}`
                : '—';
              return (
                <tr key={c.id}>
                  <td>{profNome}</td>
                  <td>{formatDate(c.data || c.Data)}</td>
                  <td>{c.concluida || c.Concluida ? 'Concluída' : 'Agendada'}</td>
                  <td>{c.observacoes || c.Observacoes || ''}</td>
                  <td>
                    <button className="btn btn-danger" onClick={() => cancelar(c.id)}>
                      Cancelar
                    </button>
                  </td>
                </tr>
              );
            })}
          </tbody>
        </table>
      </div>

      {showAgendar && (
  	<ModalAgendarConsulta
    	profissionais={profissionais}   // ← importante
    	onClose={() => setShowAgendar(false)}
    	onSubmit={onAgendar}
  	/>
	)}


      {showEditar && paciente && (
        <ModalEditarPaciente
          paciente={paciente}
          onClose={() => setShowEditar(false)}
          onSubmit={onEditarPaciente}
        />
      )}
    </Layout>
  );
}
