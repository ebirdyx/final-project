import React from 'react';
import {Route} from 'react-router-dom';
import Home from './components/Home';
import Cart from "./components/Cart";
import Layout from "./components/Layout";

const App = () => {
  return (
    <Layout>
      <Route exact path='/' component={Home}/>
      <Route path='/cart' component={Cart}/>
    </Layout>
  );
};

export default App;