using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Mcd.McdCalendarImporter
{
    public class EventInfoImporter
    {
        public Options Options { get; set; }

        protected EventInfoSource source;

        protected CKAN.Client ckan;
        protected CKAN.Resource resource;

        protected StreamReader downloadStream;

        protected List<dynamic> sourceRecords;
        protected List<EventInfoRecord> records;

        public static void Run(Options options)
        {
            var importer = new EventInfoImporter(options);

            importer.ImportAsync().GetAwaiter().GetResult();

            try
            {
            }
            catch (Exception e)
            {
                Console.WriteLine("Importer failed: {0}", e.Message);
                Console.WriteLine(e);
            }

        }

        public EventInfoImporter(Options options)
        {
            Options = options;
        }

        public void Import()
        {
            ImportAsync().GetAwaiter().GetResult();
        }

        public CKAN.Resource.Result Test ;


        public async Task ImportAsync()
        {
            // Load source from config file.

            //LoadConfig();

            // Get Resource from CKAN.

            //await GetCKANResource();

            // Check resource revision.

            //if (!CheckResourceUpdate())
            //    return;

            // Download Resource



            resource = new CKAN.Resource();


            //await DownloadResource();

            //Stream fs = File.Open("C:\\Users\\Developer\\Desktop\\brisbane-city-council.json", FileMode.Open);

            //downloadStream = new StreamReader(fs);

            //var s = downloadStream.ReadToEnd();

            // Import resource records

            EventRecord thing1 = JsonConvert.DeserializeObject<EventRecord>(File.ReadAllText(@"C:\\Users\\Developer\\Desktop\\brisbane-city-council.json"));

            var jsontest = JsonConvert.SerializeObject(thing1, Formatting.Indented);
            Console.WriteLine(jsontest);


            ImportRecords();

            // Write converted records

            WriteRecords();

            // Update data source

            UpdateDataSource();

            // Pause

            Console.ReadKey();
        }

        public void LoadConfig()
        {
            Console.WriteLine("Loading EventInfoSource from config: {0}", Options.InputFile);

            source = EventInfoSource.ReadJson(Options.InputFile);

            if (source == null)
            {
                throw new Exception("Error opening config file.");
            }
        }


        public async Task GetCKANResource()
        {
            Console.WriteLine("Getting CKAN resource {0} from {1}", source.ResourceId, source.BaseAddress);

            ckan = new CKAN.Client(source.BaseAddress);
            resource = await ckan.GetResourceAsync(source.ResourceId);

            if (!resource.success)
            {
                throw new Exception("Error retrieving CKAN resource info.");
            }
        }

        public bool CheckResourceUpdate()
        {
            bool current = resource.result.revision_id == source.RevisionId;
            bool update = !current || Options.Force;

            if (current)
                Console.WriteLine("Last import matches current revision {0}.", source.RevisionId);

            if (Options.Force)
                Console.WriteLine("Update forced.");

            if (update)
                Console.WriteLine("Updating to revision {0}", resource.result.revision_id);

            return update;
        }

        public async Task DownloadResource()
        {
            /*if (resource.result.url == null)
                throw new Exception("No valid URL.");
                */

            // http://www.trumba.com/calendars/brisbane-city-council.json

            Uri link = new Uri("https://data.gov.au/dataset/061308ba-4c8a-4e45-914d-5893026d8c3f/resource/33a0e11c-4e24-4b01-a402-e20409aeb984/download/city-of-port-phillip-community-bus-routes.json");

            Console.WriteLine("Downloading resource {0}", link);



            downloadStream = await WebUtils.DownloadStreamAsync(link);           //resource.result.url);
        }

        public void ImportRecords()
        {
            Console.WriteLine("Importing Json records.");

            EventRecord.Import(downloadStream);

            if (sourceRecords.Count == 0)
                throw new Exception("0 records to convert.");

            //records = importScript.ConvertRecords(sourceRecords).ToList();

            Console.WriteLine("{0} records converted.", sourceRecords.Count);
        }

        public void WriteRecords()
        {
            if (Options.DryRun)
                return;

            Console.WriteLine("Writing records to {0}", Options.OutputFile);

            var output = new EventInfoOutput(records);
            output.WriteJson(Options.OutputFile);
        }

        public void UpdateDataSource()
        {
            if (Options.DryRun)
                return;

            source.Update(Options.InputFile, resource);
        }
    }
}
