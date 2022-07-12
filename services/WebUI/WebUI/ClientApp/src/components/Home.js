import React, {useEffect, useState} from 'react';
import SideMenu from "./SideMenu";
import ProductsCards from "./ProductsCards";
import CatalogService from "../services/catalog";
import {Grid, Icon, Input, Pagination, Select} from "semantic-ui-react";

const searchOptions = [
  { key: 'products', text: 'Products', value: 'products' },
  { key: 'categories', text: 'Categories', value: 'categories' },
]

const pageNumberItemsOptions = [
  { key: '12', text: '12', value: 12 },
  { key: '24', text: '24', value: 24 },
  { key: '36', text: '36', value: 36 },
]

const Home = () => {
  const [products, setProducts] = useState([]);
  const [showProducts, setShowProducts] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [productsPerPage, setProductsPerPage] = useState(12);
  const totalPages = Math.ceil(products.length / productsPerPage);
  
  const paginationOnChange = (_, { activePage }) => {
    setCurrentPage(activePage);
  };
  
  const productsPerPageOnChange = (_, {value}) => {
    setProductsPerPage(value);
  }
  
  useEffect(() => {
    CatalogService.getAll()
      .then(res => setProducts(res));
  }, []);

  useEffect(() => {
    setShowProducts(products.slice((currentPage-1)*productsPerPage, productsPerPage*currentPage))
  }, [products, currentPage, productsPerPage]);

  return (
    <div>
      <Grid>
        <Grid.Column width={4} >
          <SideMenu/>
        </Grid.Column>
        
        <Grid.Column width={12} >
          <Grid columns={2}>
            <Grid.Column width={15}>
              <Input type='text' placeholder='Search...' action fluid >
                <input />
                <Select compact options={searchOptions} defaultValue='products' />
              </Input>
            </Grid.Column>
            <Grid.Column width={1}>
              <Select
                compact
                options={pageNumberItemsOptions}
                defaultValue={12}
                onChange={productsPerPageOnChange}
              />
            </Grid.Column>
          </Grid>
          
          <ProductsCards products={showProducts} />
          
          <Pagination
            size='mini'
            totalPages={totalPages}
            activePage={currentPage}
            onPageChange={paginationOnChange}
            ellipsisItem={{ content: <Icon name='ellipsis horizontal' />, icon: true }}
            firstItem={{ content: <Icon name='angle double left' />, icon: true }}
            lastItem={{ content: <Icon name='angle double right' />, icon: true }}
            prevItem={{ content: <Icon name='angle left' />, icon: true }}
            nextItem={{ content: <Icon name='angle right' />, icon: true }}
          />
        </Grid.Column>
      </Grid>
    </div>
  );
}

export default Home;