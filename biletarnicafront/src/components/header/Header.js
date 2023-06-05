import React, { useState, useEffect, useContext } from "react";
import styles from "./Header.module.scss";
import { Link, NavLink, useHistory } from "react-router-dom";
import {CgShoppingCart} from "react-icons/cg"
import { CiMenuKebab } from "react-icons/ci";
import { IoMdClose } from "react-icons/io";
import { UserContext } from "../../pages/auth/UserContext";
import { useCart } from 'react-use-cart'
const logo = (
  <div className={styles.logo}>
      <Link to="/" style={{color:"transparent"}}>
        <h2>
          Melody<span>House</span>
        </h2>
      </Link>
   </div>
)

const cart = (
  <span className={styles.cart}>
              <Link to="/cart">
                Cart
                <CgShoppingCart style={{marginTop: 4}}/>
              </Link>
            </span>
)

const Header = () => {
  const [show, setShow] = useState(false);
  const [isAdmin, setIsAdmin] = useState(false);
  const { isLoggedIn, setIsLoggedIn } = useContext(UserContext);
  const [showCart, setShowCart] = useState();
  const {
    isEmpty,
    emptyCart,
  } = useCart();

  useEffect(() => {
    const userRole = localStorage.getItem("uloga");
    if (userRole === "zaposleni" || userRole === "Zaposleni") {
      setIsAdmin(true);
    } else {
      setIsAdmin(false);
    }
  },[isLoggedIn]);
  useEffect(() => {
    const userToken = localStorage.getItem("uloga");
    if (userToken) {
      setIsLoggedIn(true);
    } else {
      setIsLoggedIn(false);
    }
  }, []);

  const toggleMenu = () => {
    setShow(!show);
  };

  const hideMenu = () => {
    setShow(false);
  };

  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("uloga");
    localStorage.removeItem("korisnikID");
    localStorage.removeItem("react-use-cart");
    setIsLoggedIn(false); // Set logged in status to false on logout
  };
  return (
    <header>
    <div className={styles.header}>
      {logo}
      <nav
        className={show ? `${styles["show-nav"]}` : `${styles["hide-nav"]}`}
      >
        <div
          className={
            show
              ? `${styles["nav-wrapper"]} ${styles["show-nav-wrapper"]}`
              : `${styles["nav-wrapper"]}`
          }
          onClick={hideMenu}
        ></div>
        
        <ul onClick={hideMenu}>
          <li className={styles["logo-mobile"]}>
            <IoMdClose
              size={22}
              color="black"
              onClick={hideMenu}
              style={{ cursor: "pointer" }}
            />
          </li>
          <li >
            <NavLink 
              to="/"
              className={({ isActive }) =>
                isActive ? `${styles.active}` : ""
              }
            >
              Home
            </NavLink>
          </li>
          <li>
            <NavLink 
              to="/contact"
              className={({ isActive }) =>
                isActive ? `${styles.active}` : ""
              }
            >
              Contact
            </NavLink>
          </li>
        </ul>
        <div className={styles["header-right"]} onClick={hideMenu}>
          <span className={styles.links}>
            {isLoggedIn ? (
              <>
                {!isAdmin && (
                  <NavLink
                    to="/order-history"
                    className={({ isActive }) =>
                      isActive ? `${styles.active}` : ""
                    }
                  >
                    Orders
                  </NavLink>
                )}
                {isAdmin && (
                  <NavLink
                    to="/admin"
                    className={({ isActive }) =>
                      isActive ? `${styles.active}` : ""
                    }
                  >
                    Admin
                  </NavLink>
                )}
                <NavLink to="/" onClick={handleLogout}>
                  Logout
                </NavLink>
              </>
            ) : (
              <>
                <NavLink
                  to="/login"
                  className={({ isActive }) =>
                    isActive ? `${styles.active}` : ""
                  }
                >
                  Login
                </NavLink>
                <NavLink
                  to="/register"
                  className={({ isActive }) =>
                    isActive ? `${styles.active}` : ""
                  }
                >
                  Registration
                </NavLink>
              </>
            )}
          </span>
          <span className={styles.cart} >
          {!isAdmin && (
                  <NavLink
                    to="/cart"
                    className={({ isActive }) =>
                      isActive ? `${styles.active}` : ""
                    }
                  >
                    Cart
                    <CgShoppingCart size={17} style={{marginTop:"0.22rem"}} />
                  </NavLink>
                )}
            
          </span>
        </div>
      </nav>
      <div className={styles["menu-icon"]}>
        <CiMenuKebab size={28} onClick={toggleMenu} />
      </div>
    </div>
  </header>
  )
}

export default Header