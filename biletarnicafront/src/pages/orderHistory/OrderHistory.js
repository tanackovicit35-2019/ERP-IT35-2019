import React, { useEffect, useState } from 'react'
import styles from "./OrderHistory.module.scss";
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom';


const OrderHistory = () => {
  const [order, setOrder] = useState([]);
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  useEffect(()=>{
    async function fetchData(){
     try{ const response = await axios.get("https://localhost:44300/api/orders/order", {
      headers: {
        Authorization: `Bearer ${token}`
      }
     });
      setOrder(response.data)
      ;}catch(error){
        console.error(error);
      }
    }
    fetchData();
  },[]);
  return (
    <section>
    <div className={`container ${styles.table}`}>
        {order===null ? (
            <>
            <h1>Your order list is empty! Continue exploring!<br/>
            <Link to="/#product" style={{textDecoration:"none", color:"purple"}}>&larr;Continue shopping</Link></h1>
            <br/>
            </>
        ):(<div>
          <div>
            <h2 style={{marginLeft:"190px", color:"purple", fontFamily:"'Parisienne',cursive"}}>Order list</h2>
            <table>
              <thead>
                <tr className={styles.head}>
                  <th>Order ID:</th>
                  <th>Date:</th>
                  <th>Total price:</th>
                  <th>Actions:</th>
                </tr>
              </thead>
              <tbody>
                {order.map((item) => {
                  return (
                    <tr key={item.porudzbinaID}>
                      <td>{item.porudzbinaID}</td>

                      <td>{item.datum}</td>
                      <td>{item.ukupnaCena}</td>
                      <td>
                        <Link to={`/order-history/${item.porudzbinaID}`}>
                          <button style={{backgroundColor:"plum", color:"white"}}>View order details</button>
                        </Link>
                      </td>
                    </tr>
                  );
                })}
              </tbody>
            </table>
          </div>
        </div>
      )}
    </div>
  </section>)
  

}

export default OrderHistory