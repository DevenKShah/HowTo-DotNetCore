using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommandLine;

namespace CommandLineParsing
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options, StaticGreetingOptions>(args)
                .MapResult(
                    (Options opts) => RunService(opts), 
                    (StaticGreetingOptions opts) => RunSomeOtherService(opts),
                    ThrowArgumentException);
        }

        private static Task RunSomeOtherService(StaticGreetingOptions arg)
        {
            for(var i =  0; i < arg.GreetingRepeatCount; i++)
            {
                Console.WriteLine($"Hello crappy World!");
            }

            return Task.CompletedTask;
        }


        private static Task RunService(Options arg)
        {
            for(var i =  0; i < arg.GreetingRepeatCount; i++)
            {
                Console.WriteLine($"{arg.InstanceName}: {arg.Greeting} World!");
            }
            return Task.CompletedTask;
        }

        private static Task ThrowArgumentException(IEnumerable<Error> arg)
        {
            Environment.Exit((int)SystemErrorCode.ERROR_INVALID_PARAMETER);
            return null;
        }
    }


    [Verb("static-greeting", HelpText="Shows a default greeting")]
    class StaticGreetingOptions
    {
        [Value(0, MetaName="Greeting repeat count", Required=false, Default=1, HelpText="Second value is the number of times greeting will be shown")]
        public int GreetingRepeatCount { get; set; }

    }

    class Options
    {
        [Option('n', "name", Required=true, HelpText="Name of the instance")]
        public string InstanceName { get; set; }

        [Value(0, MetaName="The greeting", Required=true, HelpText="First value is the greeting")]
        public string Greeting { get; set; }

        [Value(1, MetaName="Greeting repeat count", Required=false, Default=1, HelpText="Second value is the number of times greeting will be shown")]
        public int GreetingRepeatCount { get; set; }
    }

    internal enum SystemErrorCode
    {
        ERROR_FILE_NOT_FOUND = 2,
        ERROR_BAD_FORMAT = 11,
        ERROR_INVALID_PARAMETER = 87
    }
} 
