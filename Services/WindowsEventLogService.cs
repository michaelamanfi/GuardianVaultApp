using System;
using System.Diagnostics;

namespace GuardianVault
{
    /// <summary>
    /// Provides functionalities to log messages to the Windows Event Log.
    /// This class implements the ILogger interface and ensures that logs
    /// can be tracked in Windows systems via Event Viewer.
    /// </summary>
    public class WindowsEventLogService : ILogger
    {
        private readonly string sourceName;
        private readonly string logName;
        private readonly string App;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsEventLogService"/> class.
        /// Sets up the event source and log name for the event log entries.
        /// </summary>
        public WindowsEventLogService()
        {
            // Set the default source name for the event logs.
            this.sourceName = "Guardian Vault";
            this.App = this.sourceName + ": ";

            // Set the default log name where events will be written.
            this.logName = "Application";

            // Ensure the event source exists before any logging is attempted.
            EnsureEventSource();
        }

        /// <summary>
        /// Ensures that the event source exists in the Event Log.
        /// If it does not exist, it creates a new event source.
        /// Note: Creating an event source requires administrative privileges.
        /// </summary>
        private void EnsureEventSource()
        {
            // Check if the event source already exists.
            if (!EventLog.SourceExists(sourceName))
            {
                // Create a new event source with the specified name and log.
                EventLog.CreateEventSource(sourceName, logName);
            }
        }

        /// <summary>
        /// Logs informational messages to the event log.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void LogInformation(string message)
        {
            // Write an informational entry to the event log.
            WriteEntry(message, EventLogEntryType.Information);
        }

        /// <summary>
        /// Logs warning messages to the event log.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void LogWarning(string message)
        {
            // Write a warning entry to the event log.
            WriteEntry(message, EventLogEntryType.Warning);
        }

        /// <summary>
        /// Logs error messages to the event log.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void LogError(string message)
        {
            // Write an error entry to the event log.
            WriteEntry(message, EventLogEntryType.Error);
        }

        /// <summary>
        /// Writes an entry to the Windows Event Log.
        /// </summary>
        /// <param name="message">The message that will be written to the log.</param>
        /// <param name="type">The type of the log entry (Information, Warning, or Error).</param>
        private void WriteEntry(string message, EventLogEntryType type)
        {
            // Create and use an EventLog instance to write an entry to the specified log.
            using (EventLog eventLog = new EventLog(logName))
            {
                // Set the source of the event log.
                eventLog.Source = sourceName;

                // Write the entry with the specified message and event type.
                eventLog.WriteEntry(App + message, type);
            }
        }
    }
}
