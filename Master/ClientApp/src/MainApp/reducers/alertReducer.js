import { alertActionTypes } from "../actionTypes";

export function alert(state = {}, action) {
    switch (action.type) {
        case alertActionTypes.SUCCESS:
            return Object.assign({}, state, { 
                message: action.message
            });
        case alertActionTypes.ERROR:
            return Object.assign({}, state, {
                message: action.message
            });
        case alertActionTypes.CLEAR:
            return Object.assign({}, {});
        default:
            return state;
    }
}