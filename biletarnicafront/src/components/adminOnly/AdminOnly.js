import React from "react";
import { Link } from "react-router-dom";

const AdminOnlyRoute = ({ children }) => {
  const userRole = localStorage.getItem("uloga");

  if (userRole === "zaposleni"|| userRole==="Zaposleni") {

    return children;
  }
  return (
    <section style={{ height: "80vh" }}>
      <div className="container">
        <h2>Permission Denied.</h2>
        <p>This page can only be view by an Admin user.</p>
        <br />
        <Link to="/">
          <button style={{background:"plum", color:"white", borderColor:"plum", fontSize:"1rem"}}> Back To Home</button>
        </Link>
      </div>
    </section>
  );
};

export const AdminOnlyLink = ({ children }) => {
  const userRole = localStorage.getItem("uloga");

  if (userRole === "zaposleni" || userRole==="Zaposleni") {
    return children;
  }
  return null;
};

export default AdminOnlyRoute;