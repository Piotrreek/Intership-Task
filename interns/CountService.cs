using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interns
{
    public class CountService
    {
        private readonly InternsList _internsList;
        public CountService(InternsList internsList)
        {
            _internsList = internsList;
        }
        /// <summary>
        /// Search interns list to find interns with age greater than ageGt and count them
        /// </summary>
        /// <param name="ageGt"></param>
        /// <returns>Number of interns with age greater than ageGt</returns>
        public int CountAgeGt(int ageGt)
        {
            return _internsList.Interns.Count(i => i.Age > ageGt);
        }
        /// <summary>
        /// Search interns list to find interns with age lesser than ageLt and count them
        /// </summary>
        /// <param name="ageLt"></param>
        /// <returns>Number of interns with age lesser than ageLt</returns>
        public int CountAgeLt(int ageLt)
        {
            return _internsList.Interns.Count(i => i.Age < ageLt);
        }
        /// <summary>
        /// Count number of interns in interns list
        /// </summary>
        /// <returns>Number of interns</returns>
        public int Count()
        {
            return _internsList.Interns.Count;
        }
    }
}
