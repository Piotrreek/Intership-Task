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
        public int GetMaxAge(string URL)
        {
            return _internsList.Interns.Max(i => i.Age);
        }
    }
}
