import { expect } from "chai";

import { alertActionTypes } from "../../actionTypes";
import { alertActions } from "../alertActions";

describe("Alert Actions", () => {
    
    let successMessage = "Test action successful";

    let errorMessage = "Test error message";

    test("Should create an action for alert success", () => {
        let expectedActions = { type: alertActionTypes.SUCCESS, message: successMessage };

        expect(alertActions.success(successMessage)).to.deep.equal(expectedActions);
    });

    test("Should create an action for alert error", () => {
        let expectedActions = { type: alertActionTypes.ERROR, message: errorMessage };

        expect(alertActions.error(errorMessage)).to.deep.equal(expectedActions);
    });

    test("Should create an action for alert clear", () => {
        let expectedActions = { type: alertActionTypes.CLEAR };

        expect(alertActions.clear()).to.deep.equal(expectedActions);
    });

});