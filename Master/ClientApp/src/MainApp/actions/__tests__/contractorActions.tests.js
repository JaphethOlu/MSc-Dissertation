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
    
    const sinonAuthenticatedReturn = {
        status: 202,
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

    const successUser = sinonAuthenticatedReturn.body.user;    

    beforeEach(() => {
        store = mockStore({});
    });

    afterEach(() => {
        sinon.restore();
    });

    test("It returns right action for authenticated true user", done => {
        let expectedActions = [
            { type: contractorActionTypes.LOGIN_REQUEST },
            { type: contractorActionTypes.LOGIN_SUCCESS, user: successUser }
        ];

        let fake = sinon.fake.resolves(sinonAuthenticatedReturn);

        sinon.replace(authenticationService, "login", fake);

        return store.dispatch(contractorActions.login("bourneCoder@example.com", "ThisIsATestContractor"))
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
            { type: contractorActionTypes.LOGIN_REQUEST },
            { type: contractorActionTypes.LOGIN_ERROR, error: "Invalid login credentials" },
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
                        expect(actions[2]).to.have.property("message").to.equal(expectedActions[2].message)
                        expect(actions).to.deep.equal(expectedActions);
                        done();
                    });

    });

});