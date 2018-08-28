import React from "react";

class Login extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            EmailAddress: "",
            Password: ""
        };

        this.handleEmailAddress = this.handleEmailAddress.bind(this);
        this.handlePassword = this.handlePassword.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        
    }

    handleEmailAddress(event) {
        this.setState({ EmailAddress: event.target.value });
    }

    handlePassword(event) {
        this.setState({ Password: event.target.value });
    }

    handleSubmit(event) {
        alert("Login action should occur here");
        event.preventDefault();
    }

    render() {
        return(
            <form onSubmit={ this.handleSubmit }>
                <label>
                    EmailAddress:
                    <input type="text" placeholder="Email Address" 
                           value={ this.state.EmailAddress } onChange={ this.handleEmailAddress } />
                </label>

                <label>
                    Password:
                    <input type="password" placeholder="password" 
                           value={ this.state.Password } onChange={ this.handlePassword } />
                </label>

                <button>Login</button>

            </form>
        );
    }

};

export default Login;