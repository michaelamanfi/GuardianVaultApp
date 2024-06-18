using System;
using System.Management;

namespace GuardianVault
{
    /// <summary>
    /// Provides methods to retrieve unique system identifiers.
    /// </summary>
    public class SystemIdentifierService : ISystemIdentifierService
    {
        /// <summary>
        /// Retrieves the CPU ID of the current machine.
        /// </summary>
        /// <returns>The CPU ID as a string, or null if the CPU ID cannot be retrieved.</returns>
        public string GetCpuId()
        {
            try
            {
                // Create a ManagementObjectSearcher to query for the CPU ID
                //Query source: https://learn.microsoft.com/en-us/windows/win32/cimwin32prov/win32-baseboard
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select ProcessorId from Win32_Processor"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        // Return the first CPU ID found
                        return obj["ProcessorId"]?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and print an error message
                throw new Exception($"An error occurred while retrieving the CPU ID: {ex.Message}");
            }

            // Return null if no CPU ID was found or an error occurred
            return null;
        }

        /// <summary>
        /// Retrieves the motherboard serial number of the current machine.
        /// </summary>
        /// <returns>The motherboard serial number as a string, or null if it cannot be retrieved.</returns>
        public string GetMotherboardSerialNumber()
        {
            try
            {
                // Create a ManagementObjectSearcher to query for the motherboard serial number
                //Query source: https://learn.microsoft.com/en-us/windows/win32/cimwin32prov/win32-baseboard
                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        // Return the first motherboard serial number found
                        return obj["SerialNumber"]?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and print an error message
                throw new Exception($"An error occurred while retrieving the motherboard serial number: {ex.Message}");
            }

            // Return null if no motherboard serial number was found or an error occurred
            return null;
        }       
    }
}
