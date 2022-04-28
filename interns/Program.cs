using System;
using System.Threading.Tasks;

namespace interns
{
    public class Program
    {
        /// <summary>
        /// My example app
        /// </summary>
        /// <param name="ageGt">counts interns where age is greater than age, where age is an integer</param>
        /// <param name="ageLt"> counts interns where age is less than age, where age is an integer</param>
        static async Task Main(int ageGt = -1, int ageLt = -1, string[] args = null)
        {
            switch (args[0])
            {
                case "count":
                    var countService = new CountService(await GetFileData.GetInternsDataModels(args[1]));
                    if (ageGt > 0 && ageLt == -1)
                        Console.WriteLine(countService.CountAgeGt(ageGt));
                    else if (ageLt > 0 && ageGt == -1)
                        Console.WriteLine(countService.CountAgeLt(ageLt));
                    else if (ageGt == -1 && ageLt == -1)
                        Console.WriteLine(countService.Count());
                    else
                        Console.WriteLine("Unsupported operation");
                    break;
                case "max-age":
                    var maxAgeService = new MaxAgeService(await GetFileData.GetInternsDataModels(args[1]));
                    Console.WriteLine(maxAgeService.GetMaxAge());
                    break;
                default:
                    Console.WriteLine("Invalid command");
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
