﻿// Windows Houndify Console App.
// Dependencies: Windows 7/8/8.1 compatible APIs; System.Net.Http, System Speech, NAudio (Nuget), Newtonsoft.Json (Nuget)

using System;

namespace WoundifyConsole
{
    class Program
    {
        static int Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            try
            {
                Console.WriteLine("CommandLine:" + Environment.CommandLine);
                WoundifyShared.Options.OptionsInit().Wait();
                WoundifyShared.Commands.ProcessArgsAsync(args).Wait(); // Must use .Wait(), otherwise some await will cause Main to unexpectedly exit.
            }
            catch(Exception ex)
            {
                do
                {
                     Console.WriteLine("WoundifyConsole: Exception:" + ex.Message);
                }
                while ((ex = ex.InnerException) != null);
            }
#if false // don't exit until async SpeechToText finishes. wait by prompting user to hit a key.
            // create collection of async tasks (as in SpeechToTasks.RunAllPreferredSpeechToTextServices). don't exit until all have completed.
            if (System.Diagnostics.Debugger.IsAttached) // holds window open when in debugger
#endif
            {
                Console.WriteLine("Done. Hit any key to exit.");
                Console.ReadKey();
            }
            if (WoundifyShared.Log.logFile != null)
                WoundifyShared.Log.logFile.Close();
            return (0);
        }
    }
}
