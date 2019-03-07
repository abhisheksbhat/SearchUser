namespace SearchUserAPI.Models
{
    /// <summary>
    /// Interface for Search Criteria
    /// </summary>
    public interface ISearchDetail
    {
          int FromAge { get; set; }
          int ToAge { get; set; }
          bool OrConditionFlag { get; set; }
          string StateCode { get; set; }
          SearchCriteria SearchCriteriaEnum { get;set; }
    }
}
