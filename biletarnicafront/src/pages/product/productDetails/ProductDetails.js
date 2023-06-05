import axios from "axios";
import React, { useEffect, useState } from "react";
import { useParams, Link, useNavigate } from "react-router-dom";
import loading from "../../../assets/loading-gif.gif";
import { Container } from "react-bootstrap";
import { useCart } from "react-use-cart";
import styles from "../productDetails/Product.module.scss"


const ProductDetails = () => {
  const { kartaID } = useParams();
  const [ticket, setTicket]= useState(null);
  const [performer, setPerformer] = useState(null);
  const [category, setCategory] = useState(null);
  const [event, setEvent] = useState(null);
  const [quantities, setQuantities] = useState({});
  const {addItem} = useCart();

  useEffect(() => {
    const fetchTicketDetails = async () => {
      try {
        const response = await axios.get(
          'https://localhost:44300/api/tickets/' + `${kartaID}`
        );
        setTicket(response.data);
        initializeQuantities(response.data);
      } catch (error) {
        console.log(error);
      }
    };
    fetchTicketDetails();
  }, [kartaID]);
  useEffect(() => {
    const fetchPerformerDetails = async () => {
      if (ticket !== null) {
        try {
          const authorResponse = await axios.get(
            'https://localhost:44300/api/performers/' + `${ticket.izvodjacID}`
          );
          setPerformer(authorResponse.data);
        } catch (error) {
          console.log(error);
        }
      }
    };

    fetchPerformerDetails();
  }, [ticket]);

  useEffect(() => {
    const fetchEvents = async () => {
      if (ticket !== null) {
        try {
          const eventsResponse = await axios.get(
            'https://localhost:44300/api/events/' + `${ticket.dogadjajID}`
          );
          if(eventsResponse.data!==null){
            setEvent(eventsResponse.data);
          }
          else{
            console.log(eventsResponse.data);
          }
        } catch (error) {
          console.log(error);
        }
      }
    };

    fetchEvents();
  }, [ticket]);
  

  useEffect(() => {
    const fetchCategories = async () => {
      if (ticket !== null) {
        try {
          const categoriesResponse = await axios.get(
            'https://localhost:44300/api/categories/' + `${ticket.kategorijaID}`
          );
          if(categoriesResponse.data!==null){
            setCategory(categoriesResponse.data);
          }
          else{
            console.log(categoriesResponse.data);
          }
        } catch (error) {
          console.log(error);
        }
      }
    };
    

    fetchCategories();
  }, [ticket]);
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
  return (
    <div className={`container ${styles.product}`}>
      <h2 style={{textAlign:"center", fontFamily:"'Parisienne', cursive"}}>Product Details</h2>
      <div>
        <Link
          to="/#products"
          color="purple"
          style={{ textDecoration: "none", fontSize: "18px" }}
        >
          &larr; Back To Products
        </Link>
      </div>
      {ticket === null ? (
        <img src={loading} alt="Loading" style={{ width: "50px" }} />
      ) : (
        <section>
          <div className={styles.details}>
          <div className={styles.img}>
              <img src={ticket.slika} alt={ticket.kartaID} />
            </div>
            <div className={styles.content}>
              
              {performer && (
                <h3>
                  <b>Performer:</b> {performer.nazivIzvodjaca}
                </h3>
              )}
              <h2>{ticket.datumOdrzavanja}</h2>
              <h2 className={styles.price}>
                <b>Price: </b>
                {`${ticket.cenaKarte} RSD`}
              </h2>

              <h2>No of available tickets: {ticket.naStanju}</h2>
              {category && (
                <h2>
                  <b>Category:</b> {category.nazivKategorije}
                </h2>
              )}
              {event && (
                <h2>
                  <b>Event:</b> {event.nazivDogadjaja}
                </h2>
              )}
             <h2> <label>Quantity: </label><input 
            type="number"
            min="1"
            value={quantities[ticket.kartaID] || 1}
            onChange={e => {
              const newQuantities = { ...quantities };
              newQuantities[ticket.kartaID] = parseInt(e.target.value);
              setQuantities(newQuantities);
            }}/></h2>
              <button
                className="btn"
                //className="--btn --btn-danger"
                 onClick={()=> addItem({id:ticket.kartaID,price:ticket.cenaKarte, quantity:quantities[ticket.kartaID], ...ticket})}
              >
                ADD TO CART
              </button>
            </div>
          </div>
        </section>
      )}
    </div>
  );
};
export default ProductDetails;
