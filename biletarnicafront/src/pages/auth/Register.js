import React, { useState } from 'react'
import style from "./auth.module.scss"
import loginImg from "../../assets/login.jpg"
import { Link } from 'react-router-dom'
import axios from 'axios';
function Register() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [name, setName] = useState("");
  const [surname, setSurname] = useState("");
  const [email, setEmail] = useState("");

  const handleUsernameChange = (value) => {
    setUsername(value);
  };

  const handlePasswordChange = (value) => {
    setPassword(value);
  };

  const handleNameChange = (value) => {
    setName(value);
  };

  const handleSurnameChange = (value) => {
    setSurname(value);
  };

  const handleEmailChange = (value) => {
    setEmail(value);
  };


  const handleSave = () => {
    const data = {
      korisnickoIme: username,
      lozinka: password,
      ime: name,
      prezime: surname,
      email:email
    };

    axios
    .post("https://localhost:44300/register", data)
    .then((result) =>{
      alert("Success! Login now to continue!");
    })
    .catch((error) =>{
      alert(error);
    });
  };

  return( <section className={`container ${style.auth}`}>
  <div className={style.img} >
    <img src={loginImg} alt="Register" width="400"/>
  </div>
  <div className={style.form} style={{textAlign:"center"}}>
  <h2>Register</h2>
 
  <form>
    <input type='text' placeholder='Username' onChange={(e) => handleUsernameChange(e.target.value)} required/>
    <input type='password' placeholder='Password' onChange={(e) => handlePasswordChange(e.target.value)} required/>
    <input type='text' placeholder='Name' onChange={(e) => handleNameChange(e.target.value)} required/>
    <input type='text' placeholder='Surname' onChange={(e) => handleSurnameChange(e.target.value)} required/>
    <input type='email' placeholder='Email' onChange={(e) => handleEmailChange(e.target.value)} required/>
    <p></p>

    <button onClick={() => handleSave()}  style={{background:"plum", color:"white", borderColor:"plum", fontSize:"1.5rem", fontFamily: "'Parisienne', cursive"}} >Register</button>
    <p></p>
    <div className={style.links}>
    </div>
  </form>
  
  </div>
</section>
);
}


export default Register