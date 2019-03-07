﻿#region NameSpaces
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchUserAPI.Models;
using SearchUserAPI.Repositories;
using SearchUserAPI.Utility;
using SearchUserAPI.ViewModels;
#endregion

namespace SearchUserAPI.Controllers
{
    /// <summary>
    /// Web API to search User based on StateCode and Age Range
    /// </summary>
    [Route("api/[controller]")]
    public class SearchUserController : Controller
    {
        private ISearchUserRepository _userRepository;
        private List<User> _userList = null;
        private List<UserViewModel> _userVMList = null;
        private IMapper _mapper = null;
        private ISearchDetail _searchDetail;
        private readonly ILogger<SearchUserController> _log;

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="userRepository">IUserRepository</param>
        /// <param name="mapper">IMapper</param>
        public SearchUserController(ISearchUserRepository userRepository, IMapper mapper, ISearchDetail searchDetail, ILogger<SearchUserController> log)
        {
            this._userRepository = userRepository;
            this._mapper = mapper;
            this._searchDetail = searchDetail;
            this._log = log;
        }

        /// <summary>
        /// API to fetch User details based on StateCode
        /// Example Usage :
        ///                 api/searchuser/state/ny
        ///                 api/searchuser/state/NY
        /// </summary>
        /// <param name="StateCode"></param>
        /// <returns>List of Users matching the search criteria</returns>
        [HttpGet]
        [Route("state/{StateCode:alpha:length(2)}")]
        public async Task<IActionResult> GetByState(string StateCode)
        {
            try
            {
                this._searchDetail.StateCode = StateCode;
                this._searchDetail.SearchCriteriaEnum = SearchCriteria.State; ;
                this._userList = await this._userRepository.GetFilteredUsers(this._searchDetail);
                this._userVMList = this._mapper.Map<List<UserViewModel>>(this._userList);
            }
            catch (APIException ap)
            {
                _log.LogError(ap.Message + StateCode);
                return StatusCode((int)HttpStatusCode.BadRequest, ap.Message);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + " "+ StateCode.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occured while processing the API request") ;
            }
           
            return Ok(this._userVMList);
        }

        /// <summary>
        /// API to fetch User details based on Age Range 
        /// Example Usage :
        ///                 api/searchuser/agerange/15-25
        ///                 api/searchuser/AgeRange/30-40
        /// </summary>
        /// <param name="FromAge"></param>
        /// <param name="ToAge"></param>
        /// <returns>List of Users matching the search criteria</returns>
        [HttpGet]
        [Route("agerange/{FromAge:int:range(0,99)}-{ToAge:int:range(1,100)}")]
        public async Task<IActionResult> GetUsersByAgeRange(int FromAge, int ToAge)
        {
            try
            {
                // Passing StateCode as empty string so that it wont be considered for search
                this._searchDetail.FromAge = FromAge;
                this._searchDetail.ToAge = ToAge;
                this._searchDetail.SearchCriteriaEnum = SearchCriteria.AgeRange;
                this._userList = await this._userRepository.GetFilteredUsers(this._searchDetail);
                this._userVMList = this._mapper.Map<List<UserViewModel>>(this._userList);
            }
            catch (APIException ap)
            {
                _log.LogError(ap.Message + FromAge.ToString() +","+ToAge.ToString());
                return StatusCode((int)HttpStatusCode.BadRequest, ap.Message);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + " " + FromAge.ToString() + "," + ToAge.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occured while processing the API request");
            }

            return Ok(this._userVMList);
        }

        /// <summary>
        /// API to fetch User details based on StateCode And Age Range 
        /// Example Usage :
        ///                 api/searchuser/stateandagerange/NJ/15-25
        ///                 api/searchuser/stateandagerange/NY/15-40
        /// </summary>
        /// <param name="StateCode"></param>
        /// <param name="FromAge"></param>
        /// <param name="ToAge"></param>
        /// <returns>List of Users matching the search criteria</returns>
        [HttpGet]
        [Route("stateandagerange/{StateCode:alpha:length(2)}/{FromAge:int:range(0,99)}-{ToAge:int:range(1,100)}")]
        public async  Task<IActionResult>  GetUsersByStateAndAgeRange(string StateCode, int FromAge, int ToAge)
        {
            try
            {
                this._searchDetail.StateCode = StateCode;
                this._searchDetail.ToAge = ToAge;
                this._searchDetail.FromAge = FromAge;
                this._searchDetail.SearchCriteriaEnum = SearchCriteria.StateAndAgeRange;
                this._userList = await this._userRepository.GetFilteredUsers(this._searchDetail);
                this._userVMList = this._mapper.Map<List<UserViewModel>>(this._userList);
            }
            catch (APIException ap)
            {
                _log.LogError(ap.Message + StateCode +"," +FromAge.ToString() + "," + ToAge.ToString());
                return StatusCode((int)HttpStatusCode.BadRequest, ap.Message);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + " "+ StateCode + "," + FromAge.ToString() + "," + ToAge.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occured while processing the API request") ;
            }
            return Ok(this._userVMList);
        }

        /// <summary>
        /// API to fetch User details based on StateCode Or Age Range
        /// Example Usage :
        ///                 api/searchuser/stateoragerange/NJ/15-25
        ///                api/searchuser/stateoragerange/MO/15-40
        /// </summary>
        /// <param name="StateCode"></param>
        /// <param name="FromAge"></param>
        /// <param name="ToAge"></param>
        /// <returns>List of Users matching the search criteria</returns>
        [HttpGet]
        [Route("stateoragerange/{StateCode:alpha:length(2)}/{FromAge:int:range(0,99)}-{ToAge:int:range(1,100)}")]
        public async  Task<IActionResult>  GetUsersByStateOrAgeRange(string StateCode, int FromAge, int ToAge)
        {
            try
            {
                this._searchDetail.StateCode = StateCode;
                this._searchDetail.ToAge = ToAge;
                this._searchDetail.FromAge = FromAge;
                this._searchDetail.OrConditionFlag = true;
                this._searchDetail.SearchCriteriaEnum = SearchCriteria.StateOrAgeRange;
                this._userList = await this._userRepository.GetFilteredUsers(this._searchDetail);
                this._userVMList = this._mapper.Map<List<UserViewModel>>(this._userList);
            }
            catch (APIException ap)
            {
                _log.LogError(ap.Message + " " + StateCode + "," + FromAge.ToString() + "," + ToAge.ToString());
                return StatusCode((int)HttpStatusCode.BadRequest, ap.Message);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message + " " + StateCode + "," + FromAge.ToString() + "," + ToAge.ToString());
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occured while processing the API request");
            }
            return Ok(this._userVMList);
        }
    }
}
