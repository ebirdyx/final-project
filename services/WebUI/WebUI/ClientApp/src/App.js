import React from 'react';
import Home from './components/Home';
import Cart from "./components/Cart";
import Layout from "./components/Layout";
import {Profile, ProtectedRoute} from "./components/Auth";
import {useAuth0} from "@auth0/auth0-react";
import {Dimmer, Loader, Segment} from "semantic-ui-react";

const LoadingComponent = () => (
  <div>
    <Segment>
      <Dimmer>
        <Loader>Loading</Loader>
      </Dimmer>
    </Segment>
  </div>
)

const App = () => {
  const {isLoading} = useAuth0();

  if (isLoading)
    return <LoadingComponent/>;

  return (
    <Layout>
      <ProtectedRoute exact path='/' component={Home}/>
      <ProtectedRoute path='/cart' component={Cart}/>
      <ProtectedRoute path='/profile' component={Profile}/>
    </Layout>
  );
};

export default App;