namespace SearchUserAPI.Models
{
    /// <summary>
    /// A class to hold all serach criteria
    /// </summary>
    public class SearchDetail : ISearchDetail
    {
        public int FromAge { get; set; }
        public int ToAge { get; set; }
        public bool OrConditionFlag { get; set; }
        public string StateCode { get; set; }
        public SearchCriteria SearchCriteriaEnum { get ; set; }
    }
}
