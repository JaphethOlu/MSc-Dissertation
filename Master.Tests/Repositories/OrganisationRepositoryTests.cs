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
        OrganisationRepository OrganisationRepository;
        int OrganisationId = 101100;
        string OrganisationName = "Donger's Inc";
        string OrganisationDirector = "johnsnow@example.com";
        Organisation NewOrganisation;

        public OrganisationRepositoryTests()
        {
            DissertationContext DbContext = new DissertationContext();
            OrganisationRepository = new OrganisationRepository(DbContext);
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
        public void DeleteOrganisation()
        {
            OrganisationRepository.DeleteOrganisation(NewOrganisation.OrganisationName);
            Organisation deletedOrganisation = OrganisationRepository.FindOrganisationByName(NewOrganisation.OrganisationName);
            Assert.Null(deletedOrganisation);
        }

        [Test]
        public void FindOrganisationByIdTest()
        {
            Organisation organisation;
            organisation = OrganisationRepository.FindOrganisationById(OrganisationId);
            Assert.NotNull(organisation);
            Assert.AreEqual(OrganisationId, organisation.OrganisationId);
            Assert.AreEqual(OrganisationName, organisation.OrganisationName);
            Assert.AreEqual(OrganisationDirector, organisation.Director);
        }

        [Test]
        public void FindOrganisationByName()
        {
            Organisation organisation;
            organisation = OrganisationRepository.FindOrganisationByName(OrganisationName);
            Assert.NotNull(organisation);
            Assert.AreEqual(OrganisationId, organisation.OrganisationId);
            Assert.AreEqual(OrganisationName, organisation.OrganisationName);
            Assert.AreEqual(OrganisationDirector, organisation.Director);
        }

        [Test]
        public void SaveOrganisation()
        {
            OrganisationRepository.SaveNewOrganisation(NewOrganisation);
            Organisation savedOrganisation = OrganisationRepository.FindOrganisationByName(NewOrganisation.OrganisationName);
            Assert.NotNull(savedOrganisation);
        }
    }
}