const mysql = require("mysql");
const chalk = require("chalk");
const info = chalk.blue;
const success = chalk.green;
const warning = chalk.yellow;
const failure = chalk.bold.red;

const connection = mysql.createConnection({
    host        : "localhoset",
    user        : "disso",
    password    : "M4st3rDiss0"
});

connection.connect(err => {
    if (err) {
        failure(console.log("Failed to Connect to MySQL"));
        console.log(err);
    } else {
        success(console.log("Successfully connected to MySQL"));
        info(console.log("Conected as id: " + connection.threadId));
    }
});




//MySQL Port on my PC is 3306
//connection.close();