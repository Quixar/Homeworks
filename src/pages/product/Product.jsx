import { useContext, useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import AppContext from "../../app/features/context/AppContext";
import ProductCard from "../group/ui/ProductCard";

export default function Product(){
    const {slug} = useParams();
    const {request} = useContext(AppContext);
    const [info, setInfo] = useState({
        slug: "",
        product: null,
        associations: []
    });

    useEffect(() => {
        request("/api/product/" + slug)
        .then(setInfo);
    }, []);

    return !info.product
    ? <>
    <i>Немає такого товару</i>
    </> 
    : <>
        <h1 className="text-center my-4 display-5 fw-bold">Сторінка товару</h1>

<div className="row justify-content-center align-items-start g-4">
  <div className="col-lg-5 col-md-6">
    <div className="card shadow-sm border-0 rounded-4">
      <img
        src={info.product.imageUrl}
        alt={info.product.name}
        className="card-img-top border mb-2 w-50"
      />
    </div>
  </div>

  <div className="col-lg-5 col-md-6">
    <div className="card border-0 shadow-sm rounded-4 p-4">
      <h2 className="card-title fw-bold mb-3">{info.product.name}</h2>
      <p className="card-text text-muted">{info.product.description}</p>
      <h3 className="text-success fw-bold mb-4">
        ₴ {info.product.price.toFixed(2)}
      </h3>
      <button className="btn btn-dark btn-lg w-100 rounded-3">
        <i className="bi bi-cart me-2"></i> У кошик
      </button>
    </div>
  </div>

  <div className="col-lg-2 text-center d-none d-lg-block">
    <div className="border-start ps-3 text-muted small">
      <i className="bi bi-megaphone-fill fs-3 mb-2 text-secondary"></i>
      <p>Тут може бути ваша реклама</p>
    </div>
  </div>
</div>

<h3 className="mt-5 mb-3 fw-semibold text-center">
  Вас також може зацікавити
</h3>

<div className="row row-cols-1 row-cols-sm-2 row-cols-md-3 row-cols-lg-4 g-4">
  {info.associations.map((product) => (
    <ProductCard product={product} isAssociation={true} key={product.id} />
  ))}
</div>

    </>;
}