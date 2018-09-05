import { contractorActionTypes} from "../actionTypes";
import { authenticationService } from "../services";
import { alertActions } from "./alertActions";

export const contractorActions = {
    login,
    signup,
    logout
};

/* eslint-disable no-unused-vars */
function login(email, password) {
    return dispatch => {
        dispatch(request());
        let credentials = { EmailAddress: email, Password: password };
        return authenticationService.login(credentials)
                .then((res) => {
                    if(res.status == 202) {
                        dispatch(success(res.body.user));
                    } else {
                        throw new Error;
                    }
                })
                .catch((e) => {
                    let info = "Invalid login credentials";
                    dispatch(failure(info));
                    dispatch(alertActions.error(info));
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

function signup(email, password, firstName, lastName) {
    return dispatch => {
        dispatch(request());

        let account = { 
            EmailAddress: email,
            Password: password,
            FirstName: firstName,
            LastName: lastName
        };

        return authenticationService.signup(account)
                .then((res) => {
                    if(res.status == 200) {
                        dispatch(success(res.body.user));
                    } else {
                        throw new Error();
                    }
                })
                .catch((e) => {
                    let info = "There was a problem creating your account";
                    dispatch(failure(info));
                    dispatch(alertActions.error(info));
                });
    };

    function request() {
        return { type: contractorActionTypes.SIGNUP_REQUEST, authenticating: true };
    }

    function success(user) {
        return { type: contractorActionTypes.SIGNUP_SUCCESS, authenticating: false, user };
    }

    function failure(error) {
        return { type: contractorActionTypes.SIGNUP_ERROR, authenticating: false, error };
    }

};

function logout() {
    authenticationService.logout();
    return { type: contractorActionTypes.LOGOUT };
}