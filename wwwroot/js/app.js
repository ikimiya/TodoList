fetch('/api/Auth/login', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ email, password })
})
    .then(res => res.json())
    .then(data => {
        localStorage.setItem('token', data.token)
        window.location.href = '/tasks.html'
    })