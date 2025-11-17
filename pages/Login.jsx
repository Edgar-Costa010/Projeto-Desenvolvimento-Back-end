// src/pages/Login.jsx
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../api/api';

export default function Login() {
  const [email, setEmail] = useState('');
  const [senha, setSenha] = useState('');
  const [loading, setLoading] = useState(false);
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();
    setLoading(true);
    try {
      const res = await api.post('auth/login', { email, senha });
      const { token, usuario } = res.data;

      // Armazena tudo que o front precisa
      localStorage.setItem('token', token);
      localStorage.setItem('id', usuario.id);
      localStorage.setItem('user', JSON.stringify(usuario));
      localStorage.setItem('nome', usuario.nome);
      localStorage.setItem('perfil', usuario.perfil);
      localStorage.setItem('pacienteId', usuario.id); // 🔹 compatível com o Dashboard

      // Redireciona com base no perfil
      if (usuario.perfil === 'Administrador') navigate('/admin');
      else if (usuario.perfil === 'Profissional') navigate('/medico');
      else if (usuario.perfil === 'Paciente') navigate('/paciente');
      else navigate('/');
    } catch (err) {
      console.error(err);
      alert(err?.response?.data?.mensagem || 'Erro ao efetuar login.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div style={styles.wrapper}>
      <div style={styles.card}>
        <h2 style={{ textAlign: 'center' }}>Acessar o Sistema VidaPlus</h2>
        <form onSubmit={handleLogin} style={styles.form}>
          <input
            type="email"
            placeholder="E-mail"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
            style={styles.input}
          />
          <input
            type="password"
            placeholder="Senha"
            value={senha}
            onChange={(e) => setSenha(e.target.value)}
            required
            style={styles.input}
          />
          <button
            type="submit"
            disabled={loading}
            style={styles.buttonPrimary}
          >
            {loading ? 'Entrando...' : 'Entrar'}
          </button>
        </form>
        <button
          onClick={() => navigate('/cadastro')}
          style={styles.buttonSecondary}
        >
          Cadastrar-se
        </button>
      </div>
    </div>
  );
}

// Estilização visual da página
const styles = {
  wrapper: {
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    height: '100vh',
    background: 'linear-gradient(135deg, #007bff, #00bfa5)',
  },
  card: {
    background: '#fff',
    padding: '40px 30px',
    borderRadius: '12px',
    boxShadow: '0 0 18px rgba(0,0,0,0.15)',
    width: '100%',
    maxWidth: '380px',
  },
  form: {
    display: 'flex',
    flexDirection: 'column',
    gap: '12px',
  },
  input: {
    padding: '10px',
    borderRadius: '6px',
    border: '1px solid #ccc',
    fontSize: '16px',
  },
  buttonPrimary: {
    padding: '10px',
    backgroundColor: '#007bff',
    color: '#fff',
    border: 'none',
    borderRadius: '6px',
    cursor: 'pointer',
    fontWeight: 'bold',
  },
  buttonSecondary: {
    padding: '10px',
    backgroundColor: '#e0e0e0',
    color: '#333',
    border: 'none',
    borderRadius: '6px',
    cursor: 'pointer',
    width: '100%',
    marginTop: '10px',
  },
};
