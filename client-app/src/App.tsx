import Footer from "./Components/Footer.tsx";
import {Container} from "semantic-ui-react";
import NavBar from "./Components/NavBar.tsx";
import 'semantic-ui-css/semantic.min.css';
import './mystyle.css';
import Collections from "./Components/Collections.tsx";

function App() {
    return (
        <>
            <NavBar/>
            <Container style={{marginTop: '1em'}}>
                <Collections/>
                <Footer/>
            </Container>
        </>
    )
}

export default App
