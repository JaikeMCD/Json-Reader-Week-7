using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace Mcd.McdCalendarImporter
{
    public class EventInfoRecord
    {
        public string EventName { get; set; }
        public int DiaryTypeID { get; set; }
        public int DiaryThemeID { get; set; }
        public int DiaryFormatID { get; set; }
        public DateTime StartDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public DateTime InstancesEndDate { get; set; }
        public bool IsAllDay { get; set; }
        public bool IsRecurring { get; set; }
        public string Repeats { get; set; }
        public int RepeatEvery { get; set; }
        public string RepeatOnDay { get; set; }
        public string RepeatBy { get; set; }
        public string RepeatEnds { get; set; }
        public bool ManualInstances { get; set; }
        public string Description { get; set; }
        public int OwnerOutletID { get; set; }
        public int OwnerOutletServiceID { get; set; }
        public int OwnerCouncilID { get; set; }

        public string LocationTitle { get; set; }
        public string LocationAddress { get; set; }
        public string LocationState { get; set; }
        public string LocationPostcode { get; set; }
        public string LocationTimeZone { get; set; }
        public int LocationOutletID { get; set; }
        public int LocationOutletServiceID { get; set; }
        public int LocationCouncilID { get; set; }
        public double GeocodeLat { get; set; }
        public double GeocodeLong { get; set; }

        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string URL { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactSurname { get; set; }

        public int AgeFrom { get; set; }
        public int AgeTo { get; set; }
        public bool AllowRegistration { get; set; }
        public bool SendEmailOnRegistration { get; set; }
        public int AvailableSeats { get; set; }
        public string SmallImageName { get; set; }
        public string SmallImageURL { get; set; }
        public string LargeImageName { get; set; }
        public string LargeImageURL { get; set; }
        public string NextOccurence { get; set; }
        public bool AcceptedTerms { get; set; }

        public string DataProvider { get; set; }
        public string DataProviderKey { get; set; }
        public string DataProviderData { get; set; }
        public string DataProviderURL { get; set; }
        public string DataProviderStatsURL { get; set; }
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public DateTime SysStartTime { get; set; }
        public DateTime SysEndTime { get; set; }
        public string ViewableBy { get; set; }
        public int Priority { get; set; }
        public DateTime LastSeenDateTime { get; set; }


    }

    public class EventInfoOutput
    {
        public List<EventInfoRecord> OpenData { get; set; }

        public EventInfoOutput(List<EventInfoRecord> records)
        {
            OpenData = records;
        }

        public void WriteJson(string path)
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(path, json);
        }
    }
}