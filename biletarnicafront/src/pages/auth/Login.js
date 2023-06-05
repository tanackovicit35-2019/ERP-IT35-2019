import React, { useState, useEffect, useContext } from 'react'
import style from "./auth.module.scss"
import axios from "axios";
import loginImg from "../../assets/login.jpg"
import { Link, useNavigate } from 'react-router-dom'
import { UserContext } from './UserContext'


function Login() {
  const navigate = useNavigate();
  const [korisnickoIme, setUsername] = useState("");
  const [lozinka, setPassword] = useState("");
  const { setIsLoggedIn } = useContext(UserContext);

  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token!==null) {
      setIsLoggedIn(true);
    } else {
      setIsLoggedIn(false);
    }
  }, []);

  const handleUsernameChange = (value) => {
    setUsername(value);
  };
  const handlePasswordChange = (value) => {
    setPassword(value);
  };
  const handleLogin = () => {
    const userdata = {
      username: korisnickoIme,
      lozinka: lozinka,
    };

    axios
      .post("https://localhost:44300/login", userdata)
      .then((result) => {
        if(result.data);
        const korisnikID = result.data.response.korisnikID;
        localStorage.setItem("korisnikID", korisnikID);
        const role = result.data.response.role;
        localStorage.setItem("uloga", role);
        localStorage.setItem("token", result.data.response.token);
        if (role === "zaposleni") {
          navigate("/admin");
        } else if (role  === "kupac") {
          navigate("/")
        }
      })
      .catch((error) => {
        alert("Error while trying to login");
      });
      setIsLoggedIn(true);

  };


  return( 
  <section className={`container ${style.auth}`}>
      <div className={style.img} >
        <img src={loginImg} alt="Login" width="400"/>
      </div>
      <div className={style.form} style={{textAlign:"center"}}>
      <h2>Login</h2>
     
      <form>
        <input type='text' id="txtUsername" placeholder='Username' onChange={(e) => 
        handleUsernameChange(e.target.value)}/>

        <input type='password' id="txtPassword" placeholder='Password' onChange={(e) =>
        handlePasswordChange(e.target.value)}/>
        <p></p>

        <button onClick={() => handleLogin()}  style={{background:"plum", color:"white", borderColor:"plum", fontSize:"1.5rem", fontFamily: "'Parisienne', cursive"}} >Login</button>
        <p></p>
        <span className={style.register}>
          <p>Don't have an account?</p>
          <Link to="/register" style={{color:"purple", textDecoration:"none", fontWeight:"bold"}}>Register here!</Link>
        </span>
      </form>
      
      </div>
  </section>
  );
}

export default Login;