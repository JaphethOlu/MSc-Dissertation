import { authentication } from "../authenticationReducer";
import { contractorActionTypes } from "../../actionTypes";
import { expect } from "chai";

describe("authentication reducer", () => {

    let user = {
        "account": "bourneCoder@example.com",
        "token": "randomJSONwebToken",
        "role": "contractor"
    };

    let loginErrorString = "Invalid login credentials";

    let signupErrorString = "There was a problem creating your account";
    
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
        let expectedState = { authenticating: false, error: loginErrorString };
        let action = { type: contractorActionTypes.LOGIN_ERROR, authenticating: false, error: loginErrorString };
        expect(authentication([], action)).to.deep.equal(expectedState);
    });

    test("should handle SIGNUP_REQUEST", () => {
        let expectedState = { authenticating: true };
        let action = { type: contractorActionTypes.SIGNUP_REQUEST, authenticating: true };
        expect(authentication(undefined, action)).to.deep.equal(expectedState);
    });

    test("should handle SIGNUP_SUCCESS", () => {
        let expectedState = { authenticating: false, user: user, authenticated: true };
        let action = { type: contractorActionTypes.SIGNUP_SUCCESS, authenticating: false, user: user };
        expect(authentication([], action)).to.deep.equal(expectedState);
    });

    test("should handle SIGNUP_ERROR", () => {
        let expectedState = { authenticating: false, error: signupErrorString };
        let action = { type: contractorActionTypes.SIGNUP_ERROR, authenticating: false, error: signupErrorString };
        expect(authentication([], action)).to.deep.equal(expectedState);
    });

});