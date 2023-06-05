import React, { useState, useEffect } from "react";
import axios from "axios";
import { Link } from "react-router-dom";

const TicketsTable = () => {
  const [tickets, setTickets] = useState([]);

  useEffect(() => {
    fetchData();
  }, []);

  const fetchData = async () => {
    try {
      const response = await axios.get("https://localhost:44300/api/tickets"); 
      setTickets(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  const handleDelete = async (kartaID) => {
    try {
      await axios.delete("https://localhost:44300/api/tickets/"+`${kartaID}`,
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
    <div>
      <h2 style={{fontSize:"25px", textAlign:"center", color:"purple", fontFamily:"'Parisienne',cursive"}}>Tickets</h2>
      <Link to="/admin" style={{fontSize:"19px", textAlign:"center", textDecoration:"none", color:"purple"}}>&larr;Back</Link>

      <table style={{marginLeft:"auto", marginRight:"auto"}}>
        <thead>
          <tr>
            <th>Ticket ID</th>
            <th>Ticket Name</th>
            <th>Price</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {tickets.map((ticket) => (
            <tr key={ticket.kartaID}>
              <td>{ticket.kartaID}</td>
              <td>{ticket.nazivKarte}</td>
              <td>{ticket.cenaKarte}</td>
              <td>
                <button style={{backgroundColor:"plum", borderColor:"white"}}>
                <Link to={`/tickets/edit/${ticket.kartaID}`} style={{textDecoration:"none",
                 color:"white"}}>
                  Edit
                </Link>
                </button>{" "}
                |{" "}
                <button style={{backgroundColor:"plum", borderColor:"white", color:"white"}} onClick={() => handleDelete(ticket.kartaID)}>
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <div style={{textAlign:"center"}}>
      <button style={{backgroundColor:"plum", borderColor:"white", marginBottom:"2px"}}>
        <Link to="/tickets" style={{textDecoration:"none", color:"white"}}>Add new ticket</Link>
      </button>
      </div>
    </div>
  );
};

export default TicketsTable;
