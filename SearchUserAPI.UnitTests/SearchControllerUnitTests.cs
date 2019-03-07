#region Namespaces
using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchUserAPI.Controllers;
using SearchUserAPI.Models;
using SearchUserAPI.Repositories;
using SearchUserAPI.Utility;
using SearchUserAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
#endregion

namespace SearchUserAPI.UnitTests
{
    /// <summary>
    /// Unit Tests for SearchController WebAPI
    /// </summary>
    [TestClass]
    public class SearchControllerUnitTests
    {
        /// <summary>
        /// Private members
        /// </summary>
        IMapper mapper;
        List<User> userList;
        ISearchUserRepository fakeUserRepository;
        ISearchDetail fakeSearchDetail;
        SearchUserController searchUserController;
        IActionResult actionResult;
        OkObjectResult okResult;
        Logger<SearchUserController> fakeLogger;
        IMemoryCache fakeMemoryCache;
        List<State> stateList;

        /// <summary>
        /// Test Setup
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            // Arrange
            // Register Mapping profile for AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            mapper = mappingConfig.CreateMapper();

            fakeUserRepository = A.Fake<ISearchUserRepository>();
            fakeSearchDetail = A.Fake<ISearchDetail>();
            fakeLogger = A.Fake<Logger<SearchUserController>>();
            fakeMemoryCache = A.Fake<MemoryCache>();
            userList = new List<User>();
            stateList = new List<State>();
            stateList.Add(new State() { Code = "NY", Name = "New York" });
            stateList.Add(new State() { Code = "NJ", Name = "New Jersey" }); ;

        }

        [TestMethod]
        public async Task SearchController_GetByState_ValidState_ThrowsNoException()
        {
            // Arrange
            A.CallTo(() => fakeUserRepository.GetFilteredUsers(fakeSearchDetail)).Returns(userList);
            A.CallTo(() => fakeUserRepository.GetAllStates()).Returns(stateList);
            string stateCode = "NJ";

            // Act
            searchUserController = new SearchUserController(fakeUserRepository, mapper, fakeSearchDetail,fakeMemoryCache, fakeLogger);
            actionResult = await searchUserController.GetByState(stateCode);
            okResult = actionResult as OkObjectResult;

            //Assert
            Assert.IsNotNull(okResult);
        }

        [TestMethod]
        public async Task SearchController_GetByState_InvalidState_ThrowsException()
        {
            // Arrange
            A.CallTo(() => fakeUserRepository.GetFilteredUsers(fakeSearchDetail)).Returns(userList);
            A.CallTo(() => fakeUserRepository.GetAllStates()).Returns(stateList);
            string stateCode = "AB";

            // Act
            searchUserController = new SearchUserController(fakeUserRepository, mapper, fakeSearchDetail, fakeMemoryCache, fakeLogger);
            actionResult = await searchUserController.GetByState(stateCode);
            okResult = actionResult as OkObjectResult;

            //Assert
            Assert.IsNull(okResult);
        }

        [TestMethod]
        public async Task SearchController_GetByState_ValidState_ShouldHandleException()
        {
            // Arrange
            string stateCode = "NJ";
            userList.Add(new User() { Age = 25, Email = "abc@gmail.com", FirstName = "A1", LastName = "B1", ID = 1, StateCode = "NJ" });
            userList.Add(new User() { Age = 26, Email = "bcd@gmail.com", FirstName = "A2", LastName = "B2", ID = 2, StateCode = "NJ" });
            userList.Add(new User() { Age = 27, Email = "john@gmail.com", FirstName = "A3", LastName = "B3", ID = 3, StateCode = "NJ" });
            A.CallTo(() => fakeUserRepository.GetFilteredUsers(fakeSearchDetail)).Throws(new Exception("Error occured"));
            A.CallTo(() => fakeUserRepository.GetAllStates()).Returns(stateList);

            // Act
            searchUserController = new SearchUserController(fakeUserRepository, mapper, fakeSearchDetail, fakeMemoryCache, fakeLogger);
            actionResult = await searchUserController.GetByState(stateCode);
            okResult = actionResult as OkObjectResult;

            // Assert
            Assert.IsNull(okResult);
        }

        [TestMethod]
        public async Task SearchController_GetByState_ValidAgeRange_ThrowsNoException()
        {
            // Arrange
            A.CallTo(() => fakeUserRepository.GetFilteredUsers(fakeSearchDetail)).Returns(userList);
            A.CallTo(() => fakeUserRepository.GetAllStates()).Returns(stateList);

            // Act
            searchUserController = new SearchUserController(fakeUserRepository, mapper, fakeSearchDetail, fakeMemoryCache, fakeLogger);
            actionResult = await searchUserController.GetUsersByAgeRange(15, 99);
            okResult = actionResult as OkObjectResult;

            //Assert
            Assert.IsNotNull(okResult);
        }

        [TestMethod]
        public async Task SearchController_GetByState_ValidAgeRange_ReturnsProperResults()
        {
            // Arrange
            userList.Add(new User() { Age = 25, Email = "abc@gmail.com", FirstName = "A1", LastName = "B1", ID = 1, StateCode = "NJ" });
            A.CallTo(() => fakeUserRepository.GetFilteredUsers(fakeSearchDetail)).Returns(userList);
            A.CallTo(() => fakeUserRepository.GetAllStates()).Returns(stateList);

            // Act
            searchUserController = new SearchUserController(fakeUserRepository, mapper, fakeSearchDetail, fakeMemoryCache, fakeLogger);
            actionResult = await searchUserController.GetUsersByAgeRange(15, 99);
            okResult = actionResult as OkObjectResult;
            List<UserViewModel> userVM = okResult.Value as List<UserViewModel>;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.IsNotNull(userVM[0].Name);
            Assert.AreEqual(userVM[0].Name, "A1 B1");

        }

        [TestMethod]
        public async Task SearchController_GetByState_ValidAgeRange_ShouldHandleException()
        {
            // Arrange
            userList.Add(new User() { Age = 27, Email = "john@gmail.com", FirstName = "A3", LastName = "B3", ID = 3, StateCode = "NJ" });
            A.CallTo(() => fakeUserRepository.GetFilteredUsers(fakeSearchDetail)).Throws(new Exception("Error occured"));
            A.CallTo(() => fakeUserRepository.GetAllStates()).Returns(stateList);

            // Act
            searchUserController = new SearchUserController(fakeUserRepository, mapper, fakeSearchDetail, fakeMemoryCache, fakeLogger);
            actionResult = await searchUserController.GetUsersByAgeRange(15, 99);
            okResult = actionResult as OkObjectResult;

            // Assert
            Assert.IsNull(okResult);
        }

        [TestMethod]
        public async Task SearchController_GetUsersByStateAndAgeRange_ValidParams_ThrowsNoException()
        {
            // Arrange
            A.CallTo(() => fakeUserRepository.GetFilteredUsers(fakeSearchDetail)).Returns(userList);
            A.CallTo(() => fakeUserRepository.GetAllStates()).Returns(stateList);

            // Act
            searchUserController = new SearchUserController(fakeUserRepository, mapper, fakeSearchDetail, fakeMemoryCache, fakeLogger);
            actionResult = await searchUserController.GetUsersByStateAndAgeRange("NJ", 15, 99);
            okResult = actionResult as OkObjectResult;

            //Assert
            Assert.IsNotNull(okResult);
        }

        [TestMethod]
        public async Task SearchController_GetUsersByStateAndAgeRange_ValidParams_ReturnsProperResults()
        {
            // Arrange
            userList.Add(new User() { Age = 25, Email = "abc@gmail.com", FirstName = "A1", LastName = "B1", ID = 1, StateCode = "NJ" });
            A.CallTo(() => fakeUserRepository.GetFilteredUsers(fakeSearchDetail)).Returns(userList);
            A.CallTo(() => fakeUserRepository.GetAllStates()).Returns(stateList);

            // Act
            searchUserController = new SearchUserController(fakeUserRepository, mapper, fakeSearchDetail, fakeMemoryCache, fakeLogger);
            actionResult = await searchUserController.GetUsersByStateAndAgeRange("NJ", 15, 99);
            okResult = actionResult as OkObjectResult;
            List<UserViewModel> userVM = okResult.Value as List<UserViewModel>;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.IsNotNull(userVM[0].Name);
            Assert.AreEqual(userVM[0].Name, "A1 B1");
            Assert.AreEqual(userVM[0].Age, 25);
            Assert.AreEqual(userVM[0].Email, "abc@gmail.com");

        }

        [TestMethod]
        public async Task SearchController_GetUsersByStateAndAgeRange_ValidParams_ShouldHandleException()
        {
            // Arrange
            userList.Add(new User() { Age = 27, Email = "john@gmail.com", FirstName = "A3", LastName = "B3", ID = 3, StateCode = "NJ" });
            A.CallTo(() => fakeUserRepository.GetFilteredUsers(fakeSearchDetail)).Throws(new Exception("Error occured"));
            A.CallTo(() => fakeUserRepository.GetAllStates()).Returns(stateList);

            // Act
            searchUserController = new SearchUserController(fakeUserRepository, mapper, fakeSearchDetail, fakeMemoryCache,fakeLogger);
            actionResult = await searchUserController.GetUsersByStateAndAgeRange("NJ", 15, 99);
            okResult = actionResult as OkObjectResult;

            // Assert
            Assert.IsNull(okResult);
        }

        [TestMethod]
        public async Task SearchController_GetUsersByStateOrAgeRange_ValidParams_ThrowsNoException()
        {
            // Arrange
            A.CallTo(() => fakeUserRepository.GetFilteredUsers(fakeSearchDetail)).Returns(userList);
            A.CallTo(() => fakeUserRepository.GetAllStates()).Returns(stateList);

            // Act
            searchUserController = new SearchUserController(fakeUserRepository, mapper, fakeSearchDetail, fakeMemoryCache, fakeLogger);
            actionResult = await searchUserController.GetUsersByStateOrAgeRange("NJ", 15, 99);
            okResult = actionResult as OkObjectResult;

            //Assert
            Assert.IsNotNull(okResult);
        }

        [TestMethod]
        public async Task SearchController_GetUsersByStateOrAgeRange_ValidParams_ReturnsProperResults()
        {
            // Arrange
            userList.Add(new User() { Age = 25, Email = "abc@gmail.com", FirstName = "A1", LastName = "B1", ID = 1, StateCode = "NJ" });

            A.CallTo(() => fakeUserRepository.GetFilteredUsers(fakeSearchDetail)).Returns(userList);
            A.CallTo(() => fakeUserRepository.GetAllStates()).Returns(stateList);

            // Act
            searchUserController = new SearchUserController(fakeUserRepository, mapper, fakeSearchDetail, fakeMemoryCache, fakeLogger);
            actionResult = await searchUserController.GetUsersByStateOrAgeRange("NJ", 15, 99);
            okResult = actionResult as OkObjectResult;
            List<UserViewModel> userVM = okResult.Value as List<UserViewModel>;

            // Assert
            Assert.IsNotNull(okResult);
            Assert.IsNotNull(userVM[0].Name);
            Assert.AreEqual(userVM[0].Name, "A1 B1");
            Assert.AreEqual(userVM[0].Age, 25);
            Assert.AreEqual(userVM[0].Email, "abc@gmail.com");

        }

        [TestMethod]
        public async Task SearchController_GetUsersByStateOrAgeRange_ValidParams_ShouldHandleException()
        {
            // Arrange
            userList.Add(new User() { Age = 27, Email = "john@gmail.com", FirstName = "A3", LastName = "B3", ID = 3, StateCode = "NJ" });
            A.CallTo(() => fakeUserRepository.GetFilteredUsers(fakeSearchDetail)).Throws(new Exception("Error occured"));
            A.CallTo(() => fakeUserRepository.GetAllStates()).Returns(stateList);

            // Act
            searchUserController = new SearchUserController(fakeUserRepository, mapper, fakeSearchDetail, fakeMemoryCache, fakeLogger);
            actionResult = await searchUserController.GetUsersByStateOrAgeRange("NJ", 15, 99);
            okResult = actionResult as OkObjectResult;

            // Assert
            Assert.IsNull(okResult);
        }

        [TestCleanup]
        public void CleanUp()
        {
            mapper = null;
            userList = null;
            fakeUserRepository = null;
            fakeSearchDetail = null;
            searchUserController = null;
            actionResult = null;
            okResult = null;
            fakeLogger = null;
        }

    }
}
