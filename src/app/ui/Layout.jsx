import { Link, Outlet } from "react-router-dom";
import './Layout.css';
import { useContext, useRef } from "react";
import AppContext from "../../features/context/AppContext";

export default function Layout() {
    const {user, setUser, count} = useContext(AppContext);
    const closeModalRef = useRef();

    const authenticate = () => {
        setUser({
            name: "The User",
            email: "user@i.ua"
        });
        closeModalRef.current.click();
    };

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
                            <Link className="nav-link text-dark" to="/">Home</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link text-dark" to="/privacy">Privacy</Link>
                        </li>                        
                    </ul>
                    <div>
                        <h5 className="d-inline me-3">Підсумок: {count}</h5>
                        {!!user && <>
                            <button type="button" className="btn btn-outline-secondary"
                                    onClick={() => setUser(null)}>
                                <i class="bi bi-box-arrow-right"></i>
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

    <main><Outlet /></main>

    <footer className="border-top footer text-muted">
        <div className="container">
            &copy; 2025 - React-P26 - <a >Privacy</a>
        </div>
    </footer>

    <div className="modal fade" id="authModal" tabIndex="-1" aria-labelledby="authModalLabel" aria-hidden="true">
        <div className="modal-dialog">
            <div className="modal-content">
                <div className="modal-header">
                    <h1 className="modal-title fs-5" id="authModalLabel">Вхід до сайту</h1>
                    <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div className="modal-body">
                    <form id="sign-in-form">
                        <div className="input-group mb-3">
                            <span className="input-group-text" id="user-login-addon"><i className="bi bi-key"></i></span>
                            <input name="user-login" type="text" className="form-control"
                                placeholder="Логін" aria-label="Логін" aria-describedby="user-login-addon"/>
                            <div className="invalid-feedback"></div>
                        </div>
                        <div className="input-group mb-3">
                            <span className="input-group-text" id="user-password-addon"><i className="bi bi-lock"></i></span>
                            <input name="user-password" type="password" className="form-control" placeholder="Пароль"
                                aria-label="Пароль" aria-describedby="user-password-addon"/>
                            <div className="invalid-feedback"></div>
                        </div>
                    </form>
                </div>
                <div className="modal-footer">
                    <button ref={closeModalRef} type="button" className="btn btn-secondary" data-bs-dismiss="modal">Скасувати</button>
                    <button onClick={authenticate} type="button" className="btn btn-primary" form="sign-in-form">Вхід</button>
                </div>
            </div>
        </div>
    </div>

    </>;
}
/*
Д.З. Створити сторінку "Про нас" (About)
Навести на ній коротку характеристику проєкту
Додати деякі контакти (пошта, тлф)
Вбудувати до роутера
Додати посилання до заголовку шаблона
*/