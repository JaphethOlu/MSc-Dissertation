import { topOrganisationsActionTypes } from "../actionTypes";

const initialState = { 
    loadingTopAgencies: false,
    loadingTopEmployers: false,
    TopAgencies: [],
    TopEmployers: []
};

export function topOrganisations (state = initialState, action) {
    switch (action.type) {
        case topOrganisationsActionTypes.TOP_AGENCIES_REQUEST:
            return Object.assign({}, state, {
                loadingTopAgencies: action.loading,
                loadingTopEmployers: state.loadingTopEmployers,
                TopAgencies: state.TopAgencies,
                TopEmployers: state.TopEmployers
            });

        case topOrganisationsActionTypes.TOP_EMPLOYERS_REQUEST:
            return Object.assign({}, state, {
                loadingTopAgencies: state.loadingTopAgencies,
                loadingTopEmployers: action.loading,
                TopAgencies: state.TopAgencies,
                TopEmployers: state.TopEmployers
            });

        case topOrganisationsActionTypes.TOP_AGENCIES_SUCCESS:
            return Object.assign({}, state, {
                loadingTopAgencies: action.loading,
                loadingTopEmployers: state.loadingTopEmployers,
                TopAgencies: action.TopAgencies,
                TopEmployers: state.TopEmployers
            });

        case topOrganisationsActionTypes.TOP_EMPLOYERS_SUCCESS:
            return Object.assign({}, state, {
                loadingTopAgencies: state.loadingTopAgencies,
                loadingTopEmployers: action.loading,
                TopAgencies: state.TopAgencies,
                TopEmployers: action.TopEmployers
            });

        case topOrganisationsActionTypes.TOP_AGENCIES_ERROR:
            return Object.assign({}, state, {
                loadingTopEmployers: state.loadingTopEmployers,
                loadingTopAgencies: action.loading,
                TopAgencies: state.TopAgencies,
                TopEmployers: state.TopEmployers,
                error: action.error
            });

        case topOrganisationsActionTypes.TOP_EMPLOYERS_ERROR:
            return Object.assign({}, state, {
                loadingTopAgencies: state.loadingTopAgencies,
                loadingTopEmployers: action.loading,
                TopAgencies: state.TopAgencies,
                TopEmployers: state.TopEmployers,
                error: action.error
            });

        default:
            return state;
    }
};