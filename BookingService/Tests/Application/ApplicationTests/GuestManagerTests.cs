using Application;
using Application.Guest;
using Application.Guest.Requests;
using Domain.Entities;
using Domain.Ports;
using Moq;
using System.Net.NetworkInformation;

namespace ApplicationTests
{
    public class GuestManagerTests
    {

        [SetUp]
        public void Setup()
        {

        }

        [TestCase("")]
        [TestCase(null)]
        [TestCase("a")]
        [TestCase("ab")]
        [TestCase("abc")]
        public async Task Should_Return_InvalidPersonDocumentIdException_WhenDocsAreInvalid(string docNumber)
        {
            var guestDTO = new GuestDto { Name = "test", Surname = "Ciclano", Email = "ac@cde.com", IdNumber = docNumber, IdTypeCode = 1 };

            var request = new CreateGuestRequest()
            {
                Data = guestDTO,
            };


            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Create(
                It.IsAny<Guest>())).Returns(Task.FromResult(222));


            var guestManager = new GuestManager(fakeRepo.Object);

            var res = await guestManager.CreateGuest(request);

            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.INVALID_PERSON_ID);
            Assert.AreEqual(res.Message, "The ID passed is not valid");
        }


        [TestCase("", "fulano", "ciclano@teste.com")]
        [TestCase(null, "fulano", "ciclano@teste.com")]
        
        [TestCase("José", "", "ciclano@teste.com")]
        [TestCase("Maria", null, "ciclano@teste.com")]
        
        [TestCase("Abraao", "fulano", "")]
        [TestCase("Isaac", "fulano", null)]
        public async Task Should_Return_MissingRequiredInformation_WhenDocsAreInvalid(string name, string surname, string email)
        {
            var guestDTO = new GuestDto()
            {

                Name = name,
                Surname = surname,
                Email = email,
                IdNumber = "125d",
                IdTypeCode = 1
            };

            var request = new CreateGuestRequest()
            {
                Data = guestDTO
            };

            var fakeRepo = new Mock<IGuestRepository>();

            fakeRepo.Setup(x => x.Create(It.IsAny<Guest>())).Returns(Task.FromResult(222));

            var guestManager = new GuestManager(fakeRepo.Object);

            var res = await guestManager.CreateGuest(request);

            Assert.IsNotNull(res);
            Assert.False(res.Success);
            Assert.AreEqual(res.ErrorCode, ErrorCodes.MISSING_REQUIRED_INFORMATION);
            Assert.AreEqual(res.Message, "Missing required information passed");

        }
    }
}