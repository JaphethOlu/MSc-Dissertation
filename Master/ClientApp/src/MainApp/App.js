import React from "react";
import { hot } from "react-hot-loader";

import NavigationBar from "./components/NavigationBar";
import Routes from "./Routes";

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