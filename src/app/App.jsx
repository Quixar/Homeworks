import { BrowserRouter, data, Route, Routes } from 'react-router-dom';
import './ui/App.css'
import Layout from './ui/Layout/Layout';
import Home from '../pages/home/Home';
import Privacy from '../pages/privacy/Privacy';
import About from '../pages/about/About';
import AppContext from './features/context/AppContext';
import { useEffect, useRef, useState } from 'react';
import Base64 from "../shared/base64/Base64";
import Intro from '../pages/intro/intro';
import AuthModal from './ui/Layout/AuthModal';
import Group from '../pages/group/Group';
import Cart from '../pages/cart/Cart';
import Product from '../pages/product/Product';
import Alarm from './ui/Alarm';

const tokenStorageKey = "react_p26-token";

function App() {
  const [user, setUser] = useState(null);
  const [token, setToken] = useState(null);
  const [productGroup, setProductGroup] = useState([]);
  const [cart, setCart] = useState({cartItems: []});
  const alarmRef = useRef();
  const [alarmData, setAlarmData] = useState({});
  const [toastData, setToastData] = useState({});
 

  useEffect(() => {
    const storedToken = localStorage.getItem(tokenStorageKey);
    if(storedToken){
      const payload = Base64.jwtDecodePayload(storedToken);
      const exp = new Date(payload.Exp.toString().length == 13
        ? Number(payload.Exp)
        : Number(payload.Exp) * 1000
      );
      const now = new Date();
      if(exp < now)
      {
        localStorage.removeItem(tokenStorageKey);
      }else {
        console.log("Token left : ", (exp - now) /1e3, "sec");
      setToken(storedToken);}
    }
    request("/api/product-group")
       .then(homePageData => setProductGroup(homePageData.productGroup))
  },[]);

  const updateCart = () => {
    if(token != null) {
      request("/api/cart").then(data => {
      if(data != null) {
        setCart(data);
      }})
    }
    else {
      setCart({cartItems: []});
    }
  };

  useEffect(() => {
    if(token == null)
    {
       setUser(null);
        localStorage.removeItem(tokenStorageKey);
    }else {
      localStorage.setItem(tokenStorageKey, token);
      setUser(Base64.jwtDecodePayload(token));
    }
    updateCart();
  }, [token]);

  

  const request = (url, conf) => new Promise((resolve, reject) => {
    if(url.startsWith('/')) {
      url = "https://localhost:7111" + url;

      if(token){
      if(typeof conf == "undefined"){
        conf = {};
      }
      if(typeof conf.headers == 'undefined'){
        conf.headers = {};
      }
      if(typeof conf.headers['Authorization'] == 'undefined'){
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

  const alarm = (data) => new Promise((resolve, reject) => {
    data.resolve = resolve;
    data.reject = reject;
    setAlarmData(data);
    alarmRef.current.click();
  });

  const toast = (data) => {
  setToastData(data);
  };

  return <AppContext.Provider value={ {alarm, cart, request, updateCart, user, toast, token, setToken, productGroup}}>
    <BrowserRouter>
    <Routes>
      <Route path="/" element={<Layout />} >
        <Route index element={<Home />} />
        <Route path="cart" element={<Cart />} />
        <Route path="privacy" element={<Privacy />} />
        <Route path="group/:slug" element={<Group />} />
        <Route path="intro" element={<Intro />} />
        <Route path="about" element={<About />} />
        <Route path="product/:slug" element={<Product />} />
      </Route>
    </Routes>
    </BrowserRouter>
    <i style={{"display": "block", "width":"0", "height":"0", "position":'absolute' }} ref={alarmRef} data-bs-toggle="modal" data-bs-target="#alarmModal"></i>
    <Alarm alarmData={alarmData} />
    <Toast toastData={toastData} />
</AppContext.Provider>;
}

function Toast({toastData}){
   const toastShowTime = 2000; // ms
   const fadeTime = 500;      // ms
   const [isToastVisible, setToastVisible] = useState(false);
   const [opacity, setOpacity] = useState(0);
   const [queue, setQueue] = useState([]);
   const [visibleData, setVisibleData] = useState({});

  // useEffect(() => {
  //   if(toastData.message){
  //   setToastVisible(true);
  //   setTimeout( () => setOpacity(0), toastShowTime);
  //   setTimeout( () => setToastVisible(false), toastShowTime + fadeTime);
  //   }
  // }, [toastData]);

  useEffect( () => {
    setQueue([...queue, toastData]);
  }, [toastData]);

  useEffect( () => {
    console.log(queue);
    if(!isToastVisible && queue.length > 0){
      setVisibleData(queue[0]);
      setToastVisible(true); 
      setTimeout( () => setOpacity(0), toastShowTime - fadeTime);
      setTimeout(() => {
        setToastVisible(false);
        if(queue.length > 0){
          setQueue(q => q.length == 0 ? q : q.slice(1));
          // console.log('to', queue)
        }
      }, toastShowTime);
    }
  }, [queue]);

  useEffect(() => {
    if(isToastVisible){
      setOpacity(1);
    }
  },[isToastVisible]);

  return <div style={{
      position: 'absolute',
      bottom: '20px',
      left: '45vw',
      borderRadius: '10px',
      minWidth: '10vw',
      maxWidth: '25vw',
      textAlign: 'center',
      padding: '5px 10px',
      backgroundColor: '#888888',
      display: isToastVisible ? 'block' : 'none',
      opacity: opacity,
      transitionProperty: 'opacity',
      transitionDuration: `${fadeTime}ms` 
  }}>{visibleData.message}</div>
}

export default App;
