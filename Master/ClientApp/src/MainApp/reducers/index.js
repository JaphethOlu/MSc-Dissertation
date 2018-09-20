import { combineReducers } from "redux";
import { authentication } from "./authenticationReducer";
import { topOrganisations } from "./topOrganisationsReducer";
import { alert } from "./alertReducer";

const rootReducer = combineReducers({
    alert,
    authentication,
    topOrganisations
});

export default rootReducer;