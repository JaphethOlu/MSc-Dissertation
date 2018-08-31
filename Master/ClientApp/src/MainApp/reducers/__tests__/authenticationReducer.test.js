import { authentication } from "../authenticationReducer";
import { contractorActionTypes, alertActionTypes } from "../../actionTypes";
import { expect } from "chai";

describe("authentication reducer", () => {

    let user = {
        "account": "bourneCoder@example.com",
        "token": "randomJSONwebToken",
        "role": "contractor"
    };

    let errorString = "Invalid login credentials";

    test("should return initial state", () => {
        let expectedState = {};

        expect(authentication(undefined, {})).to.deep.equal(expectedState);
    });

    test("should handle LOGIN_REQUEST", () => {
        let expectedState = {};
        let action = { type: contractorActionTypes.LOGIN_REQUEST };
        expect(authentication(undefined, action)).to.deep.equal(expectedState);
    });

    test("should handle LOGIN_SUCCESS", () => {
        let expectedState = { user: user, authenticated: true };
        let action = { type: contractorActionTypes.LOGIN_SUCCESS, user: user };
        expect(authentication([], action)).to.deep.equal(expectedState);
    });

    test("should handle LOGIN_ERROR", () => {
        let expectedState = { error: errorString };
        let action = { type: contractorActionTypes.LOGIN_ERROR, error: errorString };
        expect(authentication([], action)).to.deep.equal(expectedState);
    });
});