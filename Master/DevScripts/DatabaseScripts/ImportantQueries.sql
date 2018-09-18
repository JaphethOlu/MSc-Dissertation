USE dissertation;

SELECT OrganisationName, OrganisationType, OrganisationStatement, NumberOfContracts
FROM organisation WHERE OrganisationType = "Employer" ORDER BY NumberOfContracts DESC LIMIT 5;

SELECT OrganisationName, OrganisationType, OrganisationStatement, NumberOfContracts
FROM organisation WHERE OrganisationType = "Agency" ORDER BY NumberOfContracts DESC LIMIT 5;