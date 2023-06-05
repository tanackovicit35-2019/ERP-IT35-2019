import React, { useEffect } from 'react';
import styles from "./Home.module.scss";
import { Link } from 'react-router-dom'
import Slider from '../../components/slider/Slider';
import Product from "../../pages/product/Product";

const Home = () => {
  useEffect(() => {
    const scrollToProducts = () => {
      if (window.location.hash === "#product") {
        window.scrollTo({
          top: 700,
          behavior: "smooth"
        });
      }
    };
    scrollToProducts();
  }, []);

  return (
    <div>
      <Slider />
      <Product />
    </div>
  );
};

export default Home;
