import React from 'react'
import style from "./Header.module.scss"
import { Link, NavLink } from 'react-router-dom'
import {CgShoppingCart} from "react-icons/cg"

const logo = (
  <div className={style.logo}>
      <Link to="/" style={{color:"transparent"}}>
        <h2>
          Melody<span>House</span>
        </h2>
      </Link>
   </div>
)
const cart = (
  <span className={style.cart}>
              <Link to="/cart">
                Cart
                <CgShoppingCart style={{marginTop: 4}}/>
              </Link>
            </span>
)

const Header = () => {

  return (
    <header>
      <div className={style.header}>
        {logo}

        <nav>
          
          <ul>
            <li>
              <NavLink to="/">
                Home
              </NavLink>
            </li>
            <li>
              <NavLink to="/contact">
                Contact
              </NavLink>
            </li>
          </ul>
          <div className={style["header-right"]}>
            <span className={style.links}>
              <NavLink to="/login">Login</NavLink>
              <NavLink to="/register">Register</NavLink>
              <NavLink to="/order-history">Orders</NavLink>
            </span>
            {cart}
          </div>
          
        </nav>
      </div>
    </header>
  )
}

export default Header