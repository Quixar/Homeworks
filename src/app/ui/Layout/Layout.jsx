import { Link, Outlet } from "react-router-dom";
import './Layout.css';
import { useContext, useEffect, useRef, useState } from "react";
import AppContext from "../../features/context/AppContext";
import Base64 from "../../../shared/base64/Base64";
import AuthModal from "./AuthModal";

export default function Layout() {
    const {cart, toast, user, setToken } = useContext(AppContext);

    return <>
     <header>
        <nav className="navbar navbar-expand-sm navbar-toggleable-sm navbar-light bg-white border-bottom box-shadow mb-3">
            <div className="container-fluid">
                <a className="navbar-brand" asp-area="" asp-controller="Home" asp-action="Index">ASP_P26</a>
                <button className="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target=".navbar-collapse" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                    <ul className="navbar-nav flex-grow-1">
                        <li className="nav-item">
                            <Link className="nav-link text-dark" asp-area="" asp-controller="Home" to="/">Home</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link text-dark" to="/intro">Intro</Link>
                        </li>   
                        <li className="nav-item">
                            <Link className="nav-link text-dark" to="/privacy">Privacy</Link>
                        </li>   
                         <li className="nav-item">
                            <Link className="nav-link text-dark" to="/about">About</Link>
                        </li> 
                         <li>
                            <i onClick={() => toast({message: Math.random()})} className="bi bi-fork-knife"></i>
                        </li>                         
                    </ul>
                    <div>      
                        {user && <>
                        <Link to={"/cart"} className="btn btn-outline-secondary me-3">
                        <i className="bi bi-cart"></i>
                        <span>{cart.cartItems.length}</span>
                        </Link>
                        <button type="button" className="btn btn-outline-secondary me-2" onClick={() => setToken(null)}>
                            <i className="bi bi-box-arrow-right"></i>
                        </button>
                    </>}
                    {!user && <>
                        <a ><i className="bi bi-person-circle"></i></a>
                        <button type="button" className="btn btn-outline-secondary"
                                data-bs-toggle="modal" data-bs-target="#authModal">
                            <i className="bi bi-box-arrow-in-right"></i>
                        </button>
                    </>}
                        
                    </div>
                </div>
            </div>
        </nav>
    </header>
        

    <main>
        <div className="conteiner">
        <Outlet />
            </div>
        </main>

    <footer className="border-top footer text-muted">
        <div className="container">
            &copy; 2025 - React-P26 - <Link to="/privacy">Privacy</Link>
        </div>
    </footer>

    <AuthModal />

    </>;
}

