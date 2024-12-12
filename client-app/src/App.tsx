import Footer from "./Components/Footer.tsx";
import {Container} from "semantic-ui-react";
import NavBar from "./Components/NavBar.tsx";
import 'semantic-ui-css/semantic.min.css';
import './mystyle.css';
import Collections from "./Components/Collections.tsx";
import {Route, BrowserRouter as Router, Routes} from "react-router-dom";
import LogIn from "./Components/Account/LogIn.tsx";
import Register from "./Components/Account/Register.tsx";
import AccessDenied from "./Components/Account/AccessDenied.tsx";

function App() {
    return (
        <Router>
            <NavBar/>
            <Container style={{marginTop: '1em'}}>
                <Routes>
                    <Route path = "/" element={<Collections/>} />
                    <Route path = "/login" element={<LogIn/>}/>
                    <Route path = "/register" element={<Register/>}/>
                    <Route path= "/collections" element={<Collections/>}/>
                    <Route path = "/access-denied" element={<AccessDenied/>}/>
                </Routes>
                <Footer/>
            </Container>
        </Router>
    )
}

export default App
