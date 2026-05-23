import { useContext, useEffect, useRef, useState } from "react";
import Base64 from "../../../shared/base64/Base64";
import AppContext from "../../features/context/AppContext";


export default function AuthModal() {
    const {alarmData, setToken, request} = useContext(AppContext);
    const closeModalRef = useRef();
    const modalRef = useRef();
    const [formState, setFormState] = useState({
        "login": "",
        "password": ""
    });
    const [isFormValid, setFormValid] = useState(false);
    const [error, setError] = useState(false);


    const authenticate = () => {
        console.log(formState.login, formState.password);
        // RFC 7617 
        const credentials = Base64.encode(`${formState.login}:${formState.password}`);
        request("/user/login", {
            method: "GET",
            headers: {
                'Authorization': 'Basic ' + credentials
            }
        }).then(data => {
            setToken(data);
            setError(false);
            closeModalRef.current.click();
        })
        .catch(_ => setError("Вхід скасовано"));
    };

    const onModalClose = () => {
        // console.log("Modal hide");
        setFormState({
            "login": "",
            "password": ""
        });
        setError(false);
    };

    useEffect(() => {
        // console.log("useEffect", formState.login, formState.password);
        setFormValid(formState.login.length > 2 && formState.password.length > 2);
    }, [formState]);

    useEffect(() => {
        modalRef.current.addEventListener('hide.bs.modal', onModalClose);
        return () => {
           if(modalRef.current){
            modalRef.current.removeEventListener('hide.bs.modal', onModalClose);
            }
        }
    }, []);

    return <div ref={modalRef} className="modal fade" id="authModal" tabIndex="-1" aria-labelledby="authModalLabel" aria-hidden="true">
        <div className="modal-dialog">
            <div className="modal-content">
                <div className="modal-header">
                    <h1 className="modal-title fs-5" id="authModalLabel">Вхід до сайту</h1>
                    <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div className="modal-body">
                    
                        <div className="input-group mb-3">
                            <span className="input-group-text" id="user-login-addon"><i className="bi bi-key"></i></span>
                            <input 
                                onChange={e => {
                                    // formState.login = e.target.value;
                                    // setFormState(formState);  // !! посилання не змінюється - стан не оновлюється
                                    // console.log("event", formState.login);
                                    setFormState({...formState, login: e.target.value});
                                }}
                                value={formState.login}
                                name="user-login" type="text" className="form-control"
                                placeholder="Логін" aria-label="Логін" aria-describedby="user-login-addon"/>
                            <div className="invalid-feedback"></div>
                        </div>
                        <div className="input-group mb-3">
                            <span className="input-group-text" id="user-password-addon"><i className="bi bi-lock"></i></span>
                            <input 
                                onChange={e => {
                                    setFormState(state => { return {...state, password: e.target.value}; });
                                }}
                                value={formState.password}
                                name="user-password" type="password" className="form-control" placeholder="Пароль"
                                aria-label="Пароль" aria-describedby="user-password-addon"/>
                            <div className="invalid-feedback"></div>
                        </div>
                        
                </div>
                <div className="modal-footer">
                    {error && <div className="alert alert-danger flex-grow-1 py-2" role="alert">{error}</div>}
                    <button ref={closeModalRef} onClick={onModalClose} type="button" className="btn btn-secondary" data-bs-dismiss="modal">Скасувати</button>
                    <button disabled={!isFormValid} onClick={authenticate} type="button" className="btn btn-primary" >Вхід</button>
                </div>
            </div>
        </div>
    </div>;

}