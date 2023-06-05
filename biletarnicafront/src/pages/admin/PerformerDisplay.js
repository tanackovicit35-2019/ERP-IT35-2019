import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import styles from "./Categories.module.scss"

const PerformerDisplay = () => {
  const [performer, setPerformer] = useState([]);

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      const response = await axios.get("https://localhost:44300/api/performers"); 
      setPerformer(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  const handleDelete = async (izvodjacID) => {
    try {
      await axios.delete("https://localhost:44300/api/performers/"+`${izvodjacID}`,
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
      <h2 style={{fontSize:"19px", textAlign:"center"}}>Performers</h2>
      <Link to="/admin" style={{fontSize:"19px", textAlign:"center", textDecoration:"none", color:"purple"}}>&larr;Back</Link>
      <table style={{marginLeft:"auto",marginRight:"auto"}}>
        <thead>
          <tr>
            <th style={{textAlign:"center"}}>Performer Name</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {performer.map((performer) => (
            <tr key={performer.izvodjacID}>
              <td>{performer.nazivIzvodjaca}</td>
              <td>
                <button style={{backgroundColor:"plum", borderColor:"white"}}>
                <Link to={`/performers/edit/${performer.izvodjacID}`} style={{textDecoration:"none",
                 color:"white"}}>
                  Edit
                </Link>
                </button>{" "}
                |{" "}
                <button style={{backgroundColor:"plum", borderColor:"white", color:"white"}} onClick={() => handleDelete(performer.izvodjacID)}>
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <div style={{textAlign:"center"}}>
      <button style={{backgroundColor:"plum", borderColor:"white"}}>
        <Link to="/performers" style={{textDecoration:"none", color:"white"}}>Add a performer</Link>
      </button>
      </div>
    </div>
  );
};

export default PerformerDisplay;
