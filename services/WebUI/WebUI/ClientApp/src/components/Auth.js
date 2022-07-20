import React from 'react';
import {Auth0Provider, useAuth0, withAuthenticationRequired} from "@auth0/auth0-react";
import {Button, Dimmer, Form, Image, Label, Loader, Segment} from "semantic-ui-react";
import {Route, useHistory} from "react-router-dom";

export const AuthProvider = ({children}) => {
  const domain = "dev-wmjl5tm2.us.auth0.com";
  const clientId = "FMeJHSqsHUm6ajT8xypQjlEAgoQH7WHj";
  const redirectUri = window.location.origin;

  const history = useHistory();

  const onRedirectCallback = (appState) => {
    history.push(appState?.returnTo || window.location.pathname);
  };

  return (
    <Auth0Provider
      domain={domain}
      clientId={clientId}
      redirectUri={redirectUri}
      onRedirectCallback={onRedirectCallback}
    >
      {children}
    </Auth0Provider>
  );
};

const LoadingComponent = () => (
  <div>
    <Segment>
      <Dimmer>
        <Loader>Loading</Loader>
      </Dimmer>
    </Segment>
  </div>
)

export const ProtectedRoute = ({component, ...args}) => (
  <Route component={withAuthenticationRequired(component, {
    onRedirecting: () => <LoadingComponent />,
  })} {...args} />
);

export const Profile = () => {
  const { user } = useAuth0();
  const { name, picture, email, email_verified } = user;
  
  return (
    <div>
      <h1>Profile information</h1>

      <Image style={{marginTop: '10px', marginBottom: '10px'}} src={picture} alt='' />
      
      <Form>
        <Form.Field>
          <label>Name</label>
          <input disabled value={name} />
        </Form.Field>
        
        <Form.Field>
          <label>Email</label>
          <input disabled value={email} />
        </Form.Field>
        
        <Form.Field>
          <Label color={email_verified?"green":"red"}>
            {email_verified 
              ? "This email is verified" 
              : "This email is not verified"}
          </Label>
        </Form.Field>
        
        <Button style={{marginTop: '10px'}} disabled type='submit'>Save</Button>
      </Form>
    </div>
  );
};

const LoginButton = () => {
  const {loginWithRedirect} = useAuth0();

  return (
    <Button onClick={() => loginWithRedirect()}>
      Login
    </Button>
  );
};

const LogoutButton = () => {
  const {logout} = useAuth0();

  return (
    <Button onClick={() => logout({returnTo: window.location.origin})}>
      Logout
    </Button>
  );
};

export const AuthenticationButton = () => {
  const {isAuthenticated} = useAuth0();
  return isAuthenticated ? <LogoutButton/> : <LoginButton/>;
}
