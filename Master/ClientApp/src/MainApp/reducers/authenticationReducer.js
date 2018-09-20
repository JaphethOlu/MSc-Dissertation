import { contractorActionTypes } from "../actionTypes";

let user = localStorage.getItem("user");
const initialState = user ? { user, authenticating: false, authenticated: true } : { authenticating: false };

export function authentication (state = initialState, action) {
    switch (action.type) {
        case contractorActionTypes.LOGIN_REQUEST:
        case contractorActionTypes.SIGNUP_REQUEST:
            return Object.assign({}, state, {
                authenticating: action.authenticating
            });

        case contractorActionTypes.LOGIN_SUCCESS:
        case contractorActionTypes.SIGNUP_SUCCESS:
            return Object.assign({}, state, {
                authenticating: action.authenticating,
                user: action.user,
                authenticated: true
            });

        case contractorActionTypes.LOGIN_ERROR:
        case contractorActionTypes.SIGNUP_ERROR:
            return Object.assign({}, state, {
                authenticating: action.authenticating,
                error: action.error
            });

        case contractorActionTypes.LOGOUT:
            return Object.assign({}, state, {
                authenticating: action.authenticating,
                authenticated: false
            });

        default:
            return state;
    }
}