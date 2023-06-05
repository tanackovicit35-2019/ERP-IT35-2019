import React, { useState, useEffect } from "react";
import axios from "axios";
import styles from "./Categories.module.scss"
import { useParams, Link } from "react-router-dom";

const PerformerEditForm = () => {
  const {izvodjacID} = useParams();
  const [eventData, setEventData] = useState({});
  const [formData, setFormData] = useState({
    nazivIzvodjaca:""
  });

  useEffect(() => {
    fetchPerformerData();
  }, []);

  const fetchPerformerData = async () => {
    try {
      const response = await axios.get("https://localhost:44300/api/performers/"+`${izvodjacID}`); 
      setEventData(response.data);
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


      await addPerformerToDatabase();

      setFormData({
        nazivIzvodjaca:""
      });
  
      } catch (error) {
        console.error("Error adding performer:", error);
      }
    };

    
  
      const addPerformerToDatabase = async () => {
        try {
          const response = await axios.put(
            "https://localhost:44300/api/performers",
            {
                izvodjacID:`${izvodjacID}`,
                nazivIzvodjaca:formData.nazivIzvodjaca
                
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
          console.error("Error adding performer to database:", error);
          throw error;
        }
      
    };

  return (
    <div>
      <h2 style={{ textAlign:"center", color:"purple", fontFamily:"'Parisienne',cursive"}}>Edit Category</h2>
      <Link to="/allperformers" style={{fontSize:"19px", textAlign:"center", textDecoration:"none", color:"purple"}}>&larr;Back</Link>

      <form onSubmit={handleSubmit} className={styles.formadmin}>
      <br />
        <label className={styles.labeladmin}>
          Performer Name:
          <input
            className={styles.inputadmin}
            type="text"
            name="nazivIzvodjaca"
            value={formData.nazivIzvodjaca}
            onChange={handleChange}
            required
          />
        </label>
        
        
        <button type="submit" className={styles.button}>
          Edit Performer
        </button>
      </form>
    </div>
  );
};

export default PerformerEditForm;