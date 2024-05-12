using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsResourcesManager_FSRM
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
