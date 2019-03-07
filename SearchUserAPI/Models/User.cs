namespace SearchUserAPI.Models
{
    /// <summary>
    /// User Model object
    /// </summary>
    public class User
    {
        public int ID{get;set;}
        public string FirstName{get;set;}

        public string LastName{get;set;}
        public string Email {get;set;}
        public int Age{get;set;}
        public string StateCode{get;set;}
    }
}