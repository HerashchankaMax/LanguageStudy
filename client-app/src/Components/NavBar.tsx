import {Menu, Container, Button} from "semantic-ui-react";
import Authorization from "../Authorization.tsx";
export default function Navbar() {
    return (
            <Menu inverted  style = {{height : '60px'}}>
                <Container>
                    <Menu.Item header href="/" style = {{margin : '0', padding : '0'}} >
                        <img src="/assets/logo.png" alt="logo"
                             style={{marginRight: '5px',
                                 width: "100%",
                                 height : "100%",
                                 objectFit: "contain",
                                 position: "relative",
                             margin : '0px'}} />
                    </Menu.Item>
                    <Menu.Item name="LanguageLab"/>
                    <Menu.Item style = {{padding : '0'}}>
                        <Button className='CustomNavButton'>Games</Button>
                    </Menu.Item>
                    <Menu.Item style = {{padding : '0'}}>
                        <Button className='CustomNavButton'>My Study</Button>
                    </Menu.Item>
                    <div className=' right-container authorization-container'>
                        <Authorization />
                    </div>
                </Container>
            </Menu>
    );
}