import React from 'react';
import {Menu} from "semantic-ui-react";

const NavMenu = () => {
  return (
    <Menu stackable>
      <Menu.Item>
        <img alt="logo" src='/logo.png' />
      </Menu.Item>
      
      <Menu.Item
        name='E-Commerce'
        position='left'
      >
        E-Commerce
      </Menu.Item>
    </Menu>
  );
}

export default NavMenu;