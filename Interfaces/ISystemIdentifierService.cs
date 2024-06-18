namespace GuardianVault
{
    /// <summary>
    /// Provides methods to retrieve system hardware identifiers.
    /// </summary>
    public interface ISystemIdentifierService
    {
        /// <summary>
        /// Retrieves the CPU identifier.
        /// </summary>
        /// <returns>The CPU identifier as a string.</returns>
        string GetCpuId();

        /// <summary>
        /// Retrieves the serial number of the motherboard.
        /// </summary>
        /// <returns>The motherboard serial number as a string.</returns>
        string GetMotherboardSerialNumber();
    }
}