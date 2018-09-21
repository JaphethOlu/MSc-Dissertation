import React from "react";
import { Router, Route } from "react-router-dom";

import { history } from "./utilities";

import Home from "./components/Home";

function Routes() {
    return(
        <Router history ={ history }>
            <Route exact path="/" component={Home} />
        </Router>
    );
};

export default Routes;