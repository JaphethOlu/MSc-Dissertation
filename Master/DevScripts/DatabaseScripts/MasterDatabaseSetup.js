const fs = require("fs");
const mysql = require("mysql");
const faker = require("faker");
const loremIpsum = require("lorem-ipsum");

/* eslint-disable no-console */

// Chalk Styles
const chalk = require("chalk");
const info = chalk.blue;
const success = chalk.green;
const warning = chalk.yellow;
const failure = chalk.bold.red;

const connection = mysql.createConnection({
    host        : "localhost",
    user        : "root",
    password    : "M4st3rD1ss0"
});

connection.connect(err => {
    if (err) {
        failure(console.log("Failed to Connect to MySQL"));
        console.log(err);
    } else {
        success(console.log("Successfully connected to MySQL"));
        info(console.log("Connected as id: " + connection.threadId));
    }
});

const Organisations = JSON.parse(fs.readFileSync("./DevScripts/DatabaseScripts/Organisations.json"), "utf8");

// ================== String Representation of our Queries =====================
// Database Selection Queries
const DropDatabase = "DROP DATABASE IF EXISTS Dissertation";
const CreateDatabase = "CREATE DATABASE Dissertation";
const UseDatabase = "USE Dissertation";

// Create Table Queries
const CreateContractorAccountTable = `CREATE TABLE Contractor_Account (
    EmailAddress VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(150) NOT NULL,
    FirstName VARCHAR(30) NOT NULL,
    LastName VARCHAR(30) NOT NULL,
    CONSTRAINT PK_ContractorAccount PRIMARY KEY (EmailAddress))`;

const CreateRecruiterAccountTable = `CREATE TABLE Recruiter_Account (
    EmailAddress VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(150) NOT NULL,
    FirstName VARCHAR(30) NOT NULL,
    LastName VARCHAR(30) NOT NULL,
    OrganisationID INT,
    CONSTRAINT PK_Recruiter PRIMARY KEY (EmailAddress))`;

const CreateOrganisationTable = `CREATE TABLE Organisation (
    OrganisationID INT NOT NULL,
    OrganisationName VARCHAR(75) NOT NULL,
    OrganisationType ENUM ('Employer', 'Agency') NOT NULL ,
    OrganisationStatement VARCHAR(1500),
    OrganisationProfilePictureLocation VARCHAR(250),
    OrganisationProfileBannerLocation VARCHAR(250),
    Location VARCHAR(30) NOT NULL,
    NumberOfAvailableAdverts TINYINT DEFAULT 5,
    Director VARCHAR(50) NOT NULL,
    CONSTRAINT U_Organisation UNIQUE (OrganisationID, OrganisationName, Director),
    CONSTRAINT PK_Organisation PRIMARY KEY (OrganisationID),
    CONSTRAINT FK_Organisation_Director FOREIGN KEY (Director) REFERENCES Recruiter_Account(EmailAddress),
    CONSTRAINT Organisation_Adverts CHECK (NumberOfAvailableAdverts >= 5))`;

const CreateRecruiterAccountTableConstraint = "ALTER TABLE recruiter_account ADD CONSTRAINT FK_Recruiter_Organisation FOREIGN KEY (OrganisationID) REFERENCES Organisation(OrganisationID)";

const CreateContractTable = `CREATE TABLE Contract (
    ContractID INT NOT NULL UNIQUE,
    Position VARCHAR(30),
    DateCreated DATE,
    OrganisationID INT NOT NULL,
    Location VARCHAR(30),
    Description VARCHAR(2000),
    Duration TINYINT NOT NULL,
    StartDate DATE,
    EndDate DATE,
    CONSTRAINT PK_Contract PRIMARY KEY (ContractID),
    CONSTRAINT FK_Contract_Organisation FOREIGN KEY (OrganisationID) REFERENCES Organisation(OrganisationID),
    CONSTRAINT Min_Contract_Duration CHECK(Duration > 0),
	CONSTRAINT Max_Contract_Duration CHECK(Duration <= 24))`;

const CreateContractorProfileTable = `CREATE TABLE Contractor_Profile (
    EmailAddress VARCHAR(50) NOT NULL,
    FirstName VARCHAR(30) NOT NULL,
    LastName VARCHAR(30) NOT NULL,
    Headline VARCHAR(120),
    PersonalStatement VARCHAR(800),
    WorkExperience JSON, # -- Expected to be !> 10,000 characters
    Education JSON, # -- Expected to be !> 5,000 characters
    Location VARCHAR(30),
    CONSTRAINT PK_Contractor_EmailAddress PRIMARY KEY (EmailAddress),
    CONSTRAINT FK_Contractor_EmailAddress FOREIGN KEY (EmailAddress) REFERENCES Contractor_Account(EmailAddress) ON UPDATE CASCADE ON DELETE CASCADE)`;

const CreateSavedContractTable = `CREATE TABLE Saved_Contract (
    ContractID INT NOT NULL UNIQUE,
    DateSaved DATE,
    CONSTRAINT FK_Saved_Contract FOREIGN KEY(ContractID) REFERENCES Contract(ContractID))`;

const CreateAppliedContractTable = `CREATE TABLE Applied_Contract (
    ContractID INT NOT NULL UNIQUE,
    DateApplied DATE,
    CONSTRAINT FK_Applied_Contract FOREIGN KEY(ContractID) REFERENCES Contract(ContractID))`;

const CreateLanguagesTable = `CREATE TABLE Languages (
    Language VARCHAR(30) UNIQUE,
    CONSTRAINT PK_Language PRIMARY KEY(Language)
)`;

const CreateSkillsTable = `CREATE TABLE Skills (
    Skill VARCHAR(50) UNIQUE,
    CONSTRAINT PK_Skill PRIMARY KEY(Skill))`;

const CreateIndustriesTable = `CREATE TABLE Industries (
    Industry VARCHAR(50) UNIQUE,
    CONSTRAINT PK_Industry PRIMARY KEY (Industry))`;

const InsertTestingAccount = "INSERT INTO contractor_account(EmailAddress, Password, FirstName, LastName)"
+ " VALUES (`bourneCoder@example.com`, `9mvkY64Ct1ALAO3iJpB869Mo9MARJ0TftBbS7MmTctG9Vqqz`, `Jason`, `Bourne`)";

const DatabaseSetupQueries = [
    DropDatabase, CreateDatabase, UseDatabase, CreateContractorAccountTable, CreateRecruiterAccountTable, CreateOrganisationTable,
    CreateRecruiterAccountTableConstraint, CreateContractTable, CreateContractorProfileTable, CreateSavedContractTable,
    CreateAppliedContractTable, CreateLanguagesTable, CreateSkillsTable, CreateIndustriesTable, InsertTestingAccount
];

function selectEmailProvider() {
    // A function that selects email service provider for added realism of Data
    let emailServices = ["gmail", "yahoo", "aol", "live"];
    let emailLocale = [".com", ".co.uk"];
    let localeIndex = Math.floor(Math.random() * 2);
    let serviceIndex = Math.floor(Math.random() * 4);
    let selectedService = emailServices[serviceIndex] + emailLocale[localeIndex];

    return selectedService;
};

const directors = [];


function createDirector() {

    let emailProvider = selectEmailProvider();
    let generatedFirstName = faker.name.firstName();
    let generatedLastName = faker.name.lastName();

    let generatedEmail = faker.internet.email(name.first, name.last, emailProvider);

    let director = {
        email: generatedEmail,
        name: {
            first: generatedFirstName,
            last: generatedLastName
        },
        password: faker.internet.password
    };

    return director;
};

function generateOrganisationStatement() {
    let randString = loremIpsum({
        count: 3,
        units: "paragraphs",
        paragraphLowerBound: 8,
        paragraphUpperBound: 12,
        format: "plain"
    });
    return randString;
};

function selectOrganisationLocation() {
    let locations = [ "Manchester", "Leeds", "Newcastle", "Milton Keyes", "Bristol", "Essex",
                      "Brighton", "Kent", "Surrey", "London", "Southampton", "Portsmouth"];
    
    let locationIndex = Math.floor(Math.random() * 12);

    return locations[locationIndex];
};

function createOrganisations() {
    let index = 0;
    Organisations.forEach((organisation) => {
        // TODO: Do something about OrganisationID and Profile Pictures
        //organisation.OrganisationID;
        organisation.OrganisationStatement = generateOrganisationStatement();
        //organisation.OrganisationProfilePictureLocation;
        //organisation.OrganisationProfileBannerLocation;
        organisation.Location = selectOrganisationLocation();
        organisation.Director = directors[index].email;
        let saveOrganisationQuery = `INSERT INTO organisations(OrganisationName, OrganisationType, OrganisationStatement, Location, Director)
                                    VALUES (${organisation.OrganisationName}, ${organisation.OrganisationType}, ${organisation.OrganisationStatement}, ${organisation.Location}, ${organisation.Director})`;
        executeQuery(saveOrganisationQuery);
    });
};

function populateDirectors() {
    let i;
    for(i = 0; i < Organisations.length; i++) {
        let director = createDirector();
        directors.push(director);
        let saveDirectorQuery = `INSERT INTO recruiter_account(EmailAddress, Password, FirstName, LastName) 
                                VALUES (${director.email}, ${director.password}, ${director.name.first}, ${director.name.last})`;
        executeQuery(saveDirectorQuery);
    };
};







function executeQuery(query) {
    connection.query(query, (error) => {
        if(error)
        {
            console.log(error);
        }
    });
};

//MySQL Port on my PC is 3306
//connection.close();