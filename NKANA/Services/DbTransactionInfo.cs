using System.Collections.Generic;

namespace NKANA.Services
{
    public struct DbTransactionInfo
    {
        public bool Succeeded { get; set; }
        public IList<string> Errors { get; set; }
    }
}
