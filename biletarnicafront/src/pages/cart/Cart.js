import React from 'react';
import { Link } from 'react-router-dom';
import { useCart } from 'react-use-cart';

function Cart() {
  const { isEmpty, items, updateItemQuantity, removeItem } = useCart();

  return (
    <div>
      <h2 style={{fontFamily:"'Parisienne', cursive", textAlign:"center", color:"purple", fontSize:"3rem"}}>Cart</h2>
      {isEmpty ? (
        <div style={{textAlign:"center"}}>
        <p style={{textAlign:"center",fontFamily:"'Parisienne', cursive", color:"purple", textDecoration:"none", fontSize:"2rem"}}>Cart is empty!</p>
        <Link to="/" style={{fontFamily:"'Parisienne', cursive", color:"purple", textDecoration:"none", fontSize:"2rem"}} >Click to continue shopping!</Link>
        </div>
      ) : (
        <ul style={{textAlign:"center"}}>
          {items.map((item) => (
            <li key={item.kartaID}>
              <p>Item ID: {item.kartaID}</p>
              <p>Price: {item.cenaKarte} RSD</p>
              <p>Quantity: {item.quantity}</p>
              <button onClick={() => updateItemQuantity(item.kartaID, item.quantity - 1)}>
                Decrease Quantity
              </button>
              <button onClick={() => updateItemQuantity(item.kartaID, item.quantity + 1)}>
                Increase Quantity
              </button>
              <button onClick={() => removeItem(item.kartaID)}>Remove Item</button>
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}

export default Cart;
