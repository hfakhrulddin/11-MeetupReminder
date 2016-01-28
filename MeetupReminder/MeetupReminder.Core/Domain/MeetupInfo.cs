using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupReminder.Core.Domain
{
    public class MeetupInfo
    {

            public double time { get; set; }
            public string name { get; set; }
            public MeetupInfo2 Venue{ get; set; }

        
    }


    public class MeetupInfo2
    {

        public string city { get; set; }
        public string address_1 { get; set; }


    }
}
