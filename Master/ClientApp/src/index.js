import React from "react";
import ReactDOM from "react-dom";
//import { Provider } from "react-redux";
//import store from "./store/configureStore";

import App from "./MainApp/App";

ReactDOM.render(
    /*<Provider>
        <App />
    </Provider>*/
    <App />, document.getElementById("app-container"));