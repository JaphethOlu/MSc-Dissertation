import { expect } from "chai";
import sinon from "sinon";
import thunk from "redux-thunk";
import configureMockStore from "redux-mock-store";

import { alertActionTypes, contractorActionTypes } from "../../actionTypes";
import { contractorActions } from "../contractorActions";
import { authenticationService } from "../../services";

const middlewares = [thunk];
const mockStore = configureMockStore(middlewares);

describe("Contractor Actions", () => {
    let store;
    
    const sinonLoginReturn = {
        status: 202,
        body: {
            user: {
                "account": "bourneCoder@example.com",
                "token": "randomJSONwebToken",
                "role": "contractor"
            }
        }
    };

    const sinonSignUpReturn = {
        status: 200,
        body: {
            user: {
                "account": "bourneCoder@example.com",
                "token": "randomJSONwebToken",
                "role": "contractor"
            }
        }
    };

    const sinonUnauthenticatedReturn = {
        status: 401
    };

    const successUser = sinonLoginReturn.body.user;

    const signUpUser = {
        first: "John",
        last: "Doe"
    };

    beforeEach(() => {
        store = mockStore({});
    });

    afterEach(() => {
        sinon.restore();
    });

    test("It returns right action for authenticated true user", done => {
        let expectedActions = [
            { type: contractorActionTypes.LOGIN_REQUEST, authenticating: true },
            { type: contractorActionTypes.LOGIN_SUCCESS, authenticating: false, user: successUser }
        ];

        let fake = sinon.fake.resolves(sinonLoginReturn);

        sinon.replace(authenticationService, "login", fake);

        return store.dispatch(contractorActions.login("bourneCoder@example.com", "TestPassword"))
                    .then(() => {
                        let actions = store.getActions();
                        expect(actions[1].user).to.equal(expectedActions[1].user);
                        expect(actions).to.have.lengthOf(2);
                        expect(actions[1]).to.have.property("type");
                        expect(actions[1]).to.have.property("user");
                        expect(actions).to.deep.equal(expectedActions);
                        done();
                    });

    });

    test("Return right action for unauthenicated contractor", done => {
        let expectedActions = [
            { type: contractorActionTypes.LOGIN_REQUEST, authenticating: true },
            { type: contractorActionTypes.LOGIN_ERROR, authenticating: false, error: "Invalid login credentials" },
            { type: alertActionTypes.ERROR, message: "Invalid login credentials" }
        ];

        let fake = sinon.fake.resolves(sinonUnauthenticatedReturn);

        sinon.replace(authenticationService, "login", fake);

        return store.dispatch(contractorActions.login("bourneCoderexample.com", "ThisIsATestContractor"))
                    .then(() => {
                        let actions = store.getActions();
                        expect(actions).to.have.lengthOf(3);
                        expect(actions[1]).to.have.property("type").to.equal(expectedActions[1].type);
                        expect(actions[1]).to.have.property("error").to.equal(expectedActions[1].error);
                        expect(actions[2]).to.have.property("type").to.equal(alertActionTypes.ERROR);
                        expect(actions[2]).to.have.property("message").to.equal(expectedActions[2].message);
                        expect(actions).to.deep.equal(expectedActions);
                        done();
                    });

    });

    test("Return right action for successfully creating contractor account", done => {
        let expectedActions = [
            { type: contractorActionTypes.SIGNUP_REQUEST, authenticating: true },
            { type: contractorActionTypes.SIGNUP_SUCCESS, authenticating: false, user: successUser }
        ];

        let fake = sinon.fake.resolves(sinonSignUpReturn);

        sinon.replace(authenticationService, "signup", fake);

        return store.dispatch(contractorActions.signup("bourneCoderexample.com", "TestPassword", signUpUser))
                    .then(() => {
                        let actions = store.getActions();
                        expect(actions).to.have.lengthOf(2);
                        expect(actions[0]).to.have.property("type").to.equal(expectedActions[0].type);
                        expect(actions[1]).to.have.property("type").to.equal(expectedActions[1].type);
                        expect(actions).to.deep.equal(expectedActions);
                        done();
                    });

    });

    test("Return right action for successfully creating contractor account", done => {
        let expectedActions = [
            { type: contractorActionTypes.SIGNUP_REQUEST, authenticating: true },
            { type: contractorActionTypes.SIGNUP_ERROR, authenticating: false, error: "There was a problem creating your account" },
            { type: alertActionTypes.ERROR, message: "There was a problem creating your account" }
        ];

        let fake = sinon.fake.resolves(sinonUnauthenticatedReturn);

        sinon.replace(authenticationService, "signup", fake);

        return store.dispatch(contractorActions.signup("bourneCoderexample.com", "TestPassword", signUpUser))
                    .then(() => {
                        let actions = store.getActions();
                        expect(actions).to.have.lengthOf(3);
                        expect(actions[0]).to.have.property("type").to.equal(expectedActions[0].type);
                        expect(actions[1]).to.have.property("type").to.equal(expectedActions[1].type);
                        expect(actions).to.deep.equal(expectedActions);
                        done();
                    });

    });

});