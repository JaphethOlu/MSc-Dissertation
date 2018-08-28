import React from "react";

class SignUp extends React.Component {
    constructor(props) {
        super(props);
        this.state = {
            EmailAddress: "",
            Password: "",
            FirstName: "",
            LastName: ""
        };

        this.handleEmailAddress = this.handleEmailAddress.bind(this);
        this.handlePassword = this.handlePassword.bind(this);
        this.handleFirstName = this.handleFirstName.bind(this);
        this.handleLastName = this.handleLastName.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
        
    }

    handleEmailAddress(event) {
        this.setState({ EmailAddress: event.target.value });
    }

    handlePassword(event) {
        this.setState({ Password: event.target.value });
    }

    handleFirstName(event) {
        this.setState({ FirstName: event.target.value });
    }

    handleLastName(event) {
        this.setState({ LastName: event.target.value });
    }

    handleSubmit(event) {
        alert("Sign Up action should occur here");
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

                <label>
                    First Name:
                    <input type="text" placeholder="First Name" 
                           value={ this.state.FirstName } onChange={ this.handleFirstName } />
                </label>

                <label>
                    Last Name:
                    <input type="text" placeholder="Last Name" 
                           value={ this.state.LastName } onChange={ this.handleLastName } />
                </label>

                <button>Sign Up</button>

            </form>
        );
    }

};

export default SignUp;