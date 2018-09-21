import React from "react";
import PropTypes from "prop-types";
import { connect } from "react-redux";

import { topOrganisationsActions } from "../../actions";

class TopOrganisationsBoard extends React.Component {
    constructor(props) {
        super(props);
        this.state = {};
    }

    componentDidMount() {
        const { dispatch } = this.props;
        dispatch(topOrganisationsActions.getTopAgencies());
        dispatch(topOrganisationsActions.getTopEmployers());
    }

    render() {
        return (
            <div className="top-org-list">
                <h1>Top Organisations List</h1>
            </div>
        );
    }
};

const mapStateToProps = state => {
    const { 
        loadingTopAgencies,
        loadingTopEmployers,
        TopAgencies,
        TopEmployers
    } = state.topOrganisations;
    
    return { 
        loadingTopAgencies,
        loadingTopEmployers,
        TopAgencies,
        TopEmployers
     };
};

TopOrganisationsBoard.propTypes = {
    loadingTopAgencies: PropTypes.bool.isRequired,
    loadingTopEmployers: PropTypes.bool.isRequired,
    TopAgencies: PropTypes.array,
    TopEmployers: PropTypes.array,
    dispatch: PropTypes.func.isRequired
};

const connectedTopOrganisationsBoard = connect(mapStateToProps)(TopOrganisationsBoard);

export { connectedTopOrganisationsBoard as TopOrganisationsBoard };
