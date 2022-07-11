import React from 'react';
import {Card, Image, Label} from "semantic-ui-react";

const ProductCard = ({name, description, price, imageFile}) => {
  return (
    <Card>
      <Image src={imageFile} wrapped ui={false} />
      <Card.Content>
        <Card.Header>{name}</Card.Header>
        <Card.Meta>
          <Label as='a' color='blue' ribbon>
            <span>$ {price}</span>
          </Label>
        </Card.Meta>
        <Card.Description>
          {description}
        </Card.Description>
      </Card.Content>
    </Card>
  );
};

const Content = ({products}) => {
  return (
    <div>
      <Card.Group itemsPerRow='3' doubling centered >
        {products.map(product => <ProductCard key={product.id} {...product} />)}
      </Card.Group>
    </div>
  );
};

export default Content;