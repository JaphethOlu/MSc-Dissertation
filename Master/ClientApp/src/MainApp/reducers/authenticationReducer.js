import { contractorActionTypes } from "../actionTypes";

let user = localStorage.getItem("user");
const initialState = user ? { user, authenticated: true } : {};

export function authentication (state = initialState, action) {
    switch (action.type) {
        case contractorActionTypes.LOGIN_REQUEST:
        case contractorActionTypes.SIGNUP_REQUEST:
        case contractorActionTypes.LOGOUT:
            return {};
        
        case contractorActionTypes.LOGIN_SUCCESS:
        case contractorActionTypes.SIGNUP_SUCCESS:
            return {
                user: action.user,
                authenticated: true
            };
        
        case contractorActionTypes.LOGIN_ERROR:
        case contractorActionTypes.SIGNUP_ERROR:
            return {
                error: action.error
            };
        
        default:
            return state;
    }
}