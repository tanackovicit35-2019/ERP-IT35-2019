import React from "react";
import { Link } from "react-router-dom";
import styles from "./Sidebar.module.scss";

const Sidebar = () => {
  return (
    <div className={styles.sidebar}>
      <ul className={styles.menu}>
        <li>
          <Link to="/alltickets" className={styles.menuItem}>
            Tickets
          </Link>
        </li>
        <li>
          <Link to="/allcategories" className={styles.menuItem}>
            Categories
          </Link>
        </li>
        <li>
          <Link to="/allevents" className={styles.menuItem}>
            Events
          </Link>
        </li>
        <li>
          <Link to="/allperformers" className={styles.menuItem}>
            Performers
          </Link>
        </li>
      </ul>
    </div>
  );
};

export default Sidebar;