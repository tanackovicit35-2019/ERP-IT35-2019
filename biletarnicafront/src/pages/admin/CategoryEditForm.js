import React, { useState, useEffect } from "react";
import axios from "axios";
import styles from "./Categories.module.scss"
import { useParams, Link } from "react-router-dom";

const CategoryEditForm = () => {
  const {kategorijaID} = useParams();
  const [eventData, setEventData] = useState({});
  const [formData, setFormData] = useState({
    nazivKategorije:""
  });

  useEffect(() => {
    fetchEventData();
  }, []);

  const fetchEventData = async () => {
    try {
      const response = await axios.get("https://localhost:44300/api/categories/"+`${kategorijaID}`); 
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


      await addCategoryToDatabase();

      setFormData({
        nazivKategorije:""
      });
  
      } catch (error) {
        console.error("Error adding category:", error);
      }
    };

    
  
      const addCategoryToDatabase = async () => {
        try {
          const response = await axios.put(
            "https://localhost:44300/api/categories",
            {
                kategorijaID:`${kategorijaID}`,
                nazivKategorije:formData.nazivKategorije
                
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
          console.error("Error adding category to database:", error);
          throw error;
        }
      
    };

  return (
    <div>
      <h2 style={{ textAlign:"center", color:"purple", fontFamily:"'Parisienne',cursive"}}>Edit Category</h2>
      <Link to="/allcategories" style={{fontSize:"19px", textAlign:"center", textDecoration:"none", color:"purple"}}>&larr;Back</Link>

      <form onSubmit={handleSubmit} className={styles.formadmin}>
      <br />
        <label className={styles.labeladmin}>
          Category Name:
          <input
            className={styles.inputadmin}
            type="text"
            name="nazivKategorije"
            value={formData.nazivKategorije}
            onChange={handleChange}
            required
          />
        </label>
        
        
        <button type="submit" className={styles.button}>
          Edit Category
        </button>
      </form>
    </div>
  );
};

export default CategoryEditForm;