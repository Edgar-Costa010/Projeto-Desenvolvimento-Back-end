import React, { useEffect, useState } from 'react';
import api from '../api/api';
import Layout from '../components/Layout';

export default function MedicoDashboard() {
  const [consultas, setConsultas] = useState([]);
  const [loading, setLoading] = useState(false);

  const medicoId = localStorage.getItem('id');

  const carregarConsultas = async () => {
    if (!medicoId) {
      alert('Sessão expirada. Faça login novamente.');
      return;
    }

    setLoading(true);
    try {
      const res = await api.get(`/Consultas/profissional/${medicoId}`);
      setConsultas(res.data || []);
    } catch (err) {
      console.error('Erro ao carregar consultas do médico:', err);
      alert('Erro ao carregar suas consultas.');
    } finally {
      setLoading(false);
    }
  };

  const concluirConsulta = async (id) => {
    if (!window.confirm('Deseja marcar esta consulta como concluída?')) return;

    try {
      await api.put(`/Consultas/${id}/concluir`);
      alert('Consulta concluída com sucesso!');
      await carregarConsultas();
    } catch (err) {
      console.error('Erro ao concluir consulta:', err);
      alert('Erro ao concluir consulta.');
    }
  };

  const formatarData = (data) => {
    const d = new Date(data);
    return isNaN(d.getTime()) ? data : d.toLocaleString();
  };

  useEffect(() => {
    carregarConsultas();
  }, [medicoId]);

  return (
    <Layout>
      <h2>Painel do Médico</h2>

      <div className="card">
        <h3>Minhas Consultas</h3>
        <table className="table">
          <thead>
            <tr>
              <th>Paciente</th>
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
                <td colSpan={5}>Nenhuma consulta agendada</td>
              </tr>
            )}
            {consultas.map((c) => (
              <tr key={c.id}>
                <td>{c.paciente?.nome || c.Paciente?.Nome || '—'}</td>
                <td>{formatarData(c.data || c.Data)}</td>
                <td>{c.concluida || c.Concluida ? 'Concluída' : 'Agendada'}</td>
                <td>{c.observacoes || c.Observacoes || ''}</td>
                <td>
                  {!c.concluida && !c.Concluida && (
                    <button
                      className="btn btn-success"
                      onClick={() => concluirConsulta(c.id)}
                    >
                      Concluir
                    </button>
                  )}
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </Layout>
  );
}
