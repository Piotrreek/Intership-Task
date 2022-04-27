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
                        Console.WriteLine(countService.CountAgeGt(ageGt, args[1]));
                    else if (ageLt > 0 && ageGt == -1)
                        Console.WriteLine(countService.CountAgeLt(ageLt, args[1]));
                    else if (ageGt == -1 && ageLt == -1)
                        Console.WriteLine(countService.Count(args[1]));
                    break;
                case "max-age":
                    var maxAgeService = new MaxAgeService(await GetFileData.GetInternsDataModels(args[1]));
                    Console.WriteLine(maxAgeService.GetMaxAge(args[1]));
                    break;
                default:
                    Console.WriteLine("Invalid command");
                    break;
            }
        }
    }
}
