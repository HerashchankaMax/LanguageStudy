import {Button} from "semantic-ui-react";
import {useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";


const Authorization  = () => {
    const [userName, setUserName] = useState('')
    const navigate = useNavigate();
    useEffect(() => {

        const token  = localStorage.getItem('authtoken');
        setUserName( localStorage.getItem('userName') || '');
        console.log(`got username ${userName}`)
        if(token){
            setUserName(userName);}
    }, []);
    useEffect(() => {
        setUserName( localStorage.getItem('userName') || '');
    }, [localStorage.getItem('userName')]);

    function Login() {
        navigate('/login');
    }

    function Register() {
        navigate('/register');
    }

    function LogOut() {
        localStorage.removeItem('authToken');
        localStorage.removeItem('userName');
        setUserName('');
        navigate('/');
    }

    return (
        <div>
            {userName.length == 0 ?
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
