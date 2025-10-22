import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Layout from './ui/Layout';
import Home from '../pages/home/Home';
import Privacy from '../pages/privacy/Privacy';
import AppContext from '../features/context/AppContext';
import { use, useContext, useState } from 'react';
import type { ModalData } from '../features/types/ModalData';
import ModalView from './ui/ModaView';

export default function App() {
  const [modalData, setModalData] = useState<ModalData | null>(null);

  const showModal = (data: ModalData | null) => {
    setModalData(data);
  };

  return <AppContext.Provider value={{showModal}}>
   <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout/>}>
          <Route index element={<Home />}/>
          <Route path='privacy' element={<Privacy />}/>

        </Route>
      </Routes>
    </BrowserRouter>;
    <ModalView data={modalData} />
  </AppContext.Provider>
}