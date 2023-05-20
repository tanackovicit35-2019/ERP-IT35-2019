import React, { useEffect, useState } from 'react';
import axios from 'axios';

function ProductList() {
  const [products, setProducts] = useState([]);
  const [quantities, setQuantities] = useState({});
  const [cartItems, setCartItems] = useState([]);

  useEffect(() => {
    axios
      .get('https://localhost:44300/api/tickets')
      .then(response => {
        setProducts(response.data);
        initializeQuantities(response.data);
      })
      .catch(error => {
        console.error(error);
      });
  }, []);

  useEffect(() => {
    const storedCartItems = JSON.parse(localStorage.getItem('cartItems')) || [];
    setCartItems(storedCartItems);
  }, []);

  useEffect(() => {
    localStorage.setItem('cartItems', JSON.stringify(cartItems));
  }, [cartItems]);

  const initializeQuantities = products => {
    const initialQuantities = {};
    products.forEach(product => {
      initialQuantities[product.kartaID] = 1; // Set initial quantity to 1 for each product
    });
    setQuantities(initialQuantities);
  };

  const updateQuantity = (kartaID, newQuantity) => {
    setQuantities(prevQuantities => ({
      ...prevQuantities,
      [kartaID]: newQuantity
    }));
  };

  const handleAddToCart = (product, quantity) => {
    if (quantity <= 0) {
      alert('Please choose a valid quantity.');
      return;
    }

    const cartItem = {
      kartaID: product.kartaID,
      cenaKarte: product.cenaKarte,
      quantity: parseInt(quantity)
    };

    setCartItems(prevCartItems => [...prevCartItems, cartItem]);
    updateQuantity(product.kartaID, quantity);
  };

  const removeItem = kartaID => {
    setCartItems(prevCartItems =>
      prevCartItems.filter(item => item.kartaID !== kartaID)
    );
  };

  return (
    <div>
      <h1 style={{fontFamily:"'Parisienne', cursive", textAlign:"center", color:"purple", fontSize:"3rem" }}>Product List</h1>
      <hr color='purple'></hr>
      {products.map(product => (
        <div key={product.kartaID} style={{textAlign:"center"}}>
          <h3 style={{fontFamily:"'Parisienne', cursive", color:"purple", fontSize:"1.9rem"}}>Concert ticket</h3>
          <p>Ticket ID: {product.kartaID}</p>
          <p>Date: {product.datumOdrzavanja}</p>
          <p>Number of available tickets: {product.naStanju}</p>
          <p>Price: {product.cenaKarte} RSD</p>
          <p>Performer: {product.izvodjacID}</p>
          <p>Category: {product.kategorijaID}</p>
          <p>Event: {product.dogadjajID}</p>
          <label>Quantity: </label><input 
            type="number"
            min="1"
            value={quantities[product.kartaID] || 1}
            onChange={e => {
              const newQuantities = { ...quantities };
              newQuantities[product.kartaID] = parseInt(e.target.value);
              setQuantities(newQuantities);
            }}
          /><p></p>
          <button style={{background:"plum", color:"white", borderColor:"plum", fontSize:"1rem", fontFamily: "'Parisienne', cursive", fontWeight:"bolder"}}
            onClick={() => handleAddToCart(product, quantities[product.kartaID] || 1)}

          >
            Add to Cart
          </button>
          <p></p>
          <hr color='purple'></hr>
        </div>
      ))}
    </div>
  );
}

export default ProductList;
