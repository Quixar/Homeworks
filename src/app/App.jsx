import { BrowserRouter, Route, Routes } from 'react-router-dom';
import './ui/App.css';
import Layout from './ui/Layout';
import Home from '../pages/home/Home';
import Privacy from '../pages/privacy/Privacy';
import AppContext from '../features/context/AppContext';
import { useState } from 'react';

function App() {
  const [user, setUser] = useState(null);
  const [count, setCount] = useState(0);

  return <AppContext.Provider value={{ message: "Hello from App", user, setUser, count, setCount }}>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />} >
          <Route index element={<Home />} />
          <Route path="privacy" element={<Privacy />} />
        </Route>      
      </Routes>
    </BrowserRouter>
  </AppContext.Provider>;
}

export default App;
