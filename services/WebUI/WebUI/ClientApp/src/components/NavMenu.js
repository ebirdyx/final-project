import React from 'react';
import {Icon, Label, Menu} from "semantic-ui-react";
import {Link} from "react-router-dom";
import {AuthenticationButton} from "./Auth";

const NavMenu = () => {
  return (
    <Menu stackable>
      <Menu.Item as={Link} to='/'>
        <img alt="logo" src='/logo.png' />
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
        <AuthenticationButton />
      </Menu.Item>
      
      <Menu.Item
        name='cart'
        position='right'
        as={Link}
        to='/cart'
      >
        <Label color='green' >
          <Icon name='shopping cart' size='big'/>
          2
        </Label>
      </Menu.Item>
    </Menu>
  );
}

export default NavMenu;