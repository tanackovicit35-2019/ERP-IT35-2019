import React, { useState } from "react";
import axios from "axios";
import styles from "./Categories.module.scss";
import { Link } from "react-router-dom";

const AddPerformerForm = ({ handleSubmit, handleChange, formData }) => {
  return (
    <form onSubmit={handleSubmit} className={styles.formadmin}>
        <input
          className={styles.inputadmin}
          type="text"
          name="performerName"
          placeholder="Performer name"
          value={formData.performerName}
          onChange={handleChange}
          required
        />
      
      <br />
      <button type="submit" className={styles.button}>
        Add Performer
      </button>
    </form>
  );
};

const Performers = () => {
  const [formData, setFormData] = useState({
    performerName: ""
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
      await addPerformerToDatabase();

      // Step 2: Clear the form
      setFormData({
        performerName: ""
      });

      // Display a success message or perform any additional actions
    } catch (error) {
      console.error("Error adding category:", error);
      alert(error);
      // Display an error message or handle the error accordingly
    }
  };

  const addPerformerToDatabase = async () => {
    try {
      const response = await axios.post(
        "https://localhost:44300/api/performers",
        {
          nazivIzvodjaca: formData.performerName
        },
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${localStorage.getItem("token")}`
          },
        }
      );
      alert("Performer added");
    } catch (error) {
      console.error("Error adding performer to database:", error);
      throw error;
    }
  };

  return (
    <div>
      <h2>Add New Performer</h2>
      <Link to="/admin">&larr;Back</Link>
      <AddPerformerForm
        handleSubmit={handleSubmit}
        handleChange={handleChange}
        formData={formData}
      />
    </div>
  );
};

export default Performers;