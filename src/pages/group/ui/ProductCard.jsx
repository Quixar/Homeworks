import { useContext } from "react";
import { Link, useNavigate } from "react-router-dom";
import AppContext from "../../../app/features/context/AppContext";
import "./ProductCard.css";

export default function ProductCard({ product, isAssociation }) {
  const { cart, request, updateCart } = useContext(AppContext);
  const navigate = useNavigate();
  const isInCart = cart.cartItems.some((ci) => ci.productId === product.id);

  const addToCartClick = async (e) => {
    e.preventDefault();
    try {
      await request(`/api/cart/${product.id}`, {
        method: "POST",
      });
      updateCart();
    } catch (error) {
      console.error("Помилка при додаванні:", error);
    }
  };

  const goToCartClick = (e) => {
    e.preventDefault();
    navigate("/cart");
  };

 return (
  <div className="col">
    <Link
      to={`/product/${product.slug || product.id}`}
      className="text-decoration-none h-100 d-block"
    >
      <div className={`card h-100 shadow-sm border-0 ${isAssociation ? "border-primary border-2" : ""} transition`}>
        <div className="position-relative overflow-hidden">
          <img
            src={product.imageUrl}
            className="card-img-top border mb-2 w-50"
            alt={product.name}
            style={{ 
              objectFit: 'cover',
              transition: 'transform 0.3s ease'
            }}
            onMouseOver={(e) => e.currentTarget.style.transform = 'scale(1.05)'}
            onMouseOut={(e) => e.currentTarget.style.transform = 'scale(1)'}
          />
          {isAssociation && (
            <span className="position-absolute top-0 end-0 badge bg-dark m-2">
              Рекомендуємо
            </span>
          )}
        </div>
        
        <div className="card-body d-flex flex-column">
          <h5 className="card-title text-dark fw-bold mb-2">{product.name}</h5>
          <p className="card-text text-muted small flex-grow-1">{product.description}</p>
        </div>
        
        <div className="card-footer bg-white border-0 pt-0 pb-3 px-3">
          <div className="d-flex justify-content-between align-items-center">
            <span className="h5 mb-0 text-dark fw-bold">
              ₴ {product.price.toFixed(2)}
            </span>
            <div data-in-cart={isInCart ? "1" : "0"}>
              <button
                onClick={goToCartClick}
                className={`btn btn-success btn-sm px-3 ${isInCart ? 'd-inline-flex' : 'd-none'} align-items-center gap-1`}
              >
                <i className="bi bi-cart-check"></i>
                <span className="d-none d-sm-inline">У кошику</span>
              </button>
              <button
                onClick={addToCartClick}
                data-product-id={product.id}
                className={`btn btn-outline-success btn-sm px-3 ${isInCart ? 'd-none' : 'd-inline-flex'} align-items-center gap-1`}
              >
                <i className="bi bi-cart-plus"></i>
                <span className="d-none d-sm-inline">Додати</span>
              </button>
            </div>
          </div>
        </div>
      </div>
    </Link>
  </div>
  );
}