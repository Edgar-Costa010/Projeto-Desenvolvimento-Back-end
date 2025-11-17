// src/components/ModalAgendarConsulta.jsx
import React, { useState } from 'react';

export default function ModalAgendarConsulta({ profissionais, onClose, onSubmit }) {
  const [profissionalId, setProfissionalId] = useState('');
  const [dataHora, setDataHora] = useState('');
  const [observacoes, setObservacoes] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();

    if (!profissionalId) {
      alert('Selecione um profissional.');
      return;
    }

    onSubmit({
      ProfissionalId: profissionalId,
      dataHoraISO: dataHora,
      observacoes: observacoes || '',
    });
  };

  return (
    <div className="modal-overlay">
      <div className="modal">
        <h3>Agendar Consulta</h3>
        <form onSubmit={handleSubmit}>
          <label>Profissional:</label>
          <select
            value={profissionalId}
            onChange={(e) => setProfissionalId(e.target.value)}
            required
          >
            <option value="">Selecione o profissional</option>
            {profissionais.map((p) => (
              <option key={p.id} value={p.id}>
                {p.nome} — {p.especialidade}
              </option>
            ))}
          </select>

          <label>Data e Hora:</label>
          <input
            type="datetime-local"
            value={dataHora}
            onChange={(e) => setDataHora(e.target.value)}
            required
          />

          <label>Observações:</label>
          <textarea
            value={observacoes}
            onChange={(e) => setObservacoes(e.target.value)}
            placeholder="(opcional)"
          />

          <div className="modal-actions">
            <button type="submit" className="btn btn-primary">Agendar</button>
            <button type="button" className="btn" onClick={onClose}>Cancelar</button>
          </div>
        </form>
      </div>
    </div>
  );
}
