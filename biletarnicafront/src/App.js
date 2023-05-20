import { BrowserRouter, Route, Routes } from "react-router-dom";
import { Header, Footer} from "./components"
import { Home, Contact, Login, Register, Cart } from "./pages";
import { UserProvider } from "./pages/auth/UserContext";
import { CartProvider } from 'react-use-cart';

function App() {
  return (
    <>
    <UserProvider>
       <BrowserRouter>
       <Header/>
        <Routes>
          <Route path="/" element={ <Home/> }/>
          <Route path="/contact" element={ <Contact/> }/>
          <Route path="/login" element={ <Login/> }/>
          <Route path="/register" element={ <Register/> }/>
          <Route path="/cart" element={ <Cart/> }/>
          
        </Routes>
        <CartProvider>
          </CartProvider>
       <Footer/>
       </BrowserRouter>
    </UserProvider>
    </>
  );
}

export default App;
