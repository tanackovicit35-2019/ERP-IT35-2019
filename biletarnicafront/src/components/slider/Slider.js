import React, { useEffect, useState } from 'react'
import "./Slider.module.scss";
import {AiOutlineArrowLeft, AiOutlineArrowRight} from "react-icons/ai";
import { sliderData } from './SliderData';


const Slider = () => {
  const [currentSlide, setCurrentSlide] = useState(0);
  const slideLenght = sliderData.length;

 

  const nextSlide=()=>{
    setCurrentSlide(currentSlide === slideLenght-1? 0: currentSlide+1);
  };
  const prevSlide=()=>{
    setCurrentSlide(currentSlide === 0 ? slideLenght-1: currentSlide-1);
  };

  

  return (
    <div className="slider" style={{textAlign:"center"}}>
      <AiOutlineArrowLeft style={{color:"purple"}} className="arrow prev" onClick={prevSlide}/>
      <AiOutlineArrowRight style={{color:"purple"}} className="arrow next" onClick={nextSlide}/>
      {sliderData.map((slide, index)=>{
        const{image, heading, desc} = slide


        return(
          <div key={index} className={index === currentSlide ? "slide current":"slide"}>
            {index === currentSlide &&(
              <>
                <img src={image} alt="slide"/>
                <div className="content">
                  <h2 style={{fontFamily:"'Parisienne',cursive", fontWeight:"bold", color:"purple", fontSize:"30px"}}>{heading}</h2>
                  <p>{desc}</p>
                  <hr/>
                  <a href='#product' className='btn' style={{color:"purple", textDecoration:"none", fontWeight:"bold",fontSize:"2rem", fontFamily:" 'Parisienne', cursive"}}>
                    Shop now
                  </a>
                </div>
              </>
            )}
            </div>
        )
      })}
    </div>
  )
}

export default Slider