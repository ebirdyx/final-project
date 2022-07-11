import React from 'react';
import {Container} from 'semantic-ui-react';
import NavMenu from './NavMenu';

const Layout = ({children}) => {
  return (
    <div>
      <NavMenu/>
      <Container>
        {children}
      </Container>
    </div>
  );
};

export default Layout;
