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
            Parser.Default.ParseArguments<Options>(args)
                .MapResult(RunService, ThrowArgumentException);
        }

        private static async Task RunService(Options arg)
        {
            Console.WriteLine($"{arg.InstanceName}: Hello World!");
        }

        private static Task ThrowArgumentException(IEnumerable<Error> arg)
        {
            Environment.Exit((int)SystemErrorCode.ERROR_INVALID_PARAMETER);
            return null;
        }
    }

    class Options
    {
        [Option('n', "name", Required=true, HelpText="Name of the instance")]
        public string InstanceName { get; set; }

        //[Option()]
    }

    internal enum SystemErrorCode
    {
        ERROR_FILE_NOT_FOUND = 2,
        ERROR_BAD_FORMAT = 11,
        ERROR_INVALID_PARAMETER = 87
    }
} 
