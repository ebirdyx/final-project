import React, {useEffect, useState} from 'react';
import SideMenu from "./SideMenu";
import Content from "./Content";
import CatalogService from "../services/catalog";
import {Grid} from "semantic-ui-react";

const Home = () => {
  const [products, setProducts] = useState([]);
  
  useEffect(() => {
    CatalogService.getAll()
      .then(res => setProducts(res));
  }, []);

  return (
    <>
      <Grid>
        <Grid.Column width={4} >
          <SideMenu/>
        </Grid.Column>
        
        <Grid.Column width={12} >
          <Content products={products} />
        </Grid.Column>
      </Grid>
    </>
  );
}

export default Home;