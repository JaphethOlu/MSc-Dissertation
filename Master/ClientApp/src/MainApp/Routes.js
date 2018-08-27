import React from "react";
import { Router, Route, Link } from "react-router-dom";
import createBrowserHistory from "history/createBrowserHistory";
const history = createBrowserHistory();

function Routes() {
    return(
        <Router history ={ history }>
        </Router>
    )
};

export default Routes;