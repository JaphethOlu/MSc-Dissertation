import { alert } from "../alertReducer";
import { alertActionTypes } from "../../actionTypes";
import { expect } from "chai";

describe("alert reducer", () => {
    
    let successMessage = "Test action successful";

    let errorMessage = "Test error message";

    test("should return an initial state", () => {
        let expectedState = {};

        expect(alert(undefined, {})).to.deep.equal(expectedState);
    });

    test("should return success action and success message", () => {
        let expectedState = { message: successMessage };
        let action = { type: alertActionTypes.SUCCESS, message: successMessage };

        expect(alert({}, action)).to.deep.equal(expectedState)
    });

    test("should return error action and error message", () => {
        let expectedState = { message: errorMessage };
        let action = { type: alertActionTypes.ERROR, message: errorMessage };

        expect(alert({}, action)).to.deep.equal(expectedState);
    });

    test("should clear state", () => {
        let expectedState = {};
        let action = { type: alertActionTypes.CLEAR };

        expect(alert({ message: errorMessage }, action)).to.deep.equal(expectedState);
    });

});