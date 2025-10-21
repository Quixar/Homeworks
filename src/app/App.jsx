import { BrowserRouter, Route, Routes } from 'react-router-dom';
import './ui/App.css';
import Home from '../pages/home/Home';
import Privacy from '../pages/privacy/Privacy';
import AppContext from '../features/context/AppContext';
import { useEffect, useRef, useState } from 'react';
import Base64 from '../shared/base64/Base64';
import Intro from '../pages/intro/Intro';
import Layout from './ui/layout/Layout';
import Group from '../pages/Group/Group';
import Cart from '../pages/cart/Cart';
import Product from '../pages/product/Product';
import Alarm from './ui/Alarm';

const tokenStorageKey = "react-p26-token";

function App() {
  const [user, setUser] = useState(null);
  const [token, setToken] = useState(null);
  const [productGroups, setProductGroups] = useState([]);
  const [cart, setCart] = useState({cartItems:[]});
  const alarmRef = useRef();
  const [alarmData, setAlarmData] = useState({});

  const [toastData, setToastData] = useState({}); 
  const [isToastVisible, setToastVisible] = useState(false); 

  useEffect(() => {
    const storedToken = localStorage.getItem(tokenStorageKey);
    if(storedToken) {
      const payload = Base64.jwtDecodePayload(storedToken);
      const exp = new Date(payload.Exp.toString().length == 13 
        ? Number(payload.Exp)
        : Number(payload.Exp) * 1000
      );
      const now = new Date();
      if(exp < now) {
        localStorage.removeItem(tokenStorageKey);
      }
      else {
        console.log("Token left: ", (exp - now) / 1e3, "sec");
        setToken(storedToken);
      }      
    }
    request("/api/product-group")
        .then(homePageData => setProductGroups(homePageData.productGroups));    
  }, []);

  const updateCart = () => {
    if(token != null) {
      request("/api/cart").then(data => {
        if(data != null) {
          setCart(data);
        }
      });
    }
    else {
      setCart({cartItems:[]});
    }
  };

  useEffect(() => {
    if(token == null) {
      setUser(null);
      localStorage.removeItem(tokenStorageKey);
    }
    else {
      localStorage.setItem(tokenStorageKey, token);
      setUser(Base64.jwtDecodePayload(token));
    }
    updateCart();
  }, [token]);

  const request = (url, conf) => new Promise((resolve, reject) => {
    if(url.startsWith('/')) {
      url = "https://localhost:5074" + url;
      // автоматично підставляємо токен в усі запити
      // якщо він є і у запиті немає заголовка авторизації
      if(token) {
        if(typeof conf == 'undefined') {
          conf = {};
        }
        if(typeof conf.headers == 'undefined') {
          conf.headers = {};
        }
        if(typeof conf.headers['Authorization'] == 'undefined') {
          conf.headers['Authorization'] = "Bearer " + token;
        }
      }
    }
    fetch(url, conf)
      .then(r => r.json())
      .then(j => {
        if(j.status.isOk) {
          resolve(j.data);
        }
        else {
          console.error(j);
          reject(j);
        }
      });
  });

  const alarm = (data) => new Promise( (resolve, reject) => {
    data.resolve = resolve;
    data.reject = reject;
    setAlarmData(data);
    alarmRef.current.click();
  });

  const toast = (data) => {
    setToastVisible(!isToastVisible);
  };

  return <AppContext.Provider value={ {alarm, cart, request, toast, updateCart, user, token, setToken, productGroups} }>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />} >
          <Route index element={<Home />} />
          <Route path="cart" element={<Cart />} />
          <Route path="group/:slug" element={<Group />} />
          <Route path="intro" element={<Intro />} />
          <Route path="privacy" element={<Privacy />} />
          <Route path="product/:slug" element={<Product />} />
        </Route>      
      </Routes>
    </BrowserRouter>
    <i 
      style={{display: 'block', width:0, height: 0, position: 'absolute'}}
      ref={alarmRef} 
      data-bs-toggle="modal" 
      data-bs-target="#alarmModal"></i>
    <Alarm alarmData={alarmData} />
    <Toast style={{
      position: 'absolute',
      bottom: '20px',
      left: '45vw',
      borderRadius: '10px',
      width: '10vw',
      backgroundColor: '#888888',
      display: isToastVisible ? 'block' : 'none',
    }}/>
  </AppContext.Provider>;
}

function Toast({style}) {
  return <div style={style}>Toast</div>
}

export default App;
/*
Д.З. Реалізувати роботу кнопки "Видалити весь кошик". 
Перед видаленням сформувати попередження 
"Ви підтверджуєте видалення кошику з 4 товарами на 1234 грн?"
(зауваження - сума кошику не є сумою товарів, а окремим полем)
*/