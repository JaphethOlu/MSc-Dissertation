import React from "react";
import Popup from "reactjs-popup";

import { LoginComponent } from "./LoginComponent";
import SignUpComponent from "./SignUpComponent";

function LoginSignUp() {
    return(
        <section className="con-auth">

            <Popup trigger={ <button className="con-login-btn">Login</button> }>
                <LoginComponent />
            </Popup>

            <Popup trigger={ <button className="con-signup-btn">Sign Up</button> }>
                <SignUpComponent />
            </Popup>

        </section>
    );
};

export default LoginSignUp;