import request from "superagent";

// TODO: Development baseurl

export const authenticationService = {
    login,
    signup,
    logout   
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

function signup(accountDetails) {
    return request.post("http://localhost:55903/api/register/contractor")
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