import React, {FunctionComponent, useEffect, useState} from 'react';
import {Button, Form, Grid, Header, Message, Segment} from "semantic-ui-react";
import axios from "axios";
import {useNavigate} from "react-router-dom";


const Register: FunctionComponent = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmedPassword, setConfirmedPassword] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    const [isFormValid, setIsFormValid] = useState(false);
    const [registrationResponse, setRegistrationResponse] = useState('');

    const [isPasswordConfirmationError, setPasswordConfirmationError] = useState('');
    const [firstNameError, setFirstNameError] = useState('');
    const [lastNameError, setLastNameError] = useState('');
    const [emailError, setEmailError] = useState('');
    const navigate = useNavigate();

    useEffect(() => {
        if(!firstName){setFirstNameError('First Name is required')}else {setFirstNameError('')}
        if(!lastName){setLastNameError('Last Name is required')}else{setLastNameError('')}
        if(!email){setEmailError('Email is required')}else{setEmailError('')}
        if(!password){setPasswordConfirmationError('Password is required')}else {setPasswordConfirmationError('')}

        if (password !== confirmedPassword) {
            setPasswordConfirmationError('Passwords do not match');
        } else {
            setPasswordConfirmationError('');
        }
        if (email && password && firstName && lastName && confirmedPassword) {
            setIsFormValid(true);
        } else {
            setIsFormValid(false);
        }
    }, [email, password, firstName, lastName,confirmedPassword]);

    async function handleRegistration() {
        const request = {
            Email : email,
            Password : password,
            FirstName : firstName,
            LastName : lastName
        }
        try{
        const response = await axios.post('http://localhost:5000/api/account/register', request)
        if(response.status === 200) {
            navigate('/login');
        }
        else {
            console.log(response.data);
            setRegistrationResponse(`Registration failed: ${response.data}`);
        }}
        catch (e) {
            if(axios.isAxiosError(e)) {
                console.log(e.response?.data);
                setRegistrationResponse(`Registration failed: ${e.response?.data}`);
            }
            else {
                console.log(e);
                setRegistrationResponse(`Registration failed: ${e}`);
            }
        }
    }

    return (
      <Grid>
          <Grid.Column className='login-column'>
                <Header as='h2' className='login-header'>
                    Register
                </Header>
                <Form size='large' className='login-form' onSubmit={handleRegistration}>
                    <Segment className='login-segment'>
                        {registrationResponse && <Message negative>{registrationResponse}</Message>}
                        <Form.Input
                            fluid
                            icon='user'
                            iconPosition='left'
                            placeholder='First Name'
                            className='login-input'
                            onChange={(e)=> {setFirstName(e.target.value)}}
                        />
                        {firstNameError && <Message negative>{firstNameError}</Message>}
                        <Form.Input
                        fluid
                        icon='user'
                        iconPosition='left'
                        placeholder='Last Name'
                        className={'login-input'}
                        onChange = {(e)=> setLastName(e.target.value)}
                        />
                        {lastNameError && <Message negative>{lastNameError}</Message>}
                        <Form.Input
                            fluid
                            icon='envelope'
                            iconPosition='left'
                            placeholder='E-mail address'
                            type='email'
                            className='login-input'
                            onChange={(e) => setEmail(e.target.value)}
                        />
                        {emailError && <Message negative>{emailError}</Message>}
                        <Form.Input
                            fluid
                            icon='lock'
                            iconPosition='left'
                            placeholder='Password'
                            type='password'
                            className='login-input'
                            onChange={(e) => setPassword(e.target.value)}
                        />
                        <Form.Input
                            fluid
                            icon='lock'
                            iconPosition='left'
                            placeholder='Confirm Password'
                            type='password'
                            className='login-input'
                            onChange={(e) => setConfirmedPassword(e.target.value)}
                        />
                        {isPasswordConfirmationError && <Message negative>{isPasswordConfirmationError}</Message>}
                        <Button className='login-button' fluid size='large' disabled={!isFormValid}>
                            Register
                        </Button>
                    </Segment>
                </Form>
          </Grid.Column>
      </Grid>
  );
};

export default Register;