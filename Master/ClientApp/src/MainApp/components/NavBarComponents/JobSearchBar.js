import React from "react";

class JobSearchBar extends React.Component {
    render() {
        return (
            <form className="job-search-bar">
                <input type="text" placeholder="Search Jobs"></input>
                <button className="con-search-btn">Search</button>
            </form>
        );
    }
};

export default JobSearchBar;