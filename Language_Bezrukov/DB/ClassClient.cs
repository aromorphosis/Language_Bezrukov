using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Language_Bezrukov.DB
{
    public partial class Client
    {
        public int CountVisit
        {
            get
            {
                return ClientService.Count();
            }
        }

        public DateTime? LastVisit
        {
            get
            {
                return ClientService.LastOrDefault()?.StartDate;
            }
        }

        public List<Tag> Tags
        {
            get
            {
                return Tag.ToList();
            }
        }
    }
}
