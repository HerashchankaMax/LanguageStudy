import React, { useState } from 'react';
import { Button, Form, Grid, Header, Segment } from 'semantic-ui-react';
import '../../mystyle.css';
import {useNavigate} from "react-router-dom";
import axios from "axios";
import axiosInstance from "../../axiosInstance.ts";

const LogIn: React.FC = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();

    const handleSubmit = async (e:React.FormEvent) => {
        e.preventDefault();
        console.log('Email:', email);
        console.log('Password:', password);
        try{
        const response =
            await axiosInstance.post('http://localhost:5000/api/account/login',
                {
            Email : email,
            Password : password
        });
            if(response.status == 200 ){
                console.log(response.data.email);
                localStorage.setItem('userName', response.data.email);
                localStorage.setItem('authToken', response.data.token);
                navigate('/collections');
            }
            else {
                alert('Invalid email or password');
            }
        }
        catch (e) {
            if(axios.isAxiosError(e)){
                alert(`${e.response?.data}`);

            }
            else {
                alert(e);
            }
        }

    };

    return (
        <Grid className='login-container'>
            <Grid.Column className='login-column'>
                <Header as='h2' className='login-header'>
                    Log-in to your account
                </Header>
                <Form size='large' className='login-form' onSubmit={handleSubmit}>
                    <Segment className='login-segment'>
                        <Form.Input
                            fluid
                            icon='user'
                            iconPosition='left'
                            placeholder='E-mail address'
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            className='login-input'
                        />
                        <Form.Input
                            fluid
                            icon='lock'
                            iconPosition='left'
                            placeholder='Password'
                            type='password'
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                            className='login-input'
                        />
                        <Button className='login-button' fluid size='large'>
                            Login
                        </Button>
                    </Segment>
                </Form>
            </Grid.Column>
        </Grid>
    );
};

export default LogIn;