using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchUserAPI.Controllers;
using SearchUserAPI.Models;
using SearchUserAPI.Repositories;
using SearchUserAPI.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SearchUserAPI.UnitTests
{
    [TestClass]
    public class SearchSearchUserRepositoryUnitTests
    {
        /// <summary>
        /// Private variables
        /// </summary>
        DbContextOptions<UserDbContext> options;
        ISearchDetail searchDetail;
        UserDbContext context;
        ISearchUserRepository searchSearchUserRepository;

        /// <summary>
        /// Initialization
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            // Configure in memory database
            options = new DbContextOptionsBuilder<UserDbContext>()
             .UseInMemoryDatabase(databaseName: "UserDB")
             .Options;

            searchDetail = A.Fake<ISearchDetail>();

            // Insert seed data into the database using one instance of the context
            context = new UserDbContext(options);
            context.User.Add(new User() { Age = 27, Email = "john@gmail.com", FirstName = "John", LastName = "B", ID = 1, StateCode = "NJ" });
            context.User.Add(new User() { Age = 37, Email = "a@gmail.com", FirstName = "A", LastName = "B", ID = 2, StateCode = "NJ" });
            context.User.Add(new User() { Age = 45, Email = "bcd@gmail.com", FirstName = "BCD", LastName = "", ID = 3, StateCode = "NY" });
            context.User.Add(new User() { Age = 20, Email = "amy@gmail.com", FirstName = "Amy", LastName = "K", ID = 4, StateCode = "NJ" });
            context.User.Add(new User() { Age = 72, Email = "mac@gmail.com", FirstName = "Mac", LastName = "B", ID = 5, StateCode = "NJ" });
            context.User.Add(new User() { Age = 97, Email = "ory@gmail.com", FirstName = "Ory", LastName = "A", ID = 6, StateCode = "NY" });
            context.User.Add(new User() { Age = 25, Email = "Jim@gmail.com", FirstName = "Jim", LastName = "K", ID = 7, StateCode = "CO" });
            context.SaveChanges();

            searchSearchUserRepository = new SearchUserRepository(context);
        }

        [TestMethod]
        public async Task SearchUserRepository_GetAllUsers_ShouldReturnAllUsers()
        {
            var results = await searchSearchUserRepository.GetAllUsers();
            Assert.AreEqual(7, results.Count);
        }

        [TestMethod]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByStateCode_ShouldReturnProperResults_Test1()
        {
            searchDetail.StateCode = "NJ";
            searchDetail.SearchCriteriaEnum = SearchCriteria.State;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
            Assert.AreEqual(4, results.Count);
        }

        [TestMethod]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByStateCode_ShouldReturnProperResults_Test2()
        {
            searchDetail.StateCode = "NY";
            searchDetail.SearchCriteriaEnum = SearchCriteria.State;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
            Assert.AreEqual(2, results.Count);
        }

        [TestMethod]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByStateCode_ShouldReturnProperResults_Test3()
        {
            searchDetail.StateCode = "CO";
            searchDetail.SearchCriteriaEnum = SearchCriteria.State;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
            Assert.AreEqual(1, results.Count);
        }

        [TestMethod]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByStateCode_ShouldNotReturnProperResults_Test1()
        {
            searchDetail.StateCode = "NJ";
            searchDetail.SearchCriteriaEnum = SearchCriteria.State;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
            Assert.AreNotEqual(5, results.Count);
        }

        [TestMethod]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByStateCode_ShouldNotReturnProperResults_Test2()
        {
            searchDetail.StateCode = "NY";
            searchDetail.SearchCriteriaEnum = SearchCriteria.State;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
            Assert.AreNotEqual(3, results.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(APIException), "Search condition is invalid:: State")]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByState_ShouldHandleException()
        {
            searchDetail.StateCode = "";
            searchDetail.SearchCriteriaEnum = SearchCriteria.State;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
        }

        [TestMethod]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByAgeRange_ShouldReturnProperResults_Test1()
        {
            searchDetail.FromAge = 15;
            searchDetail.ToAge = 45;
            searchDetail.SearchCriteriaEnum = SearchCriteria.AgeRange;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
            Assert.AreEqual(results.Count, 5);
        }

        [TestMethod]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByAgeRange_ShouldReturnProperResults_Test2()
        {
            searchDetail.FromAge = 15;
            searchDetail.ToAge = 90;
            searchDetail.SearchCriteriaEnum = SearchCriteria.AgeRange;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
            Assert.AreEqual(results.Count, 6);
        }

        [TestMethod]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByAgeRange_ShouldReturnProperResults_Test3()
        {
            searchDetail.FromAge = 15;
            searchDetail.ToAge = 99;
            searchDetail.SearchCriteriaEnum = SearchCriteria.AgeRange;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
            Assert.AreEqual(results.Count, 7);
        }

        [TestMethod]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByAgeRange_ShouldNotReturnProperResults_Test1()
        {
            searchDetail.FromAge = 15;
            searchDetail.ToAge = 45;
            searchDetail.SearchCriteriaEnum = SearchCriteria.AgeRange;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
            Assert.AreNotEqual(results.Count, 6);
        }

        [TestMethod]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByAgeRange_ShouldNotReturnProperResults_Test2()
        {
            searchDetail.FromAge = 15;
            searchDetail.ToAge = 90;
            searchDetail.SearchCriteriaEnum = SearchCriteria.AgeRange;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
            Assert.AreNotEqual(results.Count, 9);
        }

        [TestMethod]
        [ExpectedException(typeof(APIException), "Search condition is invalid:: AgeRange")]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByAgeRange_ShouldHandleException()
        {
            searchDetail.FromAge = 10;
            searchDetail.ToAge = 5;
            searchDetail.SearchCriteriaEnum = SearchCriteria.AgeRange;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
        }

        [TestMethod]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByStateAndAgeRange_ShouldReturnProperResults_Test1()
        {
            searchDetail.StateCode = "NY";
            searchDetail.FromAge = 15;
            searchDetail.ToAge = 45;
            searchDetail.SearchCriteriaEnum = SearchCriteria.StateAndAgeRange;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
            Assert.AreEqual(results.Count, 1);
        }

        [TestMethod]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByStateAndAgeRange_ShouldReturnProperResults_Test2()
        {
            searchDetail.FromAge = 15;
            searchDetail.ToAge = 90;
            searchDetail.StateCode = "NJ";
            searchDetail.SearchCriteriaEnum = SearchCriteria.StateAndAgeRange;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
            Assert.AreEqual(results.Count, 4);
        }

        [TestMethod]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByStateAndAgeRange_ShouldReturnProperResults_Test3()
        {
            searchDetail.FromAge = 15;
            searchDetail.ToAge = 70;
            searchDetail.StateCode = "NJ";
            searchDetail.SearchCriteriaEnum = SearchCriteria.StateAndAgeRange;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
            Assert.AreEqual(results.Count, 3);
        }

        [TestMethod]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByStateAndAgeRange_ShouldNotReturnProperResults_Test1()
        {
            searchDetail.FromAge = 15;
            searchDetail.ToAge = 45;
            searchDetail.StateCode = "MI";
            searchDetail.SearchCriteriaEnum = SearchCriteria.StateAndAgeRange;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
            Assert.AreNotEqual(results.Count, 1);
        }

        [TestMethod]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByStateAndAgeRange_ShouldNotReturnProperResults_Test2()
        {
            searchDetail.FromAge = 15;
            searchDetail.ToAge = 99;
            searchDetail.StateCode = "NJ";
            searchDetail.SearchCriteriaEnum = SearchCriteria.StateAndAgeRange;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
            Assert.AreNotEqual(results.Count, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(APIException), "Search condition is invalid:: AgeRange")]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByStateAndAgeRange_ShouldHandleException()
        {
            searchDetail.FromAge = 15;
            searchDetail.ToAge = 13;
            searchDetail.StateCode = "NJ";
            searchDetail.SearchCriteriaEnum = SearchCriteria.StateAndAgeRange;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
        }

        [TestMethod]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByStateOrAgeRange_ShouldReturnProperResults_Test1()
        {
            searchDetail.StateCode = "NY";
            searchDetail.FromAge = 15;
            searchDetail.ToAge = 45;
            searchDetail.SearchCriteriaEnum = SearchCriteria.StateOrAgeRange;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
            Assert.AreEqual(results.Count, 6);
        }

        [TestMethod]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByStateOrAgeRange_ShouldReturnProperResults_Test2()
        {
            searchDetail.FromAge = 15;
            searchDetail.ToAge = 90;
            searchDetail.StateCode = "NJ";
            searchDetail.SearchCriteriaEnum = SearchCriteria.StateOrAgeRange;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
            Assert.AreEqual(results.Count, 6);
        }

        [TestMethod]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByStateOrAgeRange_ShouldReturnProperResults_Test3()
        {
            searchDetail.FromAge = 15;
            searchDetail.ToAge = 70;
            searchDetail.StateCode = "NJ";
            searchDetail.SearchCriteriaEnum = SearchCriteria.StateOrAgeRange;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
            Assert.AreEqual(results.Count, 6);
        }

        [TestMethod]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByStateOrAgeRange_ShouldNotReturnProperResults_Test1()
        {
            searchDetail.FromAge = 15;
            searchDetail.ToAge = 45;
            searchDetail.StateCode = "MI";
            searchDetail.SearchCriteriaEnum = SearchCriteria.StateOrAgeRange;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
            Assert.AreNotEqual(results.Count, 1);
        }

        [TestMethod]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByStateOrAgeRange_ShouldNotReturnProperResults_Test2()
        {
            searchDetail.FromAge = 15;
            searchDetail.ToAge = 99;
            searchDetail.StateCode = "NJ";
            searchDetail.SearchCriteriaEnum = SearchCriteria.StateOrAgeRange;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
            Assert.AreNotEqual(results.Count, 5);
        }


        [TestMethod]
        [ExpectedException(typeof(APIException), "Search condition is invalid:: State")]
        public async Task SearchUserRepository_GetFilteredUsers_SearchByStateOrAgeRange_ShouldHandleException()
        {
            searchDetail.FromAge = 15;
            searchDetail.ToAge = 10;
            searchDetail.StateCode = "";
            searchDetail.SearchCriteriaEnum = SearchCriteria.StateOrAgeRange;
            var results = await searchSearchUserRepository.GetFilteredUsers(searchDetail);
        }

        [TestCleanup]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
            context = null;
            searchSearchUserRepository = null;
            options = null;
            searchDetail = null;
        }
    }
}
