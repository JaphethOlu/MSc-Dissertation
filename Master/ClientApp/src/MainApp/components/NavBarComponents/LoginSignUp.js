import React from "react";
import Popup from "reactjs-popup";

import { LoginPopOut } from "./LoginPopOut";
import { SignUpPopOut } from "./SignUpPopOut";

function LoginSignUp() {
    return(
        <section className="con-auth">

            <Popup trigger={ <button className="con-login-btn">Login</button> }>
                <LoginPopOut />
            </Popup>

            <Popup trigger={ <button className="con-signup-btn">Sign Up</button> }>
                <SignUpPopOut />
            </Popup>

        </section>
    );
};

export default LoginSignUp;