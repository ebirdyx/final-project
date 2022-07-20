import React from 'react';
import {useAuth0} from "@auth0/auth0-react";
import {Button, Header, Icon, Input, Item, Label, Segment} from "semantic-ui-react";
import {useDispatch, useSelector} from "react-redux";
import {updateCart} from "../store";
import NumberFormat from "react-number-format";
import {Link} from "react-router-dom";

const Currency = ({value}) => (
  <NumberFormat
    value={value}
    displayType='text'
    thousandSeparator={true}
    prefix='$'
    decimalScale={2}
    fixedDecimalScale={true}
  />
);

const Cart = () => {
  const cart = useSelector(s => s.cart);
  const sortedItems = (cart.items && cart.items.length > 0) ?
    cart.items.concat().sort((a, b) => a.productId > b.productId) : [];
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
  
  const handleRemoveItem = (id) => {
    let items = cart.items.filter(i => i.productId !== id)
    dispatch(updateCart({...cart, items, userName: email}));
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
        <div style={{display: 'flex', justifyContent: 'space-between', alignItems: 'center'}}>
          <Header as='h1'>Cart</Header>
          <Button color='facebook' as={Link} to='/checkout' >Checkout</Button>
        </div>

        <Item.Group divided >
          {sortedItems.map(i => (
            <Item key={i.productId}>
              <Item.Image style={{margin: 'auto'}} size='tiny' src='https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fcdn.pixabay.com%2Fphoto%2F2013%2F07%2F12%2F18%2F21%2Fpackage-153360_960_720.png&f=1&nofb=1' />

              <Item.Content>
                <Item.Header>{i.productName}</Item.Header>

                <Item.Meta>
                  <Label size='small' color='teal'><Currency value={i.price} /></Label>
                  <Button onClick={() => handleRemoveItem(i.productId)} size='mini' inverted color='red'>Remove</Button>
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
                    <Currency value={i.price * i.quantity} />
                  </Header>
                </Item.Extra>
              </Item.Content>
            </Item>
          ))}
        </Item.Group>
      </Segment>
      
      <div style={{textAlign: 'right'}}>
        <Segment style={{marginLeft: 'auto'}} compact >
          <Header>Total: <Currency value={cart.totalPrice} /></Header>
        </Segment>
      </div>
    </div>
  );
}

export default Cart;