#region Namespaces
using System.ComponentModel.DataAnnotations;
#endregion

namespace SearchUserAPI.Models
{
    /// <summary>
    /// State Entity
    /// </summary>
    public class State
    {
        [Key]
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
