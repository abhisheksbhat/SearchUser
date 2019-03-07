#region Namespaces
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
#endregion

namespace SearchUserAPI
{
    /// <summary>
    /// Entry point for the Application 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Static void Main
        /// </summary>
        /// <param name="args">String args</param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        ///  Creates a web host with Http pipeline as defined by the StartUp class
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
