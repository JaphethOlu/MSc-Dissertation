import React from "react";

class SignUp extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            email: "",
            password: "",
            name: {
                first: "",
                last: "",
            }
        };

        this.handleEmail = this.handleEmail.bind(this);
        this.handlePassword = this.handlePassword.bind(this);
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

    handleFirstName(event) {
        this.setState({ name: { first: event.target.value }});
    }

    handleLastName(event) {
        this.setState({ name: { last: event.target.value }});
    }

    handleSubmit(event) {
        alert("Sign Up action should occur here");
        event.preventDefault();
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

                <label>
                    First Name:
                    <input type="text" placeholder="First Name" 
                           value={ this.state.name.first } onChange={ this.handleFirstName } />
                </label>

                <label>
                    Last Name:
                    <input type="text" placeholder="Last Name" 
                           value={ this.state.name.last } onChange={ this.handleLastName } />
                </label>

                <button>Sign Up</button>

            </form>
        );
    }

};

export default SignUp;