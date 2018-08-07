DROP DATABASE IF EXISTS Dissertation;

CREATE DATABASE Dissertation;

USE Dissertation;

CREATE TABLE Users (
    EmailAddress VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(150) NOT NULL,
    UserRole ENUM('Contractor', 'Recruiter', 'Director') NOT NULL,
    CONSTRAINT PK_User PRIMARY KEY (EmailAddress)
);

CREATE TABLE Organisations (
    OrganisationID INT NOT NULL,
    OrganisationName VARCHAR(75) NOT NULL,
    OrganisationType ENUM ('Employer', 'Agency') NOT NULL ,
    PersonalStatement VARCHAR(1500),
    Location VARCHAR(30) NOT NULL,
    NumberOfAvailableAdverts TINYINT DEFAULT 5,
    Director VARCHAR(50) NOT NULL,
    CONSTRAINT U_Organisation UNIQUE (OrganisationID, OrganisationName, Director),
    CONSTRAINT PK_Organisation PRIMARY KEY (OrganisationID),
    CONSTRAINT FK_Organisation_Director FOREIGN KEY (Director) REFERENCES Users(EmailAddress),
    CONSTRAINT Organisation_Adverts CHECK (NumberOfAvailableAdverts >= 5)
);

CREATE TABLE Recruiters (
    EmailAddress VARCHAR(50) NOT NULL UNIQUE,
    FirstName VARCHAR(30) NOT NULL,
    LastName VARCHAR(30) NOT NULL,
    OrganisationID INT NOT NULL,
    CONSTRAINT PK_Recruiter PRIMARY KEY (EmailAddress),
    CONSTRAINT FK_Recruiter_EmailAddress FOREIGN KEY (EmailAddress) REFERENCES Users(EmailAddress),
    CONSTRAINT FK_Recruiter_Organisation FOREIGN KEY (OrganisationID) REFERENCES Organisations(OrganisationID)
);

CREATE TABLE Contracts (
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
    CONSTRAINT FK_Contract_Organisation FOREIGN KEY (OrganisationID) REFERENCES Organisations(OrganisationID),
    CONSTRAINT Min_Contract_Duration CHECK(Duration > 0),
	CONSTRAINT Max_Contract_Duration CHECK(Duration <= 24)
);

CREATE TABLE Contractors (
    EmailAddress VARCHAR(50) NOT NULL,
    FirstName VARCHAR(30),
    LastName VARCHAR(30),
    Headline VARCHAR(120),
    PersonalStatement VARCHAR(800),
    Location VARCHAR(30),
    CONSTRAINT PK_Contractor_EmailAddress PRIMARY KEY (EmailAddress),
    CONSTRAINT FK_Contractor_EmailAddress FOREIGN KEY (EmailAddress) REFERENCES Users(EmailAddress)
);

CREATE TABLE Work_Experience (
    EmailAddress VARCHAR(50) NOT NULL,
    EmployerName VARCHAR(100) NOT NULL,
    JobRole VARCHAR(35) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE,
    Present TINYINT(1),
    AchievementsAndResponsibilities VARCHAR(3000), -- //TODO: Convert to JSON
    CONSTRAINT FK_Work_Experience FOREIGN KEY (EmailAddress) REFERENCES Contractors(EmailAddress)
);

CREATE TABLE Education (
    InstitionName VARCHAR(75) NOT NULL,
    DegreeName VARCHAR(100) NOT NULL,
    DegreeLevel ENUM ('Secondary', 'Associate', 'Bachelor',
					  'PGCert', 'PGDip', 'Master', 'Doctorate') NOT NULL,
    WithHons TINYINT(1),
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL
);

CREATE TABLE Saved_Contract (
    ContractID INT NOT NULL UNIQUE,
    DateSaved DATE,
    CONSTRAINT FK_Saved_Contract FOREIGN KEY(ContractID) REFERENCES Contracts(ContractID)
);

CREATE TABLE Applied_Contract (
    ContractID INT NOT NULL UNIQUE,
    DateApplied DATE,
    CONSTRAINT FK_Applied_Contract FOREIGN KEY(ContractID) REFERENCES Contracts(ContractID)
);

CREATE TABLE Languages (
    Language VARCHAR(30) UNIQUE,
    CONSTRAINT PK_Language PRIMARY KEY(Language)
);

CREATE TABLE Skills (
    Skill VARCHAR(50) UNIQUE,
    CONSTRAINT PK_Skill PRIMARY KEY(Skill)
);

CREATE TABLE Industries (
    Industry VARCHAR(50) UNIQUE,
    CONSTRAINT PK_Industry PRIMARY KEY (Industry)
);