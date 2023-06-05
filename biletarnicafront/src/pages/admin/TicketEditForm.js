import React, { useState, useEffect } from "react";
import axios from "axios";
import styles from "./Categories.module.scss"
import { useParams, Link } from "react-router-dom";

const TicketEditForm = () => {
  const {kartaID} = useParams();
  const [ticketData, setTicketData] = useState({});
  const [formData, setFormData] = useState({
    datumOdrzavanja: "",
    naStanju:"",
    cenaKarte:"",
    izvodjacID:"",
    kategorijaID:"",
    dogadjajID:"",
    slika:"",
    nazivKarte:"",
    izvodjacDto:{},
    kategorijaDto:{},
    dogadjajDto:{}
  });

  useEffect(() => {
    fetchTicketData();
  }, []);

  const fetchTicketData = async () => {
    try {
      const response = await axios.get("https://localhost:44300/api/tickets/"+`${kartaID}`); 
      console.log(response.data);
      setTicketData(response.data);
      setFormData(response.data); 
    } catch (error) {
      console.error(error);
    }
  };
  
  const handleChange = (event) => {
    setFormData({ ...formData, [event.target.name]: event.target.value });
  };
    const handleSubmit = async (event) => {
      event.preventDefault();
      try {
        const izvodjac = await getPerformerId(formData.izvodjacID);
        for (const value of Object.values(formData)) {
          console.log(value);
        }
      const dogadjaj = await getEventId(formData.dogadjajID);
      const kategorija = await getCategoryId(formData.kategorijaID);


      await addTicketToDatabase(izvodjac, dogadjaj, kategorija);

      /*setFormData({
        datumOdrzavanja: "",
        naStanju:"",
        cenaKarte:"",
        izvodjacID:"",
        kategorijaID:"",
        dogadjajID:"",
        slika:"",
        nazivKarte:"",
      });*/
  
      } catch (error) {
        console.error("Error adding ticket:", error);
      }
    };
  
    const getPerformerId = async (izvodjacID) => {
        try {
          const response = await axios.get("https://localhost:44300/api/performers/"+`${izvodjacID}`, {
           
            headers: {
              "Content-Type": "application/json",
            },
          });
        
          const data = response.data;
          console.log(data);
        return data
        } catch (error) {
          console.error("Error retrieving performer ID:", error);
          throw error;
        }
      };
    
      const getEventId = async (dogadjajID) => {
        try {
          const response = await axios.get("https://localhost:44300/api/events/"+`${dogadjajID}`, {
          
            headers: {
              "Content-Type": "application/json",
            },
          });
          const data = response.data;
          console.log(data);
        return data
    
        } catch (error) {
          console.error("Error retrieving event ID:", error);
          throw error;
        }
      };
    
      const getCategoryId = async (kategorijaID) => {
        try {
          const response = await axios.get("https://localhost:44300/api/categories/"+`${kategorijaID}`, {
           
            headers: {
              "Content-Type": "application/json",
            },
          });
          const data = response.data;
          console.log(data);
        return data
        } catch (error) {
          console.error("Error retrieving category ID:", error);
          throw error;
        }
      };
    
  
      const addTicketToDatabase = async (izvodjac, dogadjaj, kategorija) => {
        try {
          const response = await axios.put(
            "https://localhost:44300/api/tickets",
            {
                kartaID:`${kartaID}`,
                nazivKarte:formData.nazivKarte,
                cenaKarte:formData.cenaKarte,
                datumOdrzavanja: formData.datumOdrzavanja,
                kategorijaID:dogadjaj.dogadjajID,
                izvodjacID:izvodjac.izvodjacID,
                dogadjajID:kategorija.kategorijaID,
                naStanju:formData.naStanju,
                slika:formData.slika,
                izvodjacDto:izvodjac,
                dogadjajDto:dogadjaj,
                kategorijaDto:kategorija
            },
            {
              headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${localStorage.getItem("token")}`
              },
            }
          );
          // Handle the response or perform any additional actions
        } catch (error) {
          console.error("Error adding ticket to database:", error);
          throw error;
        }
      
    };

  return (
    <div>
      <h2 style={{ textAlign:"center", color:"purple", fontFamily:"'Parisienne',cursive"}}>Edit Ticket</h2>
      <Link to="/alltickets" style={{fontSize:"19px", textAlign:"center", textDecoration:"none", color:"purple"}}>&larr;Back</Link>

      <form onSubmit={handleSubmit} className={styles.formadmin}>
      <br />
        <label className={styles.labeladmin}>
          Ticket Name:
          <input
            className={styles.inputadmin}
            type="text"
            name="nazivKarte"
            value={formData.nazivKarte}
            onChange={handleChange}
            required
          />
        </label>
        <label className={styles.labeladmin}>
          Date:
          <input
            className={styles.inputadmin}
            type="text"
            name="datumOdrzavanja"
            placeholder="In format YYYY-MM-DD"
            value={formData.datumOdrzavanja}
            onChange={handleChange}
            required
          />
        </label>
        <br />
        <label className={styles.labeladmin}>
          Number of available tickets:
          <input
            className={styles.inputadmin}
            type="text"
            name="naStanju"
            value={formData.naStanju}
            onChange={handleChange}
            required
          />
        </label>
        <br />
        <label className={styles.labeladmin}>
          Price:
          <input
            className={styles.inputadmin}
            type="text"
            name="cenaKarte"
            value={formData.cenaKarte}
            onChange={handleChange}
            required
          />
        </label>
        <br />
        <label className={styles.labeladmin}>
          Preformer:
          <input
            className={styles.inputadmin}
            type="text"
            name="nazivIzvodjaca"
            placeholder="Name of a preformer"
            value={formData.izvodjacDto.nazivIzvodjaca}
            onChange={handleChange}
            required
          />
        </label>
        <br />
        <label className={styles.labeladmin}>
          Category:
          <input
            className={styles.inputadmin}
            type="text"
            name="nazivKategorije"
            placeholder="Name of a category"
            value={formData.kategorijaDto.nazivKategorije}
            onChange={handleChange}
            required
          />
        </label>
        <br />
        <label className={styles.labeladmin}>
          Event:
          <input
            className={styles.inputadmin}
            type="text"
            name="nazivDogadjaja"
            placeholder="Name of an event"
            value={formData.dogadjajDto.nazivDogadjaja}
            onChange={handleChange}
            required
          />
        </label>
        <br />
        <label className={styles.labeladmin}>
          Picture:
          <textarea
            className={styles.inputadmin}
            name="slika"
            placeholder="URL of a picture"
            value={formData.slika}
            onChange={handleChange}
            required
          />
        </label>
        
        <button type="submit" className={styles.button}>
          Edit Ticket
        </button>
      </form>
    </div>
  );
};

export default TicketEditForm;