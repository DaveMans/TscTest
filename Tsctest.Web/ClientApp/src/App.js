import React, { Component, useState, useEffect } from 'react';
import { BrowserRouter, Switch, Route, NavLink } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import Login from './components/Login';
import { FetchCountryData } from './components/FetchCountryData';
import { Counter } from './components/Counter';
import axios from 'axios';
import PrivateRoute from './components/Utils/PrivateRoute';
import PublicRoute from './components/Utils/PublicRoute';
import { getToken, removeUserSession, setUserSession } from './components/Utils/Common';

import './custom.css'

function App() {
    let authLoading = useState(true);
    let setAuthLoading = useState(true);

    useEffect(() => {
        const token = getToken();
        if (!token) {
            return;
        }

        axios.post(`https://localhost:44313/login/validatecurrenttoken`, { token: token }).then(response => {
            setUserSession(response.data.token, "TestTsc");
            setAuthLoading(false);
        }).catch(error => {
            removeUserSession();
            setAuthLoading(false);
        });
    }, []);

    if (authLoading && getToken()) {
        return <div className="content">Checking Authentication...</div>
    }

    return (
        <div className="App">
            <BrowserRouter>
                <div>
                    <div className="header">
                        <NavLink exact activeClassName="active" to="/">Home</NavLink>
                        <NavLink activeClassName="active" to="/login">Login</NavLink><small>(Access without token only)</small>
                        <NavLink activeClassName="active" to="/fetchdata">Countries</NavLink><small>(Access with token only)</small>
                    </div>
                    <div className="content">
                        <Switch>
                            <Route exact path="/" component={Home} />
                            <PublicRoute path="/login" component={Login} />
                            <PrivateRoute path="/counter" component={Counter} />
                            <PrivateRoute path="/fetchdata" component={FetchCountryData} />
                        </Switch>
                    </div>
                </div>
            </BrowserRouter>
        </div>
    );
}

export default App;

