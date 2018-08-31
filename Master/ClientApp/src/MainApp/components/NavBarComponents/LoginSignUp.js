import React from "react";
import Popup from "reactjs-popup";

import { Login } from "./Login";
import SignUp from "./SignUp";

function LoginSignUp() {
    return(
        <section className="con-auth">

            <Popup trigger={ <button className="con-login-btn">Login</button> }>
                <Login />
            </Popup>

            <Popup trigger={ <button className="con-signup-btn">Sign Up</button> }>
                <SignUp />
            </Popup>

        </section>
    );
};

export default LoginSignUp;