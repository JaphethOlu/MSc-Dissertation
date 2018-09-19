import request from "superagent";
import prefix from "superagent-prefix";

import { baseUrl, conLogin, conSignUp } from "./routes";

export const authenticationService = {
    login,
    signup,
    logout   
};

function login(userCredentials) {
    return request.post(conLogin)
                  .use(prefix(baseUrl))
                  .type("form")
                  .send(userCredentials)
                  .then((res) => {
                      storeResponse(res);
                      return res;
                  })
                  .catch((err) => {
                      console.log(err);
                  });
};

function signup(accountDetails) {
    return request.post(conSignUp)
                  .use(prefix(baseUrl))
                  .type("form")
                  .send(accountDetails)
                  .then((res) => {
                      storeResponse(res);
                      return res;
                  })
                  .catch((err) => {
                      console.log(err);
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