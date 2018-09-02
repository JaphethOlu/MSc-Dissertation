import { alertActionTypes } from "../actionTypes";

export const alertActions = {
    success,
    error,
    clear
};

function success(msg) {
    return { type: alertActionTypes.SUCCESS, message: msg };
};

function error(msg) {
    return { type: alertActionTypes.ERROR, message: msg };
};

function clear() {
    return { type: alertActionTypes.CLEAR };
};
