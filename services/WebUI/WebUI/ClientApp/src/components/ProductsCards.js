import React from 'react';
import {Card, Image, Label} from "semantic-ui-react";

const ProductCard = ({name, description, price, imageFile, category}) => {
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
      <Card.Content extra textAlign='center'>
        <Label color='blue'>{category}</Label>
      </Card.Content>
    </Card>
  );
};

const ProductsCards = ({products}) => {
  return (
    <div style={{marginBottom: '30px'}}>
      <Card.Group itemsPerRow='3' doubling centered stackable >
        {products.map(product => <ProductCard key={product.id} {...product} />)}
      </Card.Group>
    </div>
  );
};

export default ProductsCards;