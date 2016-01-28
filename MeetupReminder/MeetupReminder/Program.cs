using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetupReminder.Core.Services;


namespace MeetupReminder
{
    public class Program
    {
        public static DateTime UnixToDateTime(double j)
        {
            DateTime dt;
            dt = new DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
            dt = dt.AddMilliseconds(j).ToLocalTime();
            return dt;
        }
        //public static double DateTimeToUnix(DateTime dt)
        //{
        //    return (dt - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
        //}
        static void Main(string[] args)
        {

           Console.WriteLine("Enter the name of the group you want to see meetups for");
           var groupname = Console.ReadLine();
           var meetup = MeetupSevice.GetmeetupsFor(groupname).Result;
            
            for (int i = 0; i < meetup.Count; i++)

            {
                var currentTime = UnixToDateTime(meetup[i].time);
                var Time = currentTime.ToString();
                SmsService.SendSms("+18184348962", "Name " + meetup[i].name + " Location " + meetup[i].Venue.city + "," + meetup[i].Venue.address_1 + " Time " + Time);
            }

        }
    }
}
