import { topOrganisationsActionTypes } from "../actionTypes";
import { topOrganisationsService } from "../services";
import { alertActions } from "./alertActions";

export const topOrganisationsActions = {
    getTopAgencies,
    getTopEmployers
};

/* eslint-disable no-unused-vars */
function getTopAgencies() {
    return dispatch => {
        dispatch(request());
        return topOrganisationsService.reqTopAgencies()
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
        return { type: topOrganisationsActionTypes.TOP_AGENCIES_REQUEST, loading: true };
    }

    function success(agencies) {
        return { type: topOrganisationsActionTypes.TOP_AGENCIES_SUCCESS, loading: false, TopAgencies: agencies };
    }

    function failure(error) {
        return { type: topOrganisationsActionTypes.TOP_AGENCIES_ERROR, loading: false, error };
    }
};

function getTopEmployers() {
    return dispatch => {
        dispatch(request());
        return topOrganisationsService.reqTopEmployers()
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
        return { type: topOrganisationsActionTypes.TOP_EMPLOYERS_REQUEST, loading: true };
    }

    function success(employers) {
        return { type: topOrganisationsActionTypes.TOP_EMPLOYERS_SUCCESS, loading: false, TopEmployers: employers };
    }

    function failure(error) {
        return { type: topOrganisationsActionTypes.TOP_EMPLOYERS_ERROR, loading: false, error };
    }
};