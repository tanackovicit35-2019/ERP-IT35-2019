import React, { useState } from "react";
import axios from "axios";
import styles from "./Categories.module.scss";
import { Link } from "react-router-dom";

const AddEventForm = ({ handleSubmit, handleChange, formData }) => {
  return (
    <form onSubmit={handleSubmit} className={styles.formadmin}>
        <input
          className={styles.inputadmin}
          type="text"
          name="eventName"
          placeholder="Event name"
          value={formData.eventName}
          onChange={handleChange}
          required
        />
      
      <br />
      <button type="submit" className={styles.button}>
        Add Event
      </button>
    </form>
  );
};

const Events = () => {
  const [formData, setFormData] = useState({
    eventName: ""
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
      // Step 1: Add category to the database
      await addEventToDatabase();

      // Step 2: Clear the form
      setFormData({
        eventName: ""
      });

      // Display a success message or perform any additional actions
    } catch (error) {
      console.error("Error adding event:", error);
      alert(error);
      // Display an error message or handle the error accordingly
    }
  };

  const addEventToDatabase = async () => {
    try {
      const response = await axios.post(
        "https://localhost:44300/api/events",
        {
          nazivDogadjaja: formData.eventName
        },
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${localStorage.getItem("token")}`
          },
        }
      );
      alert("Event added");
    } catch (error) {
      console.error("Error adding event to database:", error);
      throw error;
    }
  };

  return (
    <div>
      <h2>Add New Event</h2>
      <Link to="/admin">&larr;Back</Link>
      <AddEventForm
        handleSubmit={handleSubmit}
        handleChange={handleChange}
        formData={formData}
      />
    </div>
  );
};

export default Events;