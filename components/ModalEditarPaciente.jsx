import React, { useState } from 'react';

export default function ModalEditarPaciente({ paciente, onClose, onSubmit }) {
  const [form, setForm] = useState({
    nome: paciente.nome,
    email: paciente.email,
    telefone: paciente.telefone,
    cpf: paciente.cpf
  });

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const send = () => onSubmit(form);

  return (
    <div className="modal">
      <div className="modal-content">
        <h3>Editar Meus Dados</h3>

        <input name="nome" value={form.nome} onChange={handleChange} placeholder="Nome" />
        <input name="email" value={form.email} onChange={handleChange} placeholder="Email" />
        <input name="telefone" value={form.telefone} onChange={handleChange} placeholder="Telefone" />
        <input name="cpf" value={form.cpf} onChange={handleChange} placeholder="CPF" disabled />

        <div className="modal-actions">
          <button className="btn" onClick={onClose}>Cancelar</button>
          <button className="btn btn-primary" onClick={send}>Salvar</button>
        </div>
      </div>
    </div>
  );
}
