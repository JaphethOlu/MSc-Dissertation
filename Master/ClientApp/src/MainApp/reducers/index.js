import { combineReducers } from "redux";
import { authentication } from "./authenticationReducer";
import { alert } from "./alertReducer";

//TODO: Asynchronous action reducer from redux site

const rootReducer = combineReducers({
    authentication,
    alert
});

export default rootReducer;