import React from "react";
import { connect } from "react-redux";

import { contractorActionsTypes } from "../../actionTypes";
import { contractorActions } from "../../actions/contractorActions";

class Login extends React.Component {
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
        return(
            <form onSubmit={ this.handleSubmit }>
                <label>
                    Email Address:
                    <input type="text" placeholder="Email Address"
                           value={ this.state.email } onChange={ this.handleEmail } />
                </label>

                <label>
                    Password:
                    <input type="password" placeholder="password"
                           value={ this.state.password } onChange={ this.handlePassword } />
                </label>

                <button>Login</button>

            </form>
        );
    }

};

const mapDispatchToProps = dispatch => {
    return {
        handleSubmit: () => dispatch({
            type: contractorActionsTypes.LOGIN_REQUEST
        })
    }
};

const connectedLogin = connect(mapDispatchToProps)(Login);

export { connectedLogin as Login };