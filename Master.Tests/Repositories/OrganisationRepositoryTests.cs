using System.Collections.Generic;

using NUnit.Framework;

using Master.Models;
using Master.Contexts;
using Master.Repositories;
using Master.Interfaces.Repositories;

namespace Tests.Repositories
{
    [TestFixture]
    public class OrganisationRepositoryTests
    {
        OrganisationRepository Repository;
        int OrganisationId = 101100;
        string OrganisationName = "Donger's Inc";
        string OrganisationDirector = "johnsnow@example.com";
        Organisation NewOrganisation;
        const byte TopOrganisationsLength = 5;

        public OrganisationRepositoryTests()
        {
            DissertationContext DbContext = new DissertationContext();
            Repository = new OrganisationRepository(DbContext);
        }

        [OneTimeSetUp]
        public void OrganisationRepoTestSetup()
        {
            NewOrganisation = new Organisation
            {
                OrganisationName = "Zuma",
                OrganisationType = OrganisationType.Employer,
                Location = "NewCastle",
                Director = "jamesbond@example.com"
            };
        }

        [Test]
        [Ignore("Tested To Ensure DBConnection and Functionality")]
        public void DeleteOrganisation()
        {
            Repository.DeleteOrganisation(NewOrganisation.OrganisationName);
            Organisation deletedOrganisation = Repository.FindOrganisationByName(NewOrganisation.OrganisationName);
            Assert.Null(deletedOrganisation);
        }

        [Test]
        public void FindOrganisationByIdTest()
        {
            Organisation organisation;
            organisation = Repository.FindOrganisationById(OrganisationId);

            Assert.Multiple(() => {
                Assert.NotNull(organisation);
                Assert.AreEqual(OrganisationId, organisation.OrganisationId);
                Assert.AreEqual(OrganisationName, organisation.OrganisationName);
                Assert.AreEqual(OrganisationDirector, organisation.Director);
            });
        }

        [Test]
        public void FindOrganisationByName()
        {
            Organisation organisation;
            organisation = Repository.FindOrganisationByName(OrganisationName);

            Assert.Multiple(() => {
                Assert.NotNull(organisation);
                Assert.AreEqual(OrganisationId, organisation.OrganisationId);
                Assert.AreEqual(OrganisationName, organisation.OrganisationName);
                Assert.AreEqual(OrganisationDirector, organisation.Director);            
            });
        }

        [Test]
        public void IncreaseNumberOfContracts()
        {
            Organisation organisation;
            organisation = Repository.FindOrganisationById(OrganisationId);
            ushort? previousNumberOfContracts = organisation.NumberOfContracts;
            Repository.IncreaseNumberOfContracts(OrganisationId);
            organisation = Repository.FindOrganisationById(OrganisationId);
            Assert.Greater(organisation.NumberOfContracts, previousNumberOfContracts);
        }
        
        [Test]
        public void ReduceNumberOfContracts()
        {
            Organisation organisation;
            organisation = Repository.FindOrganisationById(OrganisationId);
            ushort? previousNumberOfContracts = organisation.NumberOfContracts;
            Repository.DecreaseNumberOfContracts(OrganisationId);
            organisation = Repository.FindOrganisationById(OrganisationId);
            Assert.Less(organisation.NumberOfContracts, previousNumberOfContracts);
        }

        [Test]
        public void GetMostContractsByAgency()
        {
            var result = Repository.GetMostContractsByAgency();
            Assert.True(result.GetType().IsGenericType);         
        }

        [Test]
        public void GetMostContractsByEmployer()
        {
            var result = Repository.GetMostContractsByEmployer();
            Assert.True(result.GetType().IsGenericType);
        }

        [Test]
        [Ignore("Tested To Ensure DBConnection and Functionality")]
        public void SaveOrganisation()
        {
            Repository.SaveNewOrganisation(NewOrganisation);
            Organisation savedOrganisation = Repository.FindOrganisationByName(NewOrganisation.OrganisationName);
            Assert.NotNull(savedOrganisation);
        }
    }
}