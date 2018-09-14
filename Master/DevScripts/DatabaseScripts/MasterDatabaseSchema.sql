DROP DATABASE IF EXISTS Dissertation;

CREATE DATABASE Dissertation;

USE Dissertation;

CREATE TABLE Contractor_Account (
    EmailAddress VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(150) NOT NULL,
    FirstName VARCHAR(30) NOT NULL,
    LastName VARCHAR(30) NOT NULL,
    CONSTRAINT PK_ContractorAccount PRIMARY KEY (EmailAddress)
);

CREATE TABLE Contractor_Profile (
    EmailAddress VARCHAR(50) NOT NULL,
    FirstName VARCHAR(30) NOT NULL,
    LastName VARCHAR(30) NOT NULL,
    Headline VARCHAR(120),
    PersonalStatement VARCHAR(800),
    WorkExperience JSON, # -- Expected to be !> 10,000 characters
    Education JSON, # -- Expected to be !> 5,000 characters
    Location VARCHAR(30),
    CONSTRAINT PK_Contractor_EmailAddress PRIMARY KEY (EmailAddress),
    CONSTRAINT FK_Contractor_EmailAddress FOREIGN KEY (EmailAddress) REFERENCES Contractor_Account(EmailAddress) ON UPDATE CASCADE ON DELETE CASCADE
);

CREATE TABLE Director_Account (
    EmailAddress VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(150) NOT NULL,
    FirstName VARCHAR(30) NOT NULL,
    LastName VARCHAR(30) NOT NULL,
    CONSTRAINT PK_Director PRIMARY KEY (EmailAddress)
);

CREATE TABLE Organisation (
    OrganisationID INT NOT NULL AUTO_INCREMENT,
    OrganisationName VARCHAR(75) NOT NULL,
    OrganisationType ENUM ('Employer', 'Agency') NOT NULL ,
    OrganisationStatement VARCHAR(1500),
    OrganisationProfilePictureLocation VARCHAR(250),
    OrganisationProfileBannerLocation VARCHAR(250),
    Location VARCHAR(30) NOT NULL,
    NumberOfAvailableAdverts SMALLINT UNSIGNED DEFAULT 5,
    Director VARCHAR(50) NOT NULL,
    CONSTRAINT U_Organisation UNIQUE (OrganisationID, OrganisationName, Director),
    CONSTRAINT PK_Organisation PRIMARY KEY (OrganisationID),
    CONSTRAINT FK_Organisation_Director FOREIGN KEY (Director) REFERENCES Director_Account(EmailAddress),
    CONSTRAINT Organisation_Adverts CHECK (NumberOfAvailableAdverts >= 5)
)AUTO_INCREMENT = 101100;

CREATE TABLE Recruiter_Account (
    EmailAddress VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(150) NOT NULL,
    FirstName VARCHAR(30) NOT NULL,
    LastName VARCHAR(30) NOT NULL,
    OrganisationID INT,
    CONSTRAINT PK_Recruiter PRIMARY KEY (EmailAddress),
    CONSTRAINT FK_Recruiter_Organisation FOREIGN KEY (OrganisationID) REFERENCES Organisation(OrganisationID)
);

CREATE TABLE Contract (
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
	CONSTRAINT Max_Contract_Duration CHECK(Duration <= 24)
);

CREATE TABLE Saved_Contract (
    ContractID INT NOT NULL UNIQUE,
    DateSaved DATE,
    CONSTRAINT FK_Saved_Contract FOREIGN KEY(ContractID) REFERENCES Contract(ContractID)
);

CREATE TABLE Applied_Contract (
    ContractID INT NOT NULL UNIQUE,
    DateApplied DATE,
    CONSTRAINT FK_Applied_Contract FOREIGN KEY(ContractID) REFERENCES Contract(ContractID)
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

INSERT INTO Contractor_Account(EmailAddress, Password, FirstName, LastName) 
VALUES ("bourneCoder@example.com", "9mvkY64Ct1ALAO3iJpB869Mo9MARJ0TftBbS7MmTctG9Vqqz", "Jason", "Bourne");

INSERT INTO Director_Account(EmailAddress, Password, FirstName, LastName)
VALUES ("johnsnow@example.com", "sfl7FadkEa8ifH34oerRwefeN3Haw", "John", "Snow");

INSERT INTO Director_Account(EmailAddress, Password, FirstName, LastName)
VALUES ("jamesbond@example.com", "kl56nbs8dgrnh7rt9er8s9ui3b2oi24b5y3i2obi", "James", "Bond");

INSERT INTO Organisation(OrganisationName, OrganisationType, Location, Director)
VALUES ("Donger's Inc", "Employer", "Manchester", "johnsnow@example.com");

/*
INSERT INTO Contractor_Profile(EmailAddress, FirstName, LastName)
VALUES ("bourneCoder@example.com", "Jason", "Bourne");

=============== OLD MODELS ==============
CREATE TABLE Contractor_Work_Experience (
    EmailAddress VARCHAR(50) NOT NULL,
    EmployerName VARCHAR(100) NOT NULL,
    JobRole VARCHAR(35) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE,
    Present TINYINT(1),
    AchievementsAndResponsibilities VARCHAR(3000),
    CONSTRAINT FK_Work_Experience FOREIGN KEY (EmailAddress) REFERENCES Contractor_Profile(EmailAddress) ON UPDATE CASCADE ON DELETE CASCADE
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

*/