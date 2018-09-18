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
        setUpDatabase();
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
    CONSTRAINT FK_Contractor_EmailAddress FOREIGN KEY (EmailAddress) 
    REFERENCES Contractor_Account(EmailAddress) ON UPDATE CASCADE ON DELETE CASCADE
)`;

const CreateDirectorAccountTable = `CREATE TABLE Director_Account (
    EmailAddress VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(150) NOT NULL,
    FirstName VARCHAR(30) NOT NULL,
    LastName VARCHAR(30) NOT NULL,
    CONSTRAINT PK_Director PRIMARY KEY (EmailAddress)
)`;

const CreateOrganisationTable = `CREATE TABLE Organisation (
    OrganisationID INT NOT NULL AUTO_INCREMENT,
    OrganisationName VARCHAR(75) NOT NULL,
    OrganisationType ENUM ('Employer', 'Agency') NOT NULL ,
    OrganisationStatement VARCHAR(1500),
    OrganisationProfilePictureLocation VARCHAR(250),
    OrganisationProfileBannerLocation VARCHAR(250),
    Location VARCHAR(30) NOT NULL,
    NumberOfAvailableAdverts SMALLINT UNSIGNED DEFAULT 5,
    NumberOfContracts SMALLINT UNSIGNED DEFAULT 0,
    Director VARCHAR(50) NOT NULL,
    CONSTRAINT U_Organisation UNIQUE (OrganisationID, OrganisationName, Director),
    CONSTRAINT PK_Organisation PRIMARY KEY (OrganisationID),
    CONSTRAINT FK_Organisation_Director FOREIGN KEY (Director) REFERENCES Director_Account(EmailAddress),
    CONSTRAINT Organisation_Adverts CHECK (NumberOfAvailableAdverts >= 5)
)AUTO_INCREMENT = 101100`;

const CreateRecruiterAccountTable = `CREATE TABLE Recruiter_Account (
    EmailAddress VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(150) NOT NULL,
    FirstName VARCHAR(30) NOT NULL,
    LastName VARCHAR(30) NOT NULL,
    OrganisationID INT,
    CONSTRAINT PK_Recruiter PRIMARY KEY (EmailAddress),
    CONSTRAINT FK_Recruiter_Organisation FOREIGN KEY (OrganisationID) REFERENCES Organisation(OrganisationID))`;

const CreateContractTable = `CREATE TABLE Contract (
    ContractID INT NOT NULL AUTO_INCREMENT,
    JobTitle VARCHAR(75) NOT NULL,
    DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP,
    OrganisationID INT NOT NULL,
    Location VARCHAR(30) NOT NULL,
    Description VARCHAR(2000),
    Duration TINYINT NOT NULL,
    MinimumSalary SMALLINT UNSIGNED NOT NULL,
    MaximumSalary SMALLINT UNSIGNED NOT NULL,
    ApplicationURL VARCHAR(250),
    CONSTRAINT PK_Contract PRIMARY KEY (ContractID),
    CONSTRAINT FK_Contract_Organisation FOREIGN KEY (OrganisationID) REFERENCES Organisation(OrganisationID),
    CONSTRAINT Min_Contract_Duration CHECK(Duration > 0),
	CONSTRAINT Max_Contract_Duration CHECK(Duration <= 24)
)AUTO_INCREMENT = 10000`;

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

const InsertTestContractorAccount = `INSERT INTO Contractor_Account(EmailAddress, Password, FirstName, LastName) 
VALUES ("bourneCoder@example.com", "9mvkY64Ct1ALAO3iJpB869Mo9MARJ0TftBbS7MmTctG9Vqqz", "Jason", "Bourne")`;

const InsertTestDirectorAccount = `INSERT INTO Director_Account(EmailAddress, Password, FirstName, LastName)
VALUES ("johnsnow@example.com", "sfl7FadkEa8ifH34oerRwefeN3Haw", "John", "Snow")`;

const InsertTestDirectorAccount1 = `INSERT INTO Director_Account(EmailAddress, Password, FirstName, LastName)
VALUES ("jamesbond@example.com", "kl56nbs8dgrnh7rt9er8s9ui3b2oi24b5y3i2obi", "James", "Bond")`;

const InsertTestOrganisation = `INSERT INTO Organisation(OrganisationName, OrganisationType, Location, Director)
VALUES ("Donger's Inc", "Employer", "Manchester", "johnsnow@example.com")`;

const DatabaseSetupQueries = [
    DropDatabase, CreateDatabase, UseDatabase, CreateContractorAccountTable, CreateContractorProfileTable, 
    CreateDirectorAccountTable, CreateOrganisationTable,  CreateRecruiterAccountTable, CreateContractTable,
    CreateSavedContractTable, CreateAppliedContractTable, CreateLanguagesTable, CreateSkillsTable,
    CreateIndustriesTable, InsertTestContractorAccount, InsertTestDirectorAccount, InsertTestDirectorAccount1,
    InsertTestOrganisation];

const jobTitles = [
    "Customer Operations Advisor", "Software Architect", "Systems Architect", "Business Analyst", "Web Developer",
    "Construction Manager", "Quantity Surveyor", "Technical Cordinator", "Electrical Engineer",
    "Health and Safety Supervisor", "Logistics Manager", "Personal Assistant", "Human Resources Administrator",
    "Senior Recruiter", "Project Secretary", "Commissioning Engineer", "Sales Operations Administrator",
    "Purchase Ledger Clerk", "Finance Manager", "Credit Controller", "Management Accountant", "Finance Administrator",
    "Account Assistant", "Senior Payroll Administator", "Finance Analyst", "Pension Administrator",
    "Operations Associate", "Security Risk Manager", "Chemical Engineer", "Installation Wireman", "Metal Worker",
    "Site Engineer", "Electrical Maintenance Technician", "Senior Technician", "Lab Technician", "Mechanical CAD Engineer",
    "Control & Instrumentation Supervisor", "Integration Engineer", "Pupil Support Assistant", "Teaching Assistant",
    "Primary Teacher", "Nursery Teacher", "Performance Engineer", "Software Delivery Graduate", "IT Systems Administrator",
    "Software Engineer", "Data Engineer", "Data Analyst", "2nd Line Support Engineer", "Project Manager",
    "Warehouse Operative"
];

function getRandomInt(maxValue) {
    return Math.floor(Math.random() * Math.floor(maxValue));
};

function selectJobTitle() {
    return jobTitles[getRandomInt(jobTitles.length - 1)];
};

function selectOrganisationForContract() {
    return Math.floor(Math.random() * (101114 - 101100) + 101100);
};

function selectLocation() {
    let locations = [ "Manchester", "Leeds", "Newcastle", "Milton Keyes", "Bristol", "Essex",
                      "Brighton", "Kent", "Surrey", "London", "Southampton", "Portsmouth"];    
    let locationIndex = Math.floor(Math.random() * 12);
    return locations[locationIndex];
};

function selectContractDuration() {
    let duration = [3, 6, 8, 10, 12, 16, 18, 20, 24];
    return duration[getRandomInt(duration.length - 1)];
};

function selectMinimumSalary() {
    return Math.floor(Math.random() * (250 - 150) + 150);
};

function selectMaximumSalary() {
    return Math.floor(Math.random() * (750 - 300) + 300);
};

function generateContractDescription() {
    let randString = loremIpsum({
        count: 3,
        units: "paragraphs",
        paragraphLowerBound: 3,
        paragraphUpperBound: 7,
        format: "plain"
    });
    return randString;
};

function createContracts() {
    let NumberOfContracts = 600;
    let NumberOfContractsCreated = 0;

    while(NumberOfContractsCreated != NumberOfContracts) {
        let contract = {
            title: selectJobTitle(),
            orgID: selectOrganisationForContract(),
            location: selectLocation(),
            description: generateContractDescription(),
            duration: selectContractDuration(),
            minSal: selectMinimumSalary(),
            maxSal: selectMaximumSalary()
        };

        let saveContractQuery =
        `INSERT INTO contract(JobTitle, OrganisationID, Location, Description, Duration, MinimumSalary, MaximumSalary)
         VALUES (${mysql.escape(contract.title)}, ${mysql.escape(contract.orgID)}, ${mysql.escape(contract.location)},
         ${mysql.escape(contract.description)}, ${mysql.escape(contract.duration)},
         ${mysql.escape(contract.minSal)}, ${mysql.escape(contract.maxSal)})`;

        let updateNumberOfContractQuery =
        `UPDATE organisation SET NumberOfContracts = NumberOfContracts + 1 WHERE OrganisationID = ${mysql.escape(contract.orgID)}`;

        executeQuery(saveContractQuery);
        executeQuery(updateNumberOfContractQuery);
        NumberOfContractsCreated++;
    };
    endConnection();
};

const directors = [];

function selectEmailProvider() {
    // A function that selects email service provider for added realism of Data
    let emailServices = ["gmail", "yahoo", "aol", "live"];
    let emailLocale = [".com", ".co.uk"];
    let localeIndex = Math.floor(Math.random() * 2);
    let serviceIndex = Math.floor(Math.random() * 4);
    let selectedService = emailServices[serviceIndex] + emailLocale[localeIndex];

    return selectedService;
};

function createDirector() {

    let emailProvider = selectEmailProvider();
    let generatedFirstName = faker.name.firstName();
    let generatedLastName = faker.name.lastName();
    let generatedEmail = faker.internet.email(generatedFirstName, generatedLastName, emailProvider);

    let director = {
        email: generatedEmail,
        name: {
            first: generatedFirstName,
            last: generatedLastName
        },
        password: faker.internet.password()
    };
    return director;
};

function populateDirectors() {
    for(let i = 0; i < Organisations.length; i++) {
        let director = createDirector();
        directors.push(director);
        let saveDirectorQuery = `INSERT INTO director_account(EmailAddress, Password, FirstName, LastName) 
                                VALUES (${mysql.escape(director.email)}, ${mysql.escape(director.password)},
                                ${mysql.escape(director.name.first)}, ${mysql.escape(director.name.last)})`;
        executeQuery(saveDirectorQuery);
    };
    createOrganisations();
};

function generateOrganisationStatement() {
    let randString = loremIpsum({
        count: 2,
        units: "paragraphs",
        paragraphLowerBound: 3,
        paragraphUpperBound: 7,
        format: "plain"
    });
    return randString;
};

function createOrganisations() {

    Organisations.forEach((organisation, index) => {
        // TODO: Do something about Profile Pictures
        organisation.OrganisationStatement = generateOrganisationStatement();
        //organisation.OrganisationProfilePictureLocation;
        //organisation.OrganisationProfileBannerLocation;
        organisation.Director = directors[index].email;
        let saveOrganisationQuery = 
        `INSERT INTO organisation(OrganisationName, OrganisationType, OrganisationStatement, Location, Director)
         VALUES (${mysql.escape(organisation.OrganisationName)}, ${mysql.escape(organisation.OrganisationType)},
         ${mysql.escape(organisation.OrganisationStatement)}, ${mysql.escape(organisation.Location)},
         ${mysql.escape(organisation.Director)})`;
        executeQuery(saveOrganisationQuery);
    });
    createContracts();
};

function setUpDatabase() {
    DatabaseSetupQueries.forEach((query) => {
        executeQuery(query);
    });
    populateDirectors();
};

function executeQuery(query) {
    connection.query(query, (error) => {
        if(error)
        {
            console.log(warning(error));
        }
    });
};

function endConnection() {
    connection.end(function(error) {
        if(error) { 
            console.log(error);
        } else {
            console.log(success("Operation Complete!!!"));
        }
    });
};