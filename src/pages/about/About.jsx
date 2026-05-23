export default function About() {
    return (
        <div className="container mt-5">
            <div className="card shadow-lg border-0">
                <div className="card-body text-center">
                    <h1 className="card-title display-4 mb-4">Про нас</h1>
                    <p className="card-text fs-5">
                        Цей проект створено для роботи з <strong>ASP.NET </strong> та <strong>React</strong>
                    </p>
                    <p className="card-text fs-5">
                        Він включає в себе автентифікацію користувачів, каталог товарів та кошик
                    </p>

                    <hr className="my-4" />

                    <div className="mb-3">
                        <h5 className="fw-bold">Контакти</h5>
                        <p className="mb-1">
                            <i className="bi bi-telephone-fill me-2"></i>
                            <a className="text-decoration-none">
                                +380980988898
                            </a>
                        </p>
                        <p className="mb-1">
                            <i className="bi bi-envelope-fill me-2"></i>
                            <a className="text-decoration-none">
                                brutality_barbershop@gmail.com
                            </a>
                        </p>
                        <p className="mb-0">
                            <i className="bi bi-geo-alt-fill me-2"></i>
                            Адреса: м. Одеса, вул. Канатна, 2
                        </p>
                    </div>
                </div>
            </div>
        </div>
    );
}
