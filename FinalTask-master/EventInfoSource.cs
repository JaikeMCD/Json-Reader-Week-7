using System;
using System.IO;
using Newtonsoft.Json;

namespace Mcd.McdCalendarImporter
{
    public class EventInfoSource
    {

        public Uri BaseAddress { get; set; }
        public Guid ResourceId { get; set; }
        public Guid RevisionId { get; set; }
        public DateTime LastRevision { get; set; }
        public DateTime LastImport { get; set; }
        public string ImportScript { get; set; }



        public static EventInfoSource ReadJson(string path)
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<EventInfoSource>(json);
        }

        public void WriteJson(string path)
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public void Update(string path, CKAN.Resource resource)
        {
            var r = resource.result;

            RevisionId = r.revision_id;
            LastRevision = r.last_modified;
            LastImport = DateTime.Now;

            WriteJson(path);
        }
    }
}
