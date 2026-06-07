import { useState } from 'react';
import Tasks from './Tasks';
function Login()
{

    const [page, setPage] = useState('login');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');

    // Page swap functions
    function showRegister() {
        console.log('Switched to register page');
        setPage('register');

        console.log('current page:', page);
    }

    function showLogin() {
        setPage('login');
        console.log('Switched to login page');
        console.log('current page:', page);
    }

    // Login function
    async function login() {
        const response = await fetch('/api/AuthApi/login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ email, password })
        });

        if (response.ok) {
            const data = await response.json();
            localStorage.setItem('token', data.token);
            setPage('tasks');
        } else {
            setError('Invalid email or password');
        }
    }


    // register
    async function register() {
        const response = await fetch('/api/AuthApi/register', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ email, password })
        });

        if (response.ok) {
            setPage('login');
        } else {
            setError('Email already exists');
        }
    }

    function loginForm() {
        return (
            <div className="row justify-content-center mt-5">
                <div className="col-md-4">
                    <div className="card shadow">
                        <div className="card-body">
                            <h3 className="text-center mb-4">📝 Login</h3>

                            {error && (
                                <div className="alert alert-danger">
                                    {error}
                                </div>
                            )}

                            <div className="mb-3">
                                <label className="form-label">Email</label>
                                <input
                                    type="email"
                                    className="form-control"
                                    placeholder="Enter email"
                                    value={email}
                                    onChange={(e) => setEmail(e.target.value)}
                                />
                            </div>

                            <div className="mb-3">
                                <label className="form-label">Password</label>
                                <input
                                    type="password"
                                    className="form-control"
                                    placeholder="Enter password"
                                    value={password}
                                    onChange={(e) => setPassword(e.target.value)}
                                />
                            </div>

                            <button onClick={login} className="btn btn-primary w-100">
                                Login
                            </button>

                            <div className="text-center mt-3">
                                <a
                                    href="#"
                                    onClick={(e) => {
                                        e.preventDefault();
                                        showRegister();
                                    }}
                                >
                                    Don't have an account? Register
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }

    function registerForm() {
        return (
            <div className="row justify-content-center mt-5">
                <div className="col-md-4">
                    <div className="card shadow">
                        <div className="card-body">
                            <h3 className="text-center mb-4">📝 Register</h3>

                            {error && (
                                <div className="alert alert-danger">
                                    {error}
                                </div>
                            )}

                            <div className="mb-3">
                                <label className="form-label">Email</label>
                                <input
                                    type="email"
                                    className="form-control"
                                    placeholder="Enter email"
                                    value={email}
                                    onChange={(e) => setEmail(e.target.value)}
                                />
                            </div>

                            <div className="mb-3">
                                <label className="form-label">Password</label>
                                <input
                                    type="password"
                                    className="form-control"
                                    placeholder="Enter password"
                                    value={password}
                                    onChange={(e) => setPassword(e.target.value)}
                                />
                            </div>

                            <button onClick={register} className="btn btn-primary w-100">
                                Register
                            </button>

                            <div className="text-center mt-3">
                                <a
                                    href="#"
                                    onClick={(e) => {
                                        e.preventDefault();
                                        showLogin();
                                    }}
                                >
                                    Already have an account? Login
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }

    return (
        <>
            {page === 'login' && loginForm()}
            {page === 'register' && registerForm()}
            {page === 'tasks' && <Tasks />}
        </>
    );
}

export default Login;