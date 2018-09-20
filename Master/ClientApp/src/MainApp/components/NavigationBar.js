import React from "react";

import JobSearchBar from "./NavBarComponents/JobSearchBar";
import LoginSignUp from "./NavBarComponents/LoginSignUp";

class NavigationBar extends React.Component {
    render() {
        return(
            <div className="nav-bar">
                <JobSearchBar />
                <section className="unauth-user">
                    <h1>Employers & Recruiters</h1>
                    <LoginSignUp />
                </section>
            </div>
        );
    }
};

export default NavigationBar;