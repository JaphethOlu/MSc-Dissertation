import React from "react";
import { hot } from "react-hot-loader";

import Routes from "./Routes";
import NavigationBar from "./components/NavigationBar";

class App extends React.Component {
    render () {
        return (
            <div>
                <NavigationBar />
                <Routes />
            </div>
        );
    }
};

export default hot(module)(App);