import React from "react";
import { hot } from "react-hot-loader";

import Routes from "./Routes";

class App extends React.Component {
    render () {
        return (
            <div>
                <h1>Hello From ReactDOM</h1>
                <Routes />
            </div>
        );
    }
};

export default hot(module)(App);