using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace interns
{
    /// <summary>
    /// A class to keep data of interns
    /// </summary>
    public class InternDataModel
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime InternShipStart { get; set; }
        public DateTime InternShipEnd { get; set; }
    }

    /// <summary>
    /// A class to keep list of InterDataModel objects
    /// </summary>
    public class InternsList
    {
        public List<InternDataModel> Interns { get; set; }
    }
}
