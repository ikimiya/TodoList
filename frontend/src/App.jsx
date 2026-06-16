import { useState } from 'react'
import './App.css'
import Login from './components/Login'
import Tasks from './components/Tasks'

function App() {

    const [token] = useState(localStorage.getItem('token'))

    let page 
    if (token) {
        page = <Tasks />

    } else {
        page = <Login />
    }
     
  return (
      <>
        {page}
      </>
  )
}

export default App
