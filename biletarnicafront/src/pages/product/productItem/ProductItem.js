import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import Card from "../../card/Card";
import styles from "./ProductItem.module.scss";
import axios from "axios";


const ProductItem = ({ticket, grid, kartaID, datumOdrzavanja, naStanju,  cenaKarte, izvodjacID, kategorijaID, dogadjajID,  slika})=>{
    const [performer, setPerformer] = useState(null);
    const shortenText=(text, n)=>{
        if(text.length>n){
            const shortenedText = text.substring(0,n).concat("...");
            return shortenedText;
        }
        return text;
    };
    useEffect(() => {
        const fetchPerformerDetails = async () => {
          if (ticket !== null) {
            try {
              const authorResponse = await axios.get(
                'https://localhost:44300/api/performers/' + `${ticket.izvodjacID}`
              );
              setPerformer(authorResponse.data);
            } catch (error) {
              console.log(error);
            }
          }
        };
    
        fetchPerformerDetails();
      }, [ticket]);
    
    return(
        <Card cardClass={grid? `${styles.grid}` : `${styles.list}`}>
           <Link to={`/product-details/${kartaID}`}>
                <div className={styles.img}>
                    <img src={slika} alt={kartaID}/>
                </div>
             
            <div className={styles.content}>
                <div className={styles.details}>
                    <p>{`${cenaKarte} RSD`}</p>
                    {performer && (
                <h2 style={{color:"black"}}>
                   {performer.nazivIzvodjaca}
                </h2>
              )}
                </div>
            </div>
            </Link>
        </Card>
    );
};

export default ProductItem;