import { contractorActionTypes } from "../actionTypes";
import { authenticationService } from "../services";
import { alertActions } from "./alertActions";

export const contractorActions = {
    login,
    logout
};

function login(email, password) {
    return dispatch => {
        dispatch(request());
        let credentials = { EmailAddress: email, Password: password };
        return authenticationService.login(credentials)
                .then((res) => {
                    if(res.status == 202) {
                        dispatch(success(res.body.user));
                    } else {
                        throw "Invalid login credentials";
                    }
                })
                .catch((error) => {
                    dispatch(failure(error));
                    dispatch(alertActions.error(error));
                });
    };

    function request() {
        return { type: contractorActionTypes.LOGIN_REQUEST, authenticating: true };
    }

    function success(user) {
        return { type: contractorActionTypes.LOGIN_SUCCESS, authenticating: false, user };
    }

    function failure(error) {
        return { type: contractorActionTypes.LOGIN_ERROR, authenticating: false, error };
    }
};

function logout() {
    authenticationService.logout();
    return { type: contractorActionTypes.LOGOUT };
}