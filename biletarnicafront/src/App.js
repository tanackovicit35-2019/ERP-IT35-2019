import { BrowserRouter, Navigate, Route, Routes } from "react-router-dom";
import { Header, Footer} from "./components"
import { Home, Contact, Login, Register, Cart, Admin } from "./pages";
import { UserProvider } from "./pages/auth/UserContext";
import { CartProvider } from 'react-use-cart';
import React,{useEffect, useState} from 'react';
import Categories from "./pages/admin/Categories";
import Product from "./pages/product/Product";
import ProductDetails from "./pages/product/productDetails/ProductDetails";
import Ticket from "./pages/admin/Ticket";
import Event from "./pages/admin/Event";
import Performers from "./pages/admin/Performers";
import Checkout from "./pages/checkout/Checkout";
import OrderDetails from "./pages/admin/OrderDetails";
import OrderHistory from "./pages/orderHistory/OrderHistory";
import OrderHistoryDetails from "./pages/orderHistory/orderHistoryDetails/OrderHistoryDetails"
import Success from "./pages/checkout/Success";
import Cancel from "./pages/checkout/Cancel"
import CategoryDisplay from "./pages/admin/CatergoryDisplay";
import EventDisplay from "./pages/admin/EventDisplay";
import PerformerDisplay from "./pages/admin/PerformerDisplay";
import TicketsTable from "./pages/admin/TicketsDisplay";
import TicketEditForm from "./pages/admin/TicketEditForm";
import EventEditForm from "./pages/admin/EventEditForm";
import CategoryEditForm from "./pages/admin/CategoryEditForm";
import PerformerEditForm from "./pages/admin/PerformerEditForm";


function App() {
  const [isAdmin, setIsAdmin] = useState(false);
  useEffect(() => {
    const role = localStorage.getItem("uloga");
    if (role === "zaposleni" ) {
      setIsAdmin(true);
    }
  }, []);
  return (
    <>
    <UserProvider>
       <BrowserRouter>
       <Header/>
       <CartProvider>
       <Routes>
          <Route path="/" element={ <Home/> }/>
          <Route path="/contact" element={ <Contact/> }/>
          <Route path="/login" element={ <Login/> }/>
          <Route path="/register" element={ <Register/> }/>
          <Route path="#product" element={<Product/>}/>
          <Route path='/product-details/:kartaID' element={<ProductDetails/>}/>
          <Route path='/order-history' element={<OrderHistory/>}/>
          <Route path='/order-history/:porudzbinaID' element={<OrderHistoryDetails />}/>
          <Route path="/cart" element={ <Cart/> }/>
          <Route path="/admin" element={ isAdmin ? <Admin /> : <Navigate to="/"/>} />
          <Route path='/allcategories' element={isAdmin ? <CategoryDisplay/> : <Navigate to="/" />}/>
          <Route path='/allevents' element={isAdmin ? <EventDisplay/> : <Navigate to="/" />}/>
          <Route path='/allperformers' element={isAdmin ? <PerformerDisplay/> : <Navigate to="/" />}/>
          <Route path='/alltickets' element={isAdmin ? <TicketsTable/> : <Navigate to="/" />}/>
          <Route exact path="/tickets/edit/:kartaID" element={isAdmin ? <TicketEditForm kartaID/> : <Navigate to="/" />} />
          <Route exact path="/events/edit/:dogadjajID" element={isAdmin ? <EventEditForm dogadjajID/> : <Navigate to="/" />} />
          <Route exact path="/categories/edit/:kategorijaID" element={isAdmin ? <CategoryEditForm kategorijaID/> : <Navigate to="/" />} />
          <Route exact path="/performers/edit/:izvodjacID" element={isAdmin ? <PerformerEditForm izvodjacID/> : <Navigate to="/" />} />
          <Route path='/categories' element={<Categories/>}/>
          <Route path="/events" element={<Event/>}/>
          <Route path='/orders/:porudzbinaID' element={isAdmin?<OrderDetails/>: <Navigate to="/" />}/>
          <Route path="/performers" element={<Performers/>}/>
          <Route path='/tickets' element={<Ticket/>}/>
          <Route path="/checkout" element={<Checkout/>}/>
          <Route path="/success" element={<Success/>}/>
          <Route path="/cancel" element={<Cancel/>}/>
        </Routes>
       </CartProvider>
        
        
       <Footer/>
       </BrowserRouter>
    </UserProvider>
    </>
  );
}

export default App;
