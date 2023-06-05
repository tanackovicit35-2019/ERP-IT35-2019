import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import styles from "./Categories.module.scss"

const EventDisplay = () => {
  const [event, setEvent] = useState([]);

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      const response = await axios.get("https://localhost:44300/api/events"); 
      setEvent(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  const handleDelete = async (dogadjajID) => {
    try {
      await axios.delete("https://localhost:44300/api/events/"+`${dogadjajID}`,
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
      <h2 style={{fontSize:"19px", textAlign:"center"}}>Events</h2>
      <Link to="/admin" style={{fontSize:"19px", textAlign:"center", textDecoration:"none", color:"purple"}}>&larr;Back</Link>
      <table style={{marginLeft:"auto",marginRight:"auto"}}>
        <thead>
          <tr>
            <th>Event Name</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {event.map((event) => (
            <tr key={event.dogadjajID}>
              <td>{event.nazivDogadjaja}</td>
              <td>
                <button style={{backgroundColor:"plum", borderColor:"white"}}>
                <Link to={`/events/edit/${event.dogadjajID}`} style={{textDecoration:"none",
                 color:"white"}}>
                  Edit
                </Link>
                </button>{" "}
                |{" "}
                <button style={{backgroundColor:"plum", borderColor:"white", color:"white"}} onClick={() => handleDelete(event.dogadjajID)}>
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <div style={{textAlign:"center"}}>
      <button style={{backgroundColor:"plum", borderColor:"white"}}>
        <Link to="/events" style={{textDecoration:"none", color:"white"}}>Add event</Link>
      </button>
      </div>
    </div>
  );
};

export default EventDisplay;
