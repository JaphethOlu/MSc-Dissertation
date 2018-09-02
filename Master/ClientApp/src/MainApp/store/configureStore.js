import { applyMiddleware, createStore } from "redux";
import thunkMiddleware from "redux-thunk";
import { createLogger } from "redux-logger";
import { composeWithDevTools } from "redux-devtools-extension/developmentOnly";

const loggerMiddleware = createLogger();
const composeEnhancers = composeWithDevTools({});

import rootReducer from "../reducers/index";

const store = createStore(
    rootReducer,
    /* preloadedState, */
    composeEnhancers(
        applyMiddleware(
            thunkMiddleware,
            loggerMiddleware
        )
    )
);

export default store;