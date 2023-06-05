import React, { useState, useEffect } from "react";
import { useCart } from "react-use-cart";
import { loadStripe } from "@stripe/stripe-js";
import { useNavigate } from "react-router-dom";
import loading from "../../assets/loading-gif.gif";

const stripePromise = loadStripe("pk_test_51NDYKMFnzJtEpK7S3fuEekiU8nukzXohLodZ1VowpoZZUNsUFC8FGfQy12tyKTdrnlO5Vuvp7zoKme6iYUE6jIDO00p2XiXPJA");

const Checkout = () => {
  const { cartTotal, items } = useCart();
  const [isLoading, setIsLoading] = useState(false);
  const navigate = useNavigate();
  const [products, setProducts] = useState([]);


  const handleCheckout = async (event) => {
    event.preventDefault();
    setIsLoading(true);

    try {

      const cartItems = items.map((item) => ({
        nazivKarte: item.nazivKarte,
        price: item.cenaKarte,
        quantity: item.quantity
      }));

      const payload={
        Amount: cartTotal,
        UserId: localStorage.getItem("korisnikID"),
        cartItems
      }


      const response = await fetch("https://localhost:44300/create-checkout-session", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify(payload),
      })
      console.log(response);
      const responseData = await response.json();
      console.log(responseData);
      const sessionId = responseData.sessionId;
      const stripe = await stripePromise;
      const { error } = stripe.redirectToCheckout({ sessionId });
      if (error) {
        console.log(error);
      }
    } catch (error) {
      console.log(error);
    }

    setIsLoading(false);
  };

  return (
    <section style={{textAlign:"center"}}>
      <form onClick={handleCheckout}>
        
        {isLoading ? (
          <img src={loading} alt="Loading" /> 
        ) : (
          <button style={{backgroundColor:"plum", color:"white", fontWeight:"bold"}} type="submit">Click to pay</button>
        )}
      </form>
    </section>
  );
};

export default Checkout;