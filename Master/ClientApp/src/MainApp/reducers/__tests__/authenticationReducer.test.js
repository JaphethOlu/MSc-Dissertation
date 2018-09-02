import { authentication } from "../authenticationReducer";
import { contractorActionTypes } from "../../actionTypes";
import { expect } from "chai";

describe("authentication reducer", () => {

    let user = {
        "account": "bourneCoder@example.com",
        "token": "randomJSONwebToken",
        "role": "contractor"
    };

    let errorString = "Invalid login credentials";

    test("should return initial state", () => {
        let expectedState = { authenticating: false };

        expect(authentication(undefined, {})).to.deep.equal(expectedState);
    });

    test("should handle LOGIN_REQUEST", () => {
        let expectedState = { authenticating: true };
        let action = { type: contractorActionTypes.LOGIN_REQUEST, authenticating: true };
        expect(authentication(undefined, action)).to.deep.equal(expectedState);
    });

    test("should handle LOGIN_SUCCESS", () => {
        let expectedState = { authenticating: false, user: user, authenticated: true };
        let action = { type: contractorActionTypes.LOGIN_SUCCESS, authenticating: false, user: user };
        expect(authentication([], action)).to.deep.equal(expectedState);
    });

    test("should handle LOGIN_ERROR", () => {
        let expectedState = { authenticating: false, error: errorString };
        let action = { type: contractorActionTypes.LOGIN_ERROR, authenticating: false, error: errorString };
        expect(authentication([], action)).to.deep.equal(expectedState);
    });

});