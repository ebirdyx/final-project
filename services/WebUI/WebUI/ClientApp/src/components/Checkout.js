import React, {useEffect, useState} from 'react';
import {Form, Header} from "semantic-ui-react";
import CartService from "../services/cart";
import {useHistory} from "react-router-dom";
import {useAuth0} from "@auth0/auth0-react";
import {useDispatch, useSelector} from "react-redux";
import {updateCart} from "../store";

const options = [
  { key: 'm', text: 'Male', value: 'male' },
  { key: 'f', text: 'Female', value: 'female' },
];

const Checkout = () => {
  const [formValues, setFormValues] = useState({});
  const [formValid, setFormValid] = useState(false);
  const totalPrice = useSelector(s => s.cart.totalPrice);
  const {user} = useAuth0();
  const {email} = user;
  const history = useHistory();
  const dispatch = useDispatch();
  
  const formOnChange = (e, o) => {
    setFormValues({
      ...formValues,
      [o.id]: o.value,
      userName: email,
      emailAddress: email,
      paymentMethod: 1,
      totalPrice: totalPrice
    });
  };
  
  const onSubmit = async () => {
    await CartService.checkout(formValues);
    dispatch(updateCart({totalPrice: 0, items: [], userName: email}));
    history.push('/order-completed')
  };
  
  useEffect(() => {
    setFormValid(Object.keys(formValues).length === 14);
  }, [formValues]);
  
  return (
    <Form onSubmit={onSubmit}>
      <Header as='h1'>Order information</Header>
      
      <Header as='h2'>Personal information</Header>
      <Form.Group widths='equal'>
        <Form.Input id='firstName' onChange={formOnChange} fluid label='First name' placeholder='First name' />
        <Form.Input id='lastName' onChange={formOnChange} fluid label='Last name' placeholder='Last name' />
        <Form.Select fluid label='Gender' placeholder='Gender' options={options} />
      </Form.Group>
      
      <Header as='h2'>Address information</Header>
      <Form.Group widths='equal'>
        <Form.Input id='addressLine' onChange={formOnChange} fluid label='Address line' placeholder='Address line' />
        <Form.Input id='state' onChange={formOnChange} fluid label='State' placeholder='State' />
        <Form.Input id='country' onChange={formOnChange} fluid label='Country' placeholder='Country' />
        <Form.Input id='zipCode' onChange={formOnChange} fluid label='ZipCode' placeholder='ZipCode' />
      </Form.Group>
      
      <Header as='h2'>Billing information</Header>
      <Form.Group widths='equal'>
        <Form.Input id='cardName' onChange={formOnChange} fluid label='Name on card' placeholder='John Doe' />
        <Form.Input id='cardNumber' onChange={formOnChange} fluid label='Card number' placeholder='Card number' />
        <Form.Input id='expiration' onChange={formOnChange} fluid label='Expiration' placeholder='YYMM' />
        <Form.Input id='cvv' onChange={formOnChange} fluid label='CVV' placeholder='CVV' />
      </Form.Group>
      
      <div style={{marginTop: '40px'}}></div>
      <Form.Button disabled={!formValid} style={{float: 'right'}} primary>Submit</Form.Button>
    </Form>
  );
}

export default Checkout;