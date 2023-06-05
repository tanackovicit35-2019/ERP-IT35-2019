import React,{useState, useEffect} from 'react'
import axios from 'axios';
import { useParams, Link } from 'react-router-dom';
import loading from "../../../assets/loading-gif.gif"
import styles from "../orderHistoryDetails/OrderHistoryDetails.module.scss"


const OrderDetails = () => {
    const { porudzbinaID } = useParams();
    const [orderItem, setOrderItem] = useState([]);
    const [tickets, setTickets] = useState([]);
    const [order, setOrder] = useState([]);
    const [user, setUser] = useState([]);
    const token = localStorage.getItem("token");

    useEffect(() => {
      const fetchOrderDetails = async () => {
        try {
          const response = await axios.get(
            "https://localhost:44300/api/orderItem/orders/" + `${porudzbinaID}`);
          console.log(response.data);
          setOrderItem(response.data);

        } catch (error) {
          console.log(error);
        }
      };
      fetchOrderDetails();
    }, [porudzbinaID]);
    /*useEffect(() => {
      async function fetchData() {
        try {
          const response = await axios.get("https://localhost:44300/api/orders/"+`${porudzbinaID}`, {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          });
          console.log(response.data);
          setOrder(response.data);
          const responseUser = await axios.get("https://localhost:44300/api/users/"+`${response.data.korisnikID}`, {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          });
          setUser(responseUser.data);
        } catch (error) {
          console.error(error);
        }
      }
      fetchData();
    }, []);*/
    const userid = order.korisnikID;
    console.log(userid);
    useEffect(() => {
      const fetchTicketDetails = async () => {
        try {
          const ticketRequests = orderItem.map((item) =>
            axios.get("https://localhost:44300/api/tickets/" + `${item.kartaID}`)
          );
          const response = await Promise.all(ticketRequests);
          const tickets = response.map((response) => response.data);
          setTickets(tickets);
        } catch (error) {
          console.log(error);
        }
      };
      fetchTicketDetails();
    }, [orderItem]);

  return (
    <div >
    <h2 style={{textAlign:"center", color:"purple", fontFamily:"'Parisienne',cursive"}}>Order Details</h2>
    <div>
      <Link
        to="/#product"
        color="purple"
        style={{ textDecoration: "none", fontSize: "19px" }}
      >
        &larr; Back To Products
      </Link>
    </div>
    {orderItem === null ? (
      <img src={loading} alt="Loading" style={{ width: "50px" }} />
    ) : (
        <table style={{marginLeft:"auto", marginRight:"auto"}}>
        <thead style={{alignItems:"center"}}>
          <tr style={{textAlign:"center"}}>
            <th>Date of the concert</th>
            <th>Price</th>
            <th>Amount</th>

          </tr>
        </thead>
        <tbody style={{textAlign:"center"}}>
          {tickets.map((ticketItem, index) => (
            <tr key={ticketItem.kartaID}>
              <td>{ticketItem.datumOdrzavanja}</td>
              <td>{ticketItem.cenaKarte} RSD</td>
              <td>{orderItem[index].kolicina}</td>

            </tr> 
          ))}
        </tbody>
      </table>
    )}
  </div>
  )
}

export default OrderDetails