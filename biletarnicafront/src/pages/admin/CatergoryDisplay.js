import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import styles from "./Categories.module.scss"

const CategoryDisplay = () => {
  const [category, setCategory] = useState([]);
  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      const response = await axios.get("https://localhost:44300/api/categories"); 
      setCategory(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  const handleDelete = async (kategorijaID) => {
    try {
      await axios.delete("https://localhost:44300/api/categories/"+`${kategorijaID}`,
      {
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
      }); 
      fetchData();
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <div style={{height:"600px"}}>
      <h2 style={{fontSize:"19px", textAlign:"center"}}>Categories</h2>
      <Link to="/admin" style={{fontSize:"19px", textAlign:"center", textDecoration:"none", color:"purple"}}>&larr;Back</Link>
      <table style={{marginLeft:"auto",marginRight:"auto"}}>
        <thead>
          <tr>
            <th>Category Name</th>
            <th>Actions</th>

          </tr>
        </thead>
        <tbody>
          {category.map((category) => (
            <tr key={category.kategorijaID}>
              <td>{category.nazivKategorije}</td>
              <td>
                <button style={{backgroundColor:"plum", borderColor:"white"}}>
                <Link to={`/categories/edit/${category.kategorijaID}`} style={{textDecoration:"none",
                 color:"white"}}>
                  Edit
                </Link>
                </button>{" "}
                |{" "}
                <button style={{backgroundColor:"plum", borderColor:"white", color:"white"}} onClick={() => handleDelete(category.kategorijaID)}>
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <div style={{textAlign:"center"}}>
      <button style={{backgroundColor:"plum", borderColor:"white"}}>
        <Link to="/categories" style={{textDecoration:"none", color:"white"}}>Add category</Link>
      </button>
      </div>
    </div>
  );
};

export default CategoryDisplay;
