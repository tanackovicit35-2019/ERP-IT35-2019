import React, { useState } from "react";
import axios from "axios";
import styles from "./Categories.module.scss";
import { Link } from "react-router-dom";

const AddCategoryForm = ({ handleSubmit, handleChange, formData }) => {
  return (
    <form onSubmit={handleSubmit} className={styles.formadmin}>
        <input
          className={styles.inputadmin}
          type="text"
          name="categoryName"
          placeholder="Category name"
          value={formData.categoryName}
          onChange={handleChange}
          required
        />
      
      <br />
      <button type="submit" className={styles.button}>
        Add Category
      </button>
    </form>
  );
};

const Categories = () => {
  const [formData, setFormData] = useState({
    categoryName: ""
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
      await addCategoryToDatabase();

      // Step 2: Clear the form
      setFormData({
        categoryName: ""
      });

      // Display a success message or perform any additional actions
    } catch (error) {
      console.error("Error adding category:", error);
      alert(error);
      // Display an error message or handle the error accordingly
    }
  };

  const addCategoryToDatabase = async () => {
    try {
      const response = await axios.post(
        "https://localhost:44300/api/categories",
        {
          nazivKategorije: formData.categoryName
        },
        {
          headers: {
            "Content-Type": "application/json",
            Authorization: `Bearer ${localStorage.getItem("token")}`
          },
        }
      );
      alert("Category added");
    } catch (error) {
      console.error("Error adding category to database:", error);
      throw error;
    }
  };

  return (
    <div>
      <h2 style={{fontFamily:"'Parisienne', cursive", color:"purple", fontWeight:"bold", textAlign:"center"}}>Add New Category</h2>
      <Link to="/allcategories" style={{backgroundColor:"puple", fontSize:"20px"}}>&larr;Back</Link>
      <AddCategoryForm
        handleSubmit={handleSubmit}
        handleChange={handleChange}
        formData={formData}
      />
    </div>
  );
};

export default Categories;