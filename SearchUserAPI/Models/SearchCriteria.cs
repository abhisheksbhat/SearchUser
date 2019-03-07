namespace SearchUserAPI.Models
{
    /// <summary>
    /// Enum to represent search criteria 
    /// </summary>
    public enum SearchCriteria
    {
        State = 0,
        AgeRange = 1,
        StateAndAgeRange=2,
        StateOrAgeRange=3
    };
}
