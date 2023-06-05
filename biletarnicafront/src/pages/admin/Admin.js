import React, { useState, useEffect } from "react";
import styles from "./Admin.module.scss";
import Sidebar from "../../components/sidebar/Sidebar";
import axios from "axios";
import { Link, useNavigate } from "react-router-dom";

const Admin = () => {
  const [order, setOrder] = useState([]);
  const [user, setUser] = useState([]);
  const token = localStorage.getItem("token");
  const navigate = useNavigate();
  useEffect(() => {
    async function fetchData() {
      try {
        const response = await axios.get("https://localhost:44300/api/orders", {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });
        setOrder(response.data);
      } catch (error) {
        console.error(error);
      }
    }
    fetchData();
  }, []);
 
  return (
    <div className={styles.admincontainer}>
      <Sidebar />
      <section className={styles.sectionadmin}>
        <div className={`container ${styles.table}`}>
          {order === null ? (
            <>
              <h1>
                Your order list is empty! Continue exploring!
                <br />
                <Link
                  to="/#products"
                  style={{ textDecoration: "none", color: "purple", fontFamily:"'Parisienne',cursive" }}
                >
                  &larr;Continue shopping
                </Link>
              </h1>
              <br />
            </>
          ) : (
            <div>
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
                            <Link to={`/orders/${item.porudzbinaID}`}>
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
      </section>
    </div>
  );
};

export default Admin;
