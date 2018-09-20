import { topOrganisations } from "../topOrganisationsReducer";
import { topOrganisationsActionTypes } from "../../actionTypes";
import { expect } from "chai";

describe("topOrganisations reducer", () => {

    let topAgenciesResponse = [];
    let topEmployersResponse = [];

    let errorAgencyString = "Unable to retrieve top agencies";
    let errorEmployerString = "Unable to retrieve top employers";

    test("Should return initial state", () => {
        let expectedState = {
            loadingTopAgencies: false, loadingTopEmployers: false,
            TopAgencies: [], TopEmployers: []
        };

        expect(topOrganisations(undefined, {})).to.deep.equal(expectedState);
    });

    test("should handle TOP_AGENCY_REQUEST", () => {
        let expectedState = {
            loadingTopAgencies: true, loadingTopEmployers: false,
            TopAgencies: [], TopEmployers: []
        };

        let action = { type: topOrganisationsActionTypes.TOP_AGENCIES_REQUEST, loading: true };

        let state = {
            loadingTopAgencies: false, loadingTopEmployers: false,
            TopAgencies: [], TopEmployers: []
        };

        expect(topOrganisations(state, action)).to.deep.equal(expectedState);
    });

    test("should handle TOP_EMPLOYER_REQUEST", () => {
        let expectedState = {
            loadingTopAgencies: false, loadingTopEmployers: true,
            TopAgencies: [], TopEmployers: []
        };

        let action = { type: topOrganisationsActionTypes.TOP_EMPLOYERS_REQUEST, loading: true };

        let state = {
            loadingTopAgencies: false, loadingTopEmployers: false,
            TopAgencies: [], TopEmployers: []
        };

        expect(topOrganisations(state, action)).to.deep.equal(expectedState);
    });

    test("should handle TOP_AGENCY_SUCCESS", () => {
        let expectedState = {
            loadingTopAgencies: false, loadingTopEmployers: true,
            TopAgencies: topAgenciesResponse, TopEmployers: []
        };

        let action = {
            type: topOrganisationsActionTypes.TOP_AGENCIES_SUCCESS, loading: false, TopAgencies: topAgenciesResponse
        };

        let state = {
            loadingTopAgencies: true, loadingTopEmployers: true,
            TopAgencies: [], TopEmployers: []
        };

        expect(topOrganisations(state, action)).to.deep.equal(expectedState);
    });

    test("should handle TOP_EMPLOYER_SUCCESS", () => {
        let expectedState = {
            loadingTopAgencies: true, loadingTopEmployers: false,
            TopAgencies: [], TopEmployers: topEmployersResponse
        };

        let action = {
            type: topOrganisationsActionTypes.TOP_EMPLOYERS_SUCCESS, loading: false, TopEmployers: topEmployersResponse
        };

        let state = {
            loadingTopAgencies: true, loadingTopEmployers: true,
            TopAgencies: [], TopEmployers: []
        };

        expect(topOrganisations(state, action)).to.deep.equal(expectedState);
    });

    test("should handle TOP_AGENCY_ERROR", () => {
        let expectedState = {
            loadingTopAgencies: false, loadingTopEmployers: true,
            TopAgencies: [], TopEmployers: [], error: errorAgencyString
        };

        let action = { 
            type: topOrganisationsActionTypes.TOP_AGENCIES_ERROR, loading: false, error: errorAgencyString
        };

        let state = {
            loadingTopAgencies: true, loadingTopEmployers: true,
            TopAgencies: [], TopEmployers: [],
        };

        expect(topOrganisations(state, action)).to.deep.equal(expectedState);
    });

    test("should handle TOP_EMPLOYER_ERROR", () => {
        let expectedState = {
            loadingTopAgencies: true, loadingTopEmployers: false,
            TopAgencies: [], TopEmployers: [], error: errorEmployerString
        };

        let action = {
            type: topOrganisationsActionTypes.TOP_EMPLOYERS_ERROR, loading: false, error: errorEmployerString
        };

        let state = {
            loadingTopAgencies: true, loadingTopEmployers: true,
            TopAgencies: [], TopEmployers: []
        };

        expect(topOrganisations(state, action)).to.deep.equal(expectedState);
    });

});