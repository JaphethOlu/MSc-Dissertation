import request from "superagent";

// TODO: Development baseurl

export const authenticationService = {
    login,
    logout,
    signup
};

function login(userCredentials) {
    return request.post("http://localhost:55903/api/login/contractor")
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

function signup(userCredentials) {
    return request.post("http://localhost55903/api/register/contractor")
                  .type("form")
                  .send(userCredentials)
                  .then((res) => {
                      storeResponse(res);
                      return res;
                  })
                  .catch((err) => {
                      return err;
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