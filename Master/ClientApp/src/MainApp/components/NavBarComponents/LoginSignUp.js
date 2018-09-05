import React from "react";
import Popup from "reactjs-popup";

import { LoginPopOut } from "./LoginPopOut";
import { SignUpPopOut } from "./SignUpPopOut";

const PopOutBaseStyle = {
    width: "20%",
    height: "auto",

    borderRadius: "15px",
    borderStyle: "solid"
};

function LoginSignUp() {

    return(
        <section className="con-auth">

            <Popup trigger={ <button className="login-popup-btn">Login</button> }
                   modal
                   contentStyle={ PopOutBaseStyle }
                   closeOnEscape
                   closeOnDocumentClick>
                <LoginPopOut />
            </Popup>

            <Popup trigger={ <button className="signup-popup-btn">Sign Up</button> }
                   modal
                   contentStyle={ PopOutBaseStyle }
                   closeOnEscape
                   closeOnDocumentClick>
                <SignUpPopOut />
            </Popup>

        </section>
    );
};

export default LoginSignUp;