using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interns
{
    public class MaxAgeService
    {
        private readonly InternsList _internsList;
        public MaxAgeService(InternsList internsList)
        {
            _internsList = internsList;
        }
        /// <summary>
        /// Search interns list to find intern with max age
        /// </summary>
        /// <returns>Max age of all interns</returns>
        public int GetMaxAge()
        {
            return _internsList.Interns.Max(i => i.Age);
        }
    }
}
