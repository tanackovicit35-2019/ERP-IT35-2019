import React, { useState, useEffect } from "react";
import axios from "axios";
import styles from "./Categories.module.scss"
import { useParams, Link } from "react-router-dom";

const EventEditForm = () => {
  const {dogadjajID} = useParams();
  const [eventData, setEventData] = useState({});
  const [formData, setFormData] = useState({
    nazivDogadjaja:""
  });

  useEffect(() => {
    fetchEventData();
  }, []);

  const fetchEventData = async () => {
    try {
      const response = await axios.get("https://localhost:44300/api/events/"+`${dogadjajID}`); 
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


      await addEventToDatabase();

      setFormData({
        nazivDogadjaja:""
      });
  
      } catch (error) {
        console.error("Error adding event:", error);
      }
    };

    
  
      const addEventToDatabase = async () => {
        try {
          const response = await axios.put(
            "https://localhost:44300/api/events",
            {
                dogadjajID:`${dogadjajID}`,
                nazivDogadjaja:formData.nazivDogadjaja
                
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
          console.error("Error adding event to database:", error);
          throw error;
        }
      
    };

  return (
    <div>
      <h2 style={{ textAlign:"center", color:"purple", fontFamily:"'Parisienne',cursive"}}>Edit Event</h2>
      <Link to="/allevents" style={{fontSize:"19px", textAlign:"center", textDecoration:"none", color:"purple"}}>&larr;Back</Link>

      <form onSubmit={handleSubmit} className={styles.formadmin}>
      <br />
        <label className={styles.labeladmin}>
          Event Name:
          <input
            className={styles.inputadmin}
            type="text"
            name="nazivDogadjaja"
            value={formData.nazivDogadjaja}
            onChange={handleChange}
            required
          />
        </label>
        
        
        <button type="submit" className={styles.button}>
          Edit Event
        </button>
      </form>
    </div>
  );
};

export default EventEditForm;