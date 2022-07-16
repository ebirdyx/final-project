import React from 'react';
import {useAuth0} from "@auth0/auth0-react";
import {Button, Header, Icon, Input, Item, Label, Segment} from "semantic-ui-react";
import {shallowEqual, useDispatch, useSelector} from "react-redux";
import {updateCart} from "../store";

const Cart = () => {
  const cart = useSelector(s => s.cart);
  // const cart = useSelector(s => s.cart, shallowEqual);
  const dispatch = useDispatch();
  const {user} = useAuth0();
  const {email} = user;
  const haveItems = cart 
    && cart.items 
    && cart.items.length > 0;
  
  const updateQuantity = (id, updatedItem) => {
    let items = cart.items.filter(i => i.productId !== id)
    
    if (updatedItem.quantity > 0) {
      items.push(updatedItem);
    }
    
    dispatch(updateCart({...cart, items, userName: email}));
  }
  
  const incrementQuantity = (id) => {
    const item = cart.items.find(i => i.productId === id);
    const updatedItem = {
      "quantity": item.quantity+1,
      "price": item.price,
      "color": "",
      "productId": item.productId,
      "productName": item.productName,
    }
    updateQuantity(id, updatedItem);
  }

  const decrementQuantity = (id) => {
    const item = cart.items.find(i => i.productId === id);
    const updatedItem = {
      "quantity": item.quantity-1,
      "price": item.price,
      "color": "",
      "productId": item.productId,
      "productName": item.productName,
    }
    updateQuantity(id, updatedItem);
  }
  
  if (!haveItems)
    return (
      <Segment>
        <h1>Cart</h1>
        <div>
          <p style={{textAlign: 'center', marginTop: '30px'}}>Cart is empty</p>
        </div>
      </Segment>
    );
  
  return (
    <div>
      <Segment>
        <div style={{display: 'flex', justifyContent: 'space-between'}}>
          <Header as='h1'>Cart</Header>
          <Button color='facebook'>Checkout</Button>
        </div>

        <Item.Group divided >
          {cart.items.map(i => (
            <Item key={i.productId}>
              <Item.Image style={{margin: 'auto'}} size='tiny' src='http://placekitten.com/300/300' />

              <Item.Content>
                <Item.Header>Stevie Feliciano</Item.Header>

                <Item.Meta>
                  <Label size='small' color='teal'>$ {i.price}</Label>
                </Item.Meta>

                <Item.Extra>
                  <Button negative onClick={() => decrementQuantity(i.productId)} >
                    <Icon fitted name='minus' />
                  </Button>
                  <Input style={{width: '10%'}} disabled value={i.quantity} />
                  <Button positive onClick={() => incrementQuantity(i.productId)} >
                    <Icon fitted name='plus' />
                  </Button>

                  <Header floated='right'>
                    $ {i.price * i.quantity}
                  </Header>
                </Item.Extra>
              </Item.Content>
            </Item>
          ))}
        </Item.Group>
      </Segment>
      
      <div style={{textAlign: 'right'}}>
        <Segment style={{marginLeft: 'auto'}} compact >
          <Header>Total: $ {cart.totalPrice}</Header>
        </Segment>
      </div>
    </div>
  );
}

export default Cart;