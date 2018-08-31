import { alertActionTypes } from "../actionTypes";

export const alertActions = {
    success,
    error,
    clear
};

function success(message) {
    return { type: alertActionTypes.SUCCESS, message };
};

function error(message) {
    return { type: alertActionTypes.ERROR, message };
};

function clear() {
    return { type: alertActionTypes.CLEAR };
};
