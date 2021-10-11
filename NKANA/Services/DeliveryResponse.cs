using System.Collections.Generic;

namespace NKANA.Services
{
    /// <summary>
    /// Represents an email response
    /// </summary>
    public struct DeliveryResponse
    {
        public bool Succeeded { get; set; }
        public IList<string> Errors { get; set; }
    }
}
