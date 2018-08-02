DROP DATABASE IF EXISTS Dissertation;
GO

CREATE DATABASE Dissertation;
GO

USE Dissertation;
GO

CREATE TABLE Users (
    EmailAddress VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(150) NOT NULL,
    UserRole VARCHAR(50) NOT NULL,
    CONSTRAINT PK_Users PRIMARY KEY (EmailAddress),
    CONSTRAINT User_Role_Validation CHECK(UserRole IN ('contractor', 'recruiter', 'director'))
);

CREATE TABLE Organisations (
    OrganisationID INT NOT NULL,
    OrganisationName VARCHAR(75) NOT NULL,
    OrganisationType VARCHAR(10) NOT NULL,
    PersonalStatement VARCHAR(1500),
    Location VARCHAR(30) NOT NULL,
    NumberOfAvailableAdverts TINYINT DEFAULT(5),
    Director VARCHAR(50) NOT NULL,
    CONSTRAINT U_Organisation UNIQUE (OrganisationID, OrganisationName, Director),
    CONSTRAINT PK_Organisation PRIMARY KEY (OrganisationID),
    CONSTRAINT FK_Organisation_Director FOREIGN KEY (Director) REFERENCES Users(EmailAddress),
    CONSTRAINT Organisation_Type_Validation CHECK (OrganisationType IN ('employer', 'agency')),
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
    DateCreated DATE DEFAULT GETDATE(),
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
    Role VARCHAR(35) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE,
    Present BIT,
    AchievementsAndResponsibilities VARCHAR(3000),
    CONSTRAINT FK_Work_Experience FOREIGN KEY (EmailAddress) REFERENCES Contractors(EmailAddress)
);

CREATE TABLE Education (
    InstitionName VARCHAR(75) NOT NULL,
    DegreeName VARCHAR(100) NOT NULL,
    DegreeLevel VARCHAR(20) NOT NULL,
    WithHons BIT,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    CONSTRAINT DEGREE_CLASS CHECK (DegreeLevel IN ('secondary', 'associate', 'bachelor', 
                                                    'pgcert', 'pgdip', 'master', 'doctorate'))
);
GO

CREATE TABLE Saved_Contract (
    ContractID INT NOT NULL UNIQUE,
    DateSaved DATE DEFAULT GETDATE(),
    CONSTRAINT FK_Saved_Contract FOREIGN KEY(ContractID) REFERENCES Contracts(ContractID)
);

CREATE TABLE Applied_Contract (
    ContractID INT NOT NULL UNIQUE,
    DateApplied DATE DEFAULT GETDATE(),
    CONSTRAINT FK_Applied_Contract FOREIGN KEY(ContractID) REFERENCES Contracts(ContractID)
);
GO

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
    CONSTRAINT PK_Industrie PRIMARY KEY (Industry)
);
GO