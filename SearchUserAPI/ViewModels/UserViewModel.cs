namespace SearchUserAPI.ViewModels
{
    /// <summary>
    /// View Model object to store User details
    /// </summary>
    public class UserViewModel
    {
        public int ID{get;set;}
        public string Name{get;set;}
        public string Email {get;set;}
        public int Age{get;set;}
        public string StateCode{get;set;}
    }
}