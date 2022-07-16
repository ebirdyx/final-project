import React from 'react';
import {Header, Segment} from "semantic-ui-react";

const OrderCompleted = () => {
  return (
    <div>
      <Segment inverted color='green' textAlign='center' padded='very'>
        <Header size='large' style={{marginBottom: '20px'}}>Order completed</Header>
        Thank you for placing your order with us
      </Segment>
    </div>
  );
}

export default OrderCompleted;