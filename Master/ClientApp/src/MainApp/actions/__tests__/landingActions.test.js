import { expect } from "chai";
import sinon from "sinon";
import thunk from "redux-thunk";
import configureMockStore from "redux-mock-store";

import { alertActionTypes, landingActionTypes } from "../../actionTypes";
import { landingActions } from "../landingActions";
import { landingService } from "../../services";

const middlewares = [thunk];
const mockStore = configureMockStore(middlewares);


describe("Landing Component Actions", () => {
    let store;

    const sinonTopEmployersRes = {
        status: 200,
        body: [
            {
                "organisationId": 101109,
                "organisationName": "Hailey Employment Services",
                "organisationType": 1,
                "statement": "Sint sit elit et nostrud ullamco nisi sunt sit. Minim ea occaecat officia fugiat incididunt reprehenderit culpa minim ipsum do.",
                "location": "Manchester",
                "numberOfContracts": 49
            },
            {
                "organisationId": 101111,
                "organisationName": "You're Hired Services",
                "organisationType": 1,
                "statement": "Labore reprehenderit est consectetur magna do proident veniam. Magna dolor id ex nisi officia aliquip ea veniam sint eiusmod nulla.",
                "location": "Essex",
                "numberOfContracts": 40
            }
        ]
    };

    const sinonBadRequest = {
        status: 404
    };

    beforeEach(() => {
        store = mockStore({});
    });

    afterEach(() => {
        sinon.restore();
    });

    test("It returns a list of top employers", done => {

        let expectedActions = [
            { type: landingActionTypes.TOP_EMPLOYERS_REQUEST, loading: true },
            { type: landingActionTypes.TOP_EMPLOYERS_SUCCESS, loading: false, TopEmployers: sinonTopEmployersRes.body }
        ];

        let fake = sinon.fake.resolves(sinonTopEmployersRes);

        sinon.replace(landingService, "reqTopEmployers", fake);

        return store.dispatch(landingActions.getTopEmployers())
                    .then(() => {
                        let actions = store.getActions();
                        expect(actions[1].TopEmployers).to.equal(expectedActions[1].TopEmployers);
                        expect(actions).to.deep.equal(expectedActions);
                        done();
                    });

    });

    test("Returns error on top employers request", done => {

        let errorMsg = "Unable to retrieve top employers";

        let expectedActions = [
            { type: landingActionTypes.TOP_EMPLOYERS_REQUEST, loading: true },
            { type: landingActionTypes.TOP_EMPLOYERS_ERROR, loading: false, error: errorMsg },
            { type: alertActionTypes.ERROR, message: errorMsg }
        ];

        let fake = sinon.fake.resolves(sinonBadRequest);

        sinon.replace(landingService, "reqTopEmployers", fake);

        return store.dispatch(landingActions.getTopEmployers())
                    .then(() => {
                        let actions = store.getActions();
                        expect(actions).to.deep.equal(expectedActions);
                        done();
                    });
                    
    });

    test("It returns a list of top agencies", done => {

        let expectedActions = [
            { type: landingActionTypes.TOP_AGENCIES_REQUEST, loading: true },
            { type: landingActionTypes.TOP_AGENCIES_SUCCESS, loading: false, TopAgencies: sinonTopEmployersRes.body }
        ];

        let fake = sinon.fake.resolves(sinonTopEmployersRes);

        sinon.replace(landingService, "reqTopAgencies", fake);

        return store.dispatch(landingActions.getTopAgencies())
                    .then(() => {
                        let actions = store.getActions();
                        expect(actions[1].TopAgencies).to.equal(expectedActions[1].TopAgencies);
                        expect(actions).to.deep.equal(expectedActions);
                        done();
                    });
    });

    test("Returns error on top agencies request", done => {

        let errorMsg = "Unable to retrieve top agencies";

        let expectedActions = [
            { type: landingActionTypes.TOP_AGENCIES_REQUEST, loading: true },
            { type: landingActionTypes.TOP_AGENCIES_ERROR, loading: false, error: errorMsg },
            { type: alertActionTypes.ERROR, message: errorMsg }
        ];

        let fake = sinon.fake.resolves(sinonBadRequest);

        sinon.replace(landingService, "reqTopAgencies", fake);

        return store.dispatch(landingActions.getTopAgencies())
                    .then(() => {
                        let actions = store.getActions();
                        expect(actions).to.deep.equal(expectedActions);
                        done();
                    });
    });

});