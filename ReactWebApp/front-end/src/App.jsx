import { useEffect, useState } from 'react'
import { BrowserRouter, Routes, Route, Link } from "react-router-dom";

function Home() {
  const [message, setMessage] = useState('Loading...')

  useEffect(() => {
    fetch('/api/message')
      .then(res => res.json())
      .then(data => setMessage(data.text))
      .catch(() => setMessage('Error fetching message'))
  }, [])

  return (
    <div style={{ textAlign: 'center', marginTop: '50px' }}>
      <h1>React + ASP.NET Core Demo 🚀</h1>
      <p style={{ fontSize: '20px' }}>{message}</p>
    </div>
  )
}

function About() {
  return (
    <div style={{ textAlign: 'center', marginTop: '50px' }}>
      <h1>About Page 📄</h1>
      <p>This is a simple new page.</p>
    </div>
  )
}

function NotFound() {
  return (
    <div style={{ textAlign: 'center', marginTop: '50px' }}>
      <h1>404 - Page Not Found ❌</h1>
      <p>Sorry, the page you're looking for doesn't exist.</p>
      <Link to="/">Go back home</Link>
    </div>
  )
}

export default function App() {
  return (
    <BrowserRouter>
      <nav style={{ textAlign: 'center', marginTop: '20px' }}>
        <Link to="/" style={{ margin: '0 10px' }}>Home</Link>
        <Link to="/about" style={{ margin: '0 10px' }}>About</Link>
      </nav>

      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/about" element={<About />} />
        {/* Catch-all route for 404 */}
        <Route path="*" element={<NotFound />} />
      </Routes>
    </BrowserRouter>
  )
}
