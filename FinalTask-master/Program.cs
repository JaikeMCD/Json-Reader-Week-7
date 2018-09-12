﻿using System;
using System.Collections.Generic;
using CommandLine;

namespace Mcd.McdCalendarImporter
{
    [Verb("list", HelpText = "List all OpenDataSources")]
    public class ListOptions
    { }

    [Verb("info", HelpText = "Show info for OpenDataSource")]
    public class InfoOptions
    {
        [Value(0, MetaName = "id", HelpText = "OpenDataSource ID", Required = true)]
        public int? ID { get; set; }
    }

    public class Options
    {
        /*
        [Option('i', "input", Required = true, HelpText = "Config file.")]
        public string InputFile { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output file.")]
        public string OutputFile { get; set; }
        */

        [Option('i', "input", HelpText = "Config file.")]
        public string InputFile { get; set; }

        [Option('o', "output", HelpText = "Output file.")]
        public string OutputFile { get; set; }

        [Option('d', "dry-run", HelpText = "Perform import without updating.")]
        public bool DryRun { get; set; }

        [Option('f', "force", HelpText = "Force update to current resource revision.")]
        public bool Force { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("yo");
            
            var result = Parser.Default.ParseArguments<Options>(args)
            
              .WithParsed(options => EventInfoImporter.Run(options))
                             .WithNotParsed(errors => HandleErrors(errors));


            //.MapResult(
            //(ListOptions opts) => Utilities.Source.List(opts),
            //(InfoOptions opts) => Utilities.Source.Info(opts),
            //errs => 1
            //      );
            //
            Console.ReadLine();
            return;

        }

        static void HandleErrors(IEnumerable<Error> errors)
        {
        }
    }
}
