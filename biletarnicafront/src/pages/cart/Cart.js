import React from 'react';
import { Link,useNavigate } from 'react-router-dom';
import { useCart } from 'react-use-cart';

const Cart = () => {
  const {
    isEmpty,
    totalUniqueItems,
    items,
    totalItems,
    updateItemQuantity,
    removeItem,
    emptyCart,
    cartTotal
  } = useCart();
  const navigate = useNavigate();
  

  const checkout=()=>{
    const logedin = localStorage.getItem("token");
    if(logedin!==null){
      navigate("/checkout");
    }else{
      navigate("/login");
    }
  }

  return (
    <div style={{textAlign:"center"}}>
      <h2 style={{fontFamily:"'Parisienne', cursive", textAlign:"center", color:"purple", fontSize:"3rem"}}>Cart</h2>
      <hr style={{marginRight:"38px",marginLeft:"38px", background:"purple", height:"3px"}}></hr>

      {isEmpty ? (
        <div style={{textAlign:"center"}}>
        <p style={{textAlign:"center",fontFamily:"'Parisienne', cursive", color:"purple", textDecoration:"none", fontSize:"2rem"}}>Cart is empty!</p>
        <Link to="/" style={{fontFamily:"'Parisienne', cursive", color:"purple", textDecoration:"none", fontSize:"2rem"}} >Click to continue shopping!</Link>
        </div>
      ) : (
        <ul style={{textAlign:"center", listStyle:'none'}}>
          {items.map((item) => (
            <li key={item.id}>
              <p>Ticket ID: {item.kartaID}</p>
              <p>Price: {item.cenaKarte} RSD</p>
              <p>Quantity: {item.quantity}</p>
              <button style={{background:"plum", color:"white", borderColor:"plum", fontSize:"1rem"}} onClick={() => updateItemQuantity(item.id, item.quantity - 1)}>
                Decrease Quantity
              </button>
              <button style={{background:"plum", color:"white", borderColor:"plum", fontSize:"1rem"}} onClick={() => updateItemQuantity(item.id, item.quantity + 1)}>
                Increase Quantity
              </button>
              <button style={{background:"plum", color:"white", borderColor:"plum", fontSize:"1rem"}} onClick={() => removeItem(item.id)}>Remove Item</button>
              <hr style={{marginRight:"38px", background:"purple", height:"3px"}}></hr>
            </li>
          ))}
          <p style={{color:"purple", fontSize:"1.5rem", fontWeight:"bolder", textDecoration:"double underline"}}>Total with tax: {cartTotal+0.2*cartTotal} RSD</p>
          <button className="btn" style={{background:"plum", color:"white", borderColor:"plum", fontSize:"1rem"}} onClick={checkout}>Checkout</button>
        </ul>
      )}
    </div>
  );
}

export default Cart;
