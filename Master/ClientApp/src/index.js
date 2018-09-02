import React from "react";
import { render } from "react-dom";
import { Provider } from "react-redux";
import store from "./MainApp/store/configureStore";

import App from "./MainApp/App";

render(
<Provider store={ store }>
    <App />
</Provider>, document.getElementById("app-container"));