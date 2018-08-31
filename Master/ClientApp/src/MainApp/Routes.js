import React from "react";
import { Router, Route, Link } from "react-router-dom";

import { history } from "./utilities";

function Routes() {
    return(
        <Router history ={ history }>
        </Router>
    )
};

export default Routes;