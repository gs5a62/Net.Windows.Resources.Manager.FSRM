using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsResourcesManager
{
    public enum FsrmResultStatus
    {
        UnknownError,
        QuotaNotFound,
        PathQuotaNotFound,
        PathQuotaAlreadyExists,
        PathTooLong,
        NullParameter
    }
}
