using CSharp.Meetup.Connect;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Spring.Social.OAuth1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetupReminder.Core.Domain;

namespace MeetupReminder.Core.Services
{
    public class MeetupSevice
    {
        private const string MeetupApiKey = "3ppdpnmm95jbenp89livvl69sc";
        private const string MeetupApiSecret = "uh1gq3p1d4178omll95fci9pnu";

        private async static Task<OAuthToken> authenticate()
        {
            var meetupServiceProvider = new MeetupServiceProvider(MeetupApiKey, MeetupApiSecret);
            Console.Write("Getting request token...");
            var oauthToken = await meetupServiceProvider.OAuthOperations.FetchRequestTokenAsync("oob", null);
            Console.WriteLine("Done");
            var authenticateUrl = meetupServiceProvider.OAuthOperations.BuildAuthorizeUrl(oauthToken.Value, null);
            Console.WriteLine("Redirect user for authentication: " + authenticateUrl);
            Process.Start(authenticateUrl);
            Console.WriteLine("Enter PIN Code from Meetup authorization page:");
            var pinCode = Console.ReadLine();
            Console.Write("Getting access token...");
            var requestToken = new AuthorizedRequestToken(oauthToken, pinCode);
            var oauthAccessToken = await meetupServiceProvider.OAuthOperations.ExchangeForAccessTokenAsync(requestToken, null);
            Console.WriteLine("Done");
            return oauthAccessToken;
        }

        public static async Task<List<MeetupInfo>> GetmeetupsFor(string groupname)
        {
            var token = await authenticate();
            var meetupServiceProvider = new MeetupServiceProvider(MeetupApiKey, MeetupApiSecret);
            var meetup = meetupServiceProvider.GetApi(token.Value, token.Secret);
            var group = groupname;
            string json = await meetup.RestOperations.GetForObjectAsync<string>($"https://api.meetup.com/2/events?group_urlname={group}");

            var o = JObject.Parse(json);

            List<MeetupInfo> meetupEvents = JsonConvert.DeserializeObject<List<MeetupInfo>>(o["results"].ToString());

            return meetupEvents;
        }
    }
}
