import React, {useEffect} from 'react'
import { useNavigate } from 'react-router-dom'
import { useCart } from 'react-use-cart'

function Success() {
  const {
    isEmpty,
    emptyCart,
  } = useCart();
  const navigate = useNavigate();


  useEffect(()=>{
    if(isEmpty!==true){
      emptyCart();
      navigate("/order-history");
    }
      navigate("/order-history");
  })

  return (
    <div style={{textAlign:"center", fontFamily:"'Parisienne',cursive", fontSize:"18px", color:"purple"}}>Success!
    <h2 style={{textAlign:"center", fontFamily:"'Parisienne',cursive", fontSize:"18px", color:"purple"}}>Thank you for shopping at MelodyHouse!</h2>
    </div>
  )
}

export default Success