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
        public int CountAgeGt(int ageGt, string URL)
        {
            return _internsList.Interns.Count(i => i.Age > ageGt);
        }
        public int CountAgeLt(int ageLt, string URL)
        {
            return _internsList.Interns.Count(i => i.Age < ageLt);
        }
        public int Count(string URL)
        {
            return _internsList.Interns.Count;
            
        }
    }
}
