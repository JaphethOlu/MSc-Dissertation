import { landingActionTypes } from "../actionTypes";
import { landingService } from "../services";
import { alertActions } from "./alertActions";

export const landingActions = {
    getTopAgencies,
    getTopEmployers
};

/* eslint-disable no-unused-vars */
function getTopAgencies() {
    return dispatch => {
        dispatch(request());
        return landingService.reqTopAgencies()
                    .then((res) => {
                        if(res.status == 200) {
                            dispatch(success(res.body));
                        } else {
                            throw new Error;
                        }
                    })
                    .catch((e) => {
                        let info = "Unable to retrieve top agencies";
                        dispatch(failure(info));
                        dispatch(alertActions.error(info));
                    });
    };

    function request() {
        return { type: landingActionTypes.TOP_AGENCIES_REQUEST, loading: true };
    }

    function success(agencies) {
        return { type: landingActionTypes.TOP_AGENCIES_SUCCESS, loading: false, TopAgencies: agencies };
    }

    function failure(error) {
        return { type: landingActionTypes.TOP_AGENCIES_ERROR, loading: false, error };
    }
};

function getTopEmployers() {
    return dispatch => {
        dispatch(request());
        return landingService.reqTopEmployers()
                    .then((res) => {
                        if(res.status == 200) {
                            dispatch(success(res.body));
                        } else {
                            throw new Error;
                        }
                    })
                    .catch((e) => {
                        let info = "Unable to retrieve top employers";
                        dispatch(failure(info));
                        dispatch(alertActions.error(info));
                    });
    };

    function request() {
        return { type: landingActionTypes.TOP_EMPLOYERS_REQUEST, loading: true };
    }

    function success(employers) {
        return { type: landingActionTypes.TOP_EMPLOYERS_SUCCESS, loading: false, TopEmployers: employers };
    }

    function failure(error) {
        return { type: landingActionTypes.TOP_EMPLOYERS_ERROR, loading: false, error };
    }
};