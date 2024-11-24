import {Button} from "semantic-ui-react";
import {useState} from "react";


const Authorization  = () => {
    const [auth, setAuth] = useState(false)
    const [userName, setUserName] = useState('Herashchanka Maksim')

    function Login() {
        setAuth(true);
    }

    function Register() {
        setAuth(true);
    }

    function LogOut() {
        setAuth(false);
    }

    return (
        <div>
            {!auth ?
                (
                    <div className='buttonContainer'>
                        <Button className= 'CustomNavButton'  onClick={Login}>LogIn</Button>
                        <Button className= 'CustomNavButton'  onClick={Register}>Register</Button>
                    </div>
                ) :
                (
                    <div className="buttonContainer">
                        <a href="/account" style={{alignContent:'center', marginLeft : 'auto'}}>{userName}</a>
                        <Button className= 'CustomNavButton' onClick={LogOut}>LogOut</Button>
                    </div>
                )}
        </div>
    )
};

export default Authorization;
