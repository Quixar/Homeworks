import { useContext } from "react";
import AppContext from "../../app/features/context/AppContext";
import "../cart/ui/Cart.css"
import { Link } from "react-router-dom";

export default function Cart() {
    const {cart} = useContext(AppContext);
    
    const isEmpty = cart.cartItems.length == 0;
    
    const clearCart = () => {
    const confirmed = window.confirm(
      `Ви підтверджуєте видалення кошику з ${cart.cartItems.length} товарами на ₴${cart.totalPrice}?`
    );

    if (confirmed) {
      request("/api/cart", {
        method: "DELETE",
      })
        .then(() => {
          updateCart();
          alert("Кошик успішно видалено!");
        })
        .catch(alert);
    }
};

    return <>
    <div className="row mb-3">
        <div className="col col-11 text-center"><b className="display-5">Мій кошик</b></div>
        {!isEmpty && <div className="col col-1"><button onClick={clearCart} ><i className="bi bi-x-lg"></i></button></div>}
    </div>
    {isEmpty && <div className="alert alert-warning" role="alert">
        Треба робити&nbsp;
        <Link to="/">базар</Link>
        </div>}
    {!isEmpty &&
    <div className="row cart-table-header mb-2 text-muted">
        <div className="col col-6 col-lg-5 offset-1">Товар</div>
        <div className="col col-1">Ціна</div>
        <div className="col col-3 col-lg-2 text-center">Кількість</div>
        <div className="col col-1">Вартість</div>
        <div className="col col-1"></div>
    </div>
    }
    {cart.cartItems.map(ci => <CartItem cartItem={ci} key={ci.id} />)}

    </>;
}

function CartItem({cartItem}) {
    const {alarm, request, updateCart} = useContext(AppContext);
    const changeQuantity = (cnt) => {
        if(cnt + cartItem.quatity <= 0){
            alarm({
                title: "Дія незворотня",
                message: `Підверджуєте видалення позиції '${cartItem.product.name}'`,
                buttons: [
                    {status: "negative", title: "Скасувати"},
                    {status: "positive", title: "Видалити"},
                    {status: "neutral", title: "Закрити"},
                ],
                icon: "stop"
            })
            .then(status => {
               if(status == "positive"){
                 request("/api/cart/" + cartItem.productId + "?increment=" + cnt, {
                      method: "PATCH",
                  }).then(updateCart).catch(alert);
               }
            })
            .catch(() => {
                console.log("alarm canceled");
            });
        }
        else{
             request("/api/cart/" + cartItem.productId + "?increment=" + cnt, {
                      method: "PATCH",
                  }).then(updateCart).catch(alert);
        }
    }

    

    return  <div className="row align-items-center bg-light border rounded-3 p-3 mb-3 shadow-sm">
     <div className="col-12 col-md-2 text-center mb-3 mb-md-0">
         <img src={cartItem.product.imageUrl}
              className="item-page-img img-fluid rounded"
              style={ {"maxHeight": "100px", "objectFit": "cover"} }
              alt={cartItem.product.name} />
     </div>
     <div className="col-12 col-md-4">
         <h6 className="mb-1">{cartItem.product.name}</h6>
         <p className="text-muted mb-0" style={{"fontSize": "0.9rem"}}>{cartItem.product.description}</p>
     </div>
     <div className="col-12 col-md-2 text-center">
         <span className="fw-bold">₴{cartItem.product.price}</span>
     </div>
     <div className="col-12 col-md-2 text-center">
         <div className="input-group input-group-sm w-75 mx-auto">
             <button data-cart-product-id={cartItem.product.Id} onClick={() => changeQuantity(-1)} className="btn btn-outline-secondary" >-</button>
             <input type="text" className="form-control text-center" value={cartItem.quatity} readOnly style={{"maxWidth": "60px"}}/>
             <button data-cart-product-id={cartItem.product.Id} onClick={() => changeQuantity(1)}  className="btn btn-outline-secondary" >+</button>
         </div>
     </div>
     <div className="col-12 col-md-1 text-center">
         <span className="fw-bold">₴{cartItem.price}</span>
     </div>
     <div className="col-12 col-md-1 text-center">
         <button onClick={() => changeQuantity(-cartItem.quatity)} data-cart-product-id={cartItem.product.id} className="btn btn-outline-danger">
             <i className="bi bi-trash"></i>
         </button>
     </div>
 </div>
} 