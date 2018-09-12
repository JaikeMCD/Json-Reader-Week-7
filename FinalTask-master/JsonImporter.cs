using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mcd.McdCalendarImporter
{
    public class EventRecord
    {

        public int eventID { get; set; }
        public string template { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string location { get; set; }
        public string webLink { get; set; }
        public DateTime startDateTime { get; set; }
        public DateTime endDateTime { get; set; }
        public string dateTimeFormatted { get; set; }
        public bool allDay { get; set; }
        public string startTimeZoneOffset { get; set; }
        public string endTimeZoneOffset { get; set; }
        public bool canceled { get; set; }
        public bool openSignUp { get; set; }
        public bool reservationFull { get; set; }
        public bool pastDeadline { get; set; }
        public bool requiresPayment { get; set; }
        public bool refundsAllowed { get; set; }
        public bool waitingListAvailable { get; set; }
        public string signUpUrl { get; set; }
     /*   public string repeatingRegistration { get; set; }
        public string eventImage { get; set; }
        public string detailImage { get; set; }
        public string customFields { get; set; }
        public string permaLinkUrl { get; set; }
        public string eventActionUrl { get; set; }
        public string categoryCalendar { get; set; }
*/
     

        public class DynamicExample
        {
            public void Test()
            {
                string json = "{}";

                var dyn = JsonConvert.DeserializeObject<dynamic>(json);

                dynamic eventID = dyn.eventID;


            }
        }

            public static EventRecord Import(StreamReader reader)
            {
                string json = reader.ReadToEnd();
                EventRecord thing1 = JsonConvert.DeserializeObject<EventRecord>(json);

                return thing1;
            }
    }
}

