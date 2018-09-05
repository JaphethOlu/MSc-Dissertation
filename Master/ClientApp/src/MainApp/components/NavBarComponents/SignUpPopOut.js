import React from "react";
import PropTypes from "prop-types";
import { connect } from "react-redux";

import { contractorActions } from "../../actions/contractorActions";

class SignUpPopOut extends React.Component {
    constructor(props) {

        super(props);
        this.state = {
            email: "",
            password: "",
            repassword: "",
            firstName: "",
            lastName: ""
        };

        this.handleEmail = this.handleEmail.bind(this);
        this.handlePassword = this.handlePassword.bind(this);
        this.handleRePassword = this.handleRePassword.bind(this);
        this.handleFirstName = this.handleFirstName.bind(this);
        this.handleLastName = this.handleLastName.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);

    }

    handleEmail(event) {
        this.setState({ email: event.target.value });
    }

    handlePassword(event) {
        this.setState({ password: event.target.value });
    }

    handleRePassword(event) {
        this.setState({ repassword: event.target.value });
    }

    handleFirstName(event) {
        this.setState({ firstName: event.target.value });
    }

    handleLastName(event) {
        this.setState({ lastName: event.target.value });
    }

    handleSubmit(event) {
        event.preventDefault();
        const { email, password, repassword, firstName, lastName } = this.state;
        const { dispatch } = this.props;
        if(email && password && firstName && lastName && repassword) {
            dispatch(contractorActions.signup(email, password, firstName, lastName));
        }
    }

    render() {

        //const { authenicating } = this.props;
        const { email, firstName, lastName, password, repassword } = this.state;

        return(
            <form className="auth-form" onSubmit={ this.handleSubmit }>

                <label>
                    <h3> Email Address: </h3>
                    <input type="text" placeholder="example@email.com"
                           value={ email } onChange={ this.handleEmail } />
                </label>

                <label>
                    <h3> First Name: </h3>
                    <input type="text" placeholder="First Name"
                           value={ firstName } onChange={ this.handleFirstName } />
                </label>

                <label>
                    <h3> Last Name: </h3>
                    <input type="text" placeholder="Last Name"
                           value={ lastName } onChange={ this.handleLastName } />
                </label>

                <label>
                    <h3> Password: </h3>
                    <input type="password" placeholder="Password"
                           value={ password } onChange={ this.handlePassword } />
                </label>

                <label>
                    <h3> Re Type Password: </h3>
                    <input type="password" placeholder="Re-Type Password"
                           value={ repassword } onChange={ this.handleRePassword } />
                </label>


                <button className="auth-form-submit-btn">Sign Up</button>

                <div className="auth-form-alt">༼ つ ◕_◕ ༽つ <h2> Already have an account </h2></div>

            </form>
        );
    }

};

const mapStateToProps = state => {
    const { authenicating } = state.authentication;
    return {
        authenicating
    };
};

SignUpPopOut.propTypes = {
    authenticating: PropTypes.bool,
    dispatch: PropTypes.func
};

const connectedSignUp = connect(mapStateToProps)(SignUpPopOut);

export { connectedSignUp as SignUpPopOut };