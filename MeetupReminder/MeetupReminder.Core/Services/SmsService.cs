using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;

namespace MeetupReminder.Core.Services
{
    public class SmsService
    {
        private const string FromNumber = "+18186290414";
        private const string TwilioAccountSid = "AC2420c32f43f4146740647598a88b3633";
        private const string TwilioAuthToken = "ac48ed4f1418aa45e8c848761002908d";

        public static void SendSms(string to, string message) {


            var twilio = new TwilioRestClient(TwilioAccountSid, TwilioAuthToken);
            twilio.SendMessage(FromNumber, to, message);

        }
    }
}
