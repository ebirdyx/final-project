import React, {useEffect, useState} from 'react';
import CatalogService from "../services/catalog";
import {Button, Card, Grid, Icon, Image, Input, Label, Menu, Pagination, Select} from "semantic-ui-react";
import {useDispatch, useSelector} from "react-redux";
import {updateCart} from "../store";
import {useAuth0} from "@auth0/auth0-react";

const searchOptions = [
  { key: 'products', text: 'Products', value: 'products' },
  { key: 'categories', text: 'Categories', value: 'categories' },
];

const pageNumberItemsOptions = [
  { key: '12', text: '12', value: 12 },
  { key: '24', text: '24', value: 24 },
  { key: '36', text: '36', value: 36 },
];

const ProductCard = ({id, name, description, price, imageFile, category}) => {
  const dispatch = useDispatch();
  const cart = useSelector(s => s.cart);
  const {user} = useAuth0();
  const {email} = user;
  const isProductInCart = cart.items 
    && cart.items.some(i => i.productId === id);
  
  const handleAddCard = (event) => {
    event.preventDefault();
    
    let items = [];
    if (cart.items.some(i => i.productId === id)) {
      const quantity = cart.items.find(i => i.productId === id).quantity;
      items = cart.items.filter(i => i.productId !== id)
      items.push({
        "quantity": quantity+1,
        "price": price,
        "color": "",
        "productId": id,
        "productName": name,
      });
    } else {
      items = [...cart.items];
      items.push({
        "quantity": 1,
        "price": price,
        "color": "",
        "productId": id,
        "productName": name,
      }); 
    }
    
    dispatch(updateCart({...cart, items, userName: email}));
  }
  
  const handleRemoveCard = (event) => {
    event.preventDefault();
    let items = cart.items.filter(i => i.productId !== id)
    dispatch(updateCart({...cart, items, userName: email}));
  }

  const styles = {
    display: 'flex',
    flexDirection: 'row',
    width: '100%',
    justifyContent: 'center',
    marginBottom: '10px'
  };
  
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
      <Card.Content extra textAlign='center' >
        <div style={styles}>
          <Label color='blue'>{category}</Label>
        </div>
        {isProductInCart
        ? <Button onClick={handleRemoveCard} color='red'>Remove from cart</Button>
        : <Button onClick={handleAddCard} color='green'>Add to cart</Button>
        }
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

const CategoriesMenu = ({products, categories, selectedCategory, categoryOnSelect}) => {
  const productsPerCategory = (category) => {
    return products.filter(p => p.category === category).length;
  };
  
  return (
    <Menu vertical>
      <Menu.Item
        name='all'
        active={selectedCategory === 'all'}
        onClick={categoryOnSelect}
      >
        <Label color='teal'>{products.length}</Label>
        <span style={{fontWeight: 'bold'}}>All</span>
      </Menu.Item>

      {categories.map(c =>
        <Menu.Item
          key={c} name={c}
          active={selectedCategory === c}
          onClick={categoryOnSelect}
        >
          <Label color='teal'>{productsPerCategory(c)}</Label>
          {c}
        </Menu.Item>
      )}
    </Menu>
  );
};

const ProductPagination = ({totalPages, currentPage, paginationOnChange}) => {
  return (
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
  );
};

const SearchTopMenu = ({onSearchChange, searchOption, searchOptionOnChange, productsPerPageOnChange}) => {
  return (
    <Grid columns={2}>
      <Grid.Column width={15}>
        <Input
          type='text'
          action fluid
          placeholder='Search...'
          onChange={onSearchChange}
        >
          <input />
          <Select
            compact
            options={searchOptions}
            defaultValue={searchOption}
            onChange={searchOptionOnChange}
          />
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
  );
};

const Home = () => {
  const [products, setProducts] = useState([]);
  const [categories, setCategories] = useState([]);
  const [selectedCategory, setSelectedCategory] = useState('all');
  const [showProducts, setShowProducts] = useState([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [productsPerPage, setProductsPerPage] = useState(12);
  const [searchOption, setSearchOption] = useState('products');
  const [searchTerm, setSearchTerm] = useState('');
  const [totalPages, setTotalPages] = useState(0);
  
  const paginationOnChange = (_, { activePage }) => {
    setCurrentPage(activePage);
  };
  
  const productsPerPageOnChange = (_, {value}) => {
    setProductsPerPage(value);
  };
  
  const searchOptionOnChange = (_, {value}) => {
    setSearchOption(value);
  };
  
  const categoryOnSelect = (_, {name}) => {
    setSelectedCategory(name);
  };
  
  const onSearchChange = (_, {value}) => {
    setSearchTerm(value);
  };
  
  useEffect(() => {
    CatalogService.getAll()
      .then(res => {
        setProducts(res);
        setCategories([...new Set(res.map(p => p.category))]);
      });
  }, []);

  useEffect(() => {
    const selectedProducts = products
      .filter(p => searchOption === 'products' 
        ? p.name.toLowerCase().includes(searchTerm) 
        : p.category.toLowerCase().includes(searchTerm.toLowerCase()))
      .filter(p => selectedCategory === 'all' ? true : p.category === selectedCategory);
    
    setShowProducts(selectedProducts
      .slice((currentPage-1)*productsPerPage, productsPerPage*currentPage))

    setTotalPages(
      Math.ceil(selectedProducts.length / productsPerPage));
  }, [products, currentPage, productsPerPage, selectedCategory, searchTerm, searchOption]);
  
  return (
    <div>
      <Grid>
        <Grid.Column width={4} >
          <CategoriesMenu
            products={products}
            categories={categories}
            selectedCategory={selectedCategory}
            categoryOnSelect={categoryOnSelect}
          />
        </Grid.Column>
        
        <Grid.Column width={12} >
          <SearchTopMenu
            onSearchChange={onSearchChange}
            searchOption={searchOption}
            searchOptionOnChange={searchOptionOnChange}
            productsPerPageOnChange={productsPerPageOnChange}
          />
          
          <ProductsCards products={showProducts} />

          {totalPages > 1 &&
            <ProductPagination
              totalPages={totalPages}
              currentPage={currentPage}
              paginationOnChange={paginationOnChange}
            />
          }
        </Grid.Column>
      </Grid>
    </div>
  );
};

export default Home;