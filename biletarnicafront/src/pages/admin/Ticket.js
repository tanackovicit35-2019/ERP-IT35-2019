import React, { useState } from "react";
import axios from "axios";
import styles from "./Categories.module.scss";
import { Link } from "react-router-dom";

export const Ticket = () => {
  const [formData, setFormData] = useState({
    datumOdrzavanja: "",
    naStanju:"",
    cenaKarte:"",
    nazivIzvodjaca:"",
    nazivKategorije:"",
    nazivDogadjaja:"",
    slika:"",
    nazivKarte:""
  });

  const handleChange = (event) => {
    const { name, value } = event.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
    try {
      const izvodjacID = await getPerformerId(formData.nazivIzvodjaca);
      const dogadjajID = await getEventId(formData.nazivDogadjaja);
      const kategorijaID = await getCategoryId(formData.nazivKategorije);


      await addTicketToDatabase(izvodjacID, dogadjajID, kategorijaID);

      setFormData({
        datumOdrzavanja: "",
        naStanju:"",
        cenaKarte:"",
        nazivIzvodjaca:"",
        nazivKategorije:"",
        nazivDogadjaja:"",
        slika:"",
        nazivKarte:""
      });

    } catch (error) {
      console.error("Error adding ticket:", error);
    }
  };

  const getPerformerId = async (nazivIzvodjaca) => {
    try {
      const response = await axios.get("https://localhost:44300/performers/name", {
        params: {
          nazivIzvodjaca
        },
        headers: {
          "Content-Type": "application/json",
        },
      });
      const data = response.data;
      console.log(data);
      return data.izvodjacID
    } catch (error) {
      console.error("Error retrieving performer ID:", error);
      throw error;
    }
  };

  const getEventId = async (nazivDogadjaja) => {
    try {
      const response = await axios.get("https://localhost:44300/events/name", {
        params: {
          nazivDogadjaja
        },
        headers: {
          "Content-Type": "application/json",
        },
      });
      const data = response.data;
      console.log(data);
      return data.dogadjajID
    } catch (error) {
      console.error("Error retrieving event ID:", error);
      throw error;
    }
  };

  const getCategoryId = async (nazivKategorije) => {
    try {
      const response = await axios.get("https://localhost:44300/category/name", {
        params: {
          nazivKategorije
        },
        headers: {
          "Content-Type": "application/json",
        },
      });
      const data = response.data;
      console.log(data);
      return data.kategorijaID
    } catch (error) {
      console.error("Error retrieving category ID:", error);
      throw error;
    }
  };

  const addTicketToDatabase = async (izvodjacID, dogadjajID, kategorijaID) => {
    try {
      const response = await axios.post(
        "https://localhost:44300/api/tickets",
        {
            datumOdrzavanja: formData.datumOdrzavanja,
            naStanju:formData.naStanju,
            cenaKarte:formData.cenaKarte,
            izvodjacID:parseInt(izvodjacID),
            kategorijaID:parseInt(dogadjajID),
            dogadjajID:parseInt(kategorijaID),
            slika:formData.slika,
            nazivKarte:formData.nazivKarte
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
      <h2 style={{ textAlign:"center", color:"purple", fontFamily:"'Parisienne',cursive"}}>Add New Ticket</h2>
      <Link to="/admin" style={{fontSize:"19px", textAlign:"center", textDecoration:"none", color:"purple"}}>&larr;Back</Link>
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
            value={formData.nazivIzvodjaca}
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
            value={formData.nazivKategorije}
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
            value={formData.nazivDogadjaja}
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
          Add Ticket
        </button>
      </form>
    </div>
  );
};
export default Ticket;
