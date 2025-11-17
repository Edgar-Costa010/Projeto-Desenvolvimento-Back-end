// src/pages/Cadastro.jsx
import React, { useState } from 'react';
import api from '../api/api';
import { useNavigate } from 'react-router-dom';

export default function Cadastro() {
  const [form, setForm] = useState({
    nome: '',
    cpf: '',
    email: '',
    telefone: '',
    dataNascimento: '',
    endereco: '',
    senha: ''
  });
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    try {
      await api.post('Auth/register', form);
      alert('Conta criada com sucesso!');
      setTimeout(() => navigate('/login'), 300);
    } catch (err) {
      console.error(err);
      alert(err?.response?.data?.mensagem || 'Erro ao criar conta');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div
      style={{
        display: 'flex',
        justifyContent: 'center',   // centraliza horizontal
        alignItems: 'center',       // centraliza vertical
        height: '100vh',            // ocupa a tela toda
        backgroundColor: '#f5f7fa'  // tom suave de fundo
      }}
    >
      <div
        className="card"
        style={{
          width: '100%',
          maxWidth: 500,
          padding: 30,
          boxShadow: '0 2px 10px rgba(0,0,0,0.1)',
          borderRadius: 12,
          backgroundColor: 'white'
        }}
      >
        <h2 style={{ textAlign: 'center', marginBottom: 20 }}>Criar nova conta</h2>
        <form onSubmit={handleSubmit} className="form">
          <input
            placeholder="Nome"
            value={form.nome}
            onChange={e => setForm({ ...form, nome: e.target.value })}
            required
          />
          <input
            placeholder="CPF"
            value={form.cpf}
            onChange={e => setForm({ ...form, cpf: e.target.value })}
            required
          />
          <input
            placeholder="E-mail"
            type="email"
            value={form.email}
            onChange={e => setForm({ ...form, email: e.target.value })}
            required
          />
          <input
            placeholder="Telefone"
            value={form.telefone}
            onChange={e => setForm({ ...form, telefone: e.target.value })}
          />
          <label style={{ marginTop: 10 }}>Data de Nascimento</label>
          <input
            type="date"
            value={form.dataNascimento}
            onChange={e => setForm({ ...form, dataNascimento: e.target.value })}
            required
          />
          <input
            placeholder="Endereço"
            value={form.endereco}
            onChange={e => setForm({ ...form, endereco: e.target.value })}
            required
          />
          <input
            placeholder="Senha"
            type="password"
            value={form.senha}
            onChange={e => setForm({ ...form, senha: e.target.value })}
            required
          />
          <button
            className="btn btn-primary"
            type="submit"
            disabled={loading}
            style={{ width: '100%', marginTop: 12 }}
          >
            {loading ? 'Enviando...' : 'Criar conta'}
          </button>
        </form>
      </div>
    </div>
  );
}
