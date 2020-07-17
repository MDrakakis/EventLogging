using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace EventLogging
{
  class Program
  {
    static void Main(string[] args)
    {
      //Application logs
      EventLog eventLog = new EventLog("Application");

      Console.WriteLine("Reading event logs. Please wait...");

      //Only Today events with entry type of error.            
      List<EventLogEntry> filteredLogList = (List<EventLogEntry>)eventLog.Entries.OfType<EventLogEntry>()
                                            .Where(e => e.EntryType == EventLogEntryType.Error)
                                            .OrderByDescending(e => e.TimeGenerated)
                                            .ToList();

      foreach (var log in filteredLogList)
      {
        String sourse = log.Source;
        EventLogEntryType entryType = log.EntryType;
        String category = log.Category;
        DateTime timeGenerated = log.TimeGenerated;
        DateTime timeWritten = log.TimeWritten;
        String message = log.Message;

        if (timeWritten == DateTime.Today) // Display Log if timeWritten is today.
        {
          Console.WriteLine();
          Console.WriteLine($"------------------{sourse}-----------------");
          Console.WriteLine($"-- Source: {sourse}\nEntry Type: {entryType}\nCategory: {category}\nDateTime Generated: {timeGenerated}\nDateTime Writen: {timeWritten}\nMessage: {message}\n --");
          Console.WriteLine($"------------------End {sourse} Log-------------");
          Console.WriteLine();

          Console.Write("Continue ?");
          Console.ReadLine();
        }
      }
      Console.Write("Press any key to exit...");
      Console.ReadLine();
    }
  }
}
