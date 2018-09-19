import request from "superagent";
import prefix from "superagent-prefix";

import { base, conLogin, conSignUp } from "./routes";

export const authenticationService = {
    login,
    signup,
    logout   
};

function login(userCredentials) {
    return request.post(conLogin)
                .use(prefix(base))
                .type("form")
                .send(userCredentials)
                .then((res) => {
                    storeResponse(res);
                    return res;
                });
};

function signup(accountDetails) {
    return request.post(conSignUp)
                .use(prefix(base))
                .type("form")
                .send(accountDetails)
                .then((res) => {
                    storeResponse(res);
                    return res;
                });
};

function logout() {
    localStorage.removeItem("user");
};

function storeResponse(response) {
    if(response.status === 202 || response.status === 200) {
        localStorage.setItem("user", response.body.user);
    }
};