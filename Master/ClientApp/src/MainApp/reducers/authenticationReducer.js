import { contractorConstants } from "../constants/contractorConstants";

let user = JSON.parse(localStorage.getItem("user"));
const initialState = user ? { user, authenticated: true } : {};

export function authenticationReducer (state = initialState, action) {
    switch (action.type) {
        case contractorConstants.LOGIN_REQUEST:
        case contractorConstants.SIGNUP_REQUEST:
        case contractorConstants.LOGOUT:
            return {};
        
        case contractorConstants.LOGIN_SUCCESS:
        case contractorConstants.SIGNUP_SUCCESS:
            return {
                user: action.user,
                authenticated: true
            };
        
        case contractorConstants.LOGIN_ERROR:
        case contractorConstants.SIGNUP_ERROR:
            return {
                error: action.error
            };
        
        default:
            return state;
    }
}