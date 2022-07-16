import React from 'react';
import {Button, Icon, Label, Menu} from "semantic-ui-react";
import {Link} from "react-router-dom";
import {AuthenticationButton} from "./Auth";
import {useSelector} from "react-redux";
import {useAuth0} from "@auth0/auth0-react";

const NavMenu = () => {
  const {isAuthenticated} = useAuth0();
  const cart = useSelector(s => s.cart);

  return (
    <Menu stackable>
      <Menu.Item as={Link} to='/'>
        <img alt="logo" src='/logo.png'/>
      </Menu.Item>

      <Menu.Item
        name='E-Commerce'
        position='left'
        as={Link}
        to='/'
      >
        E-Commerce
      </Menu.Item>

      <Menu.Item
        name='login-button'
        position='right'
      >
        <AuthenticationButton/>
      </Menu.Item>

      {isAuthenticated
        &&
        <Menu.Item
          name='Profile'
          position='right'
          as={Link}
          to='/profile'
        >
          <Button>Profile</Button>
        </Menu.Item>
      }

      <Menu.Item
        name='cart'
        position='right'
        as={Link}
        to='/cart'
      >
        <Label color='green'>
          <Icon name='shopping cart' size='big'/>
          {cart.items && cart.items.length}
        </Label>
      </Menu.Item>
    </Menu>
  );
}

export default NavMenu;