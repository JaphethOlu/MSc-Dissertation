import { alertActionTypes } from "../actionTypes";

export function alert(state = {}, action) {
    switch (action.type) {
        case alertActionTypes.SUCCESS:
            return {
                message: action.message
            };
        case alertActionTypes.ERROR:
            return {
                message: action.message
            };
        case alertActionTypes.CLEAR:
            return {};
        default:
            return state;
    }
}