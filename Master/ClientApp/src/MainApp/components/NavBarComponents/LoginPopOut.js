import React from "react";
import PropTypes from "prop-types";
import { connect } from "react-redux";

import { contractorActions } from "../../actions/contractorActions";

class LoginPopOut extends React.Component {
    constructor(props) {

        super(props);
        this.state = {
            email: "",
            password: ""
        };

        this.handleEmail = this.handleEmail.bind(this);
        this.handlePassword = this.handlePassword.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);

    }

    handleEmail(event) {
        this.setState({ email: event.target.value });
    }

    handlePassword(event) {
        this.setState({ password: event.target.value });
    }

    handleSubmit(event) {
        event.preventDefault();
        const { email, password } = this.state;
        const { dispatch } = this.props;
        if(email && password) {
            dispatch(contractorActions.login(email, password));
        }
    }

    render() {

        //const { authenticating } = this.props;
        const { email, password } = this.state;

        return(
            <form className="auth-form" onSubmit={ this.handleSubmit }>

                <label>
                    <h3> E-mail: </h3>
                    <input type="text" placeholder="example@email.com"
                           value={ email } onChange={ this.handleEmail } />
                </label>

                <label>
                    <h3> Password: </h3>
                    <input type="password" placeholder="password"
                           value={ password } onChange={ this.handlePassword } />
                </label>

                <button className="auth-form-submit-btn">Login</button>

                <div className="auth-form-alt"> <h2> Forgot Password? </h2>¯_(ツ)_/¯</div>

            </form>
        );
    }

};

const mapStateToProps = state => {
    const { authenticating, authenticated } = state.authentication;
    return { authenticating, authenticated };
};

LoginPopOut.propTypes = {
    authenticating: PropTypes.bool.isRequired,
    authenticated: PropTypes.bool.isRequired,
    dispatch: PropTypes.func.isRequired
};

const connectedLogin = connect(mapStateToProps)(LoginPopOut);

export { connectedLogin as LoginPopOut };