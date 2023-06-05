import React, { useEffect, useState } from 'react'
import styles from "./Product.module.scss"
import ProductList from "./productList/ProductList";
import axios from 'axios';
import { FaCogs } from "react-icons/fa";
const Product = () => {
  const [data, setData] = useState([]);
  const [category, setCategory] = useState([]);
  const [selectedCategory, setSelectedCategory] = useState(null);
  const [tickets, setTickets] = useState([]);

  useEffect(()=>{
    async function fetchCategory(){
      try{
        const cat = await axios.get("https://localhost:44300/api/categories");
        setCategory(cat.data);
      }catch(error){
        console.log(error);
      }
    };
    fetchCategory();
  }, []);

  useEffect(() => {
    const fetchTicketsByCategory = async () => {
      try {
        const response = await axios.get("https://localhost:44300/tickets/"+`${selectedCategory}`);
        setTickets(response.data);
      } catch (error) {
        console.error('Error fetching tickets by category:', error);
      }
    };

    if (selectedCategory) {
      fetchTicketsByCategory();
    }
  }, [selectedCategory]);

  const handleCategorySelect = (categoryName) => {
    setSelectedCategory(categoryName);
  };
  useEffect(()=>{
    async function fetchData(){
     try{ const response = await axios.get("https://localhost:44300/api/tickets/");
      setData(response.data)
      ;}catch(error){
        console.error(error);
      }
    }
    fetchData();
  },[]);

  return (
    
    <section>
      <div className={`container ${styles.product}`}>
      <aside>
            <ul>
            {category.map((categ)=>(
              <li key={categ.kategorijaID} onClick={()=>handleCategorySelect(categ.nazivKategorije)} style={{cursor:"pointer"}}>{categ.nazivKategorije}</li>
            ))}
          </ul>
        
        </aside>
        <div className={styles.content}>
            <img
             // src={spinnerImg}
             // alt="Loading.."
             // style={{ width: "50px" }}
              //className="--center-all"
            />{selectedCategory ? (<ProductList products={tickets}/>):( <ProductList products={data} />)}
          
          <div className={styles.icon}>
            <FaCogs size={20} color="rgb(83,72,43)" />
           
            <p>
            </p>
          </div>
        </div>
      </div>
    </section>
  );
};

export default Product