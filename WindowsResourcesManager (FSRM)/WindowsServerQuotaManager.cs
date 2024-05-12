using System;
using System.Runtime.InteropServices;
using FsrmLib;

namespace WindowsResourcesManager_FSRM
{
    /// <summary>
    /// for more info visit
    /// https://learn.microsoft.com/en-us/openspecs/windows_protocols/ms-fsrm/a6620ce9-e026-4a20-9ba2-e56280e230e0
    /// </summary>
    public class WindowsServerQuotaManager : IDisposable
    {
        private readonly IFsrmQuotaManager _quotaManager = new FsrmQuotaManager();

        public FsrmResult<IFsrmQuota> GetQuotaIfExists(string path)
        {
            try
            {
                return FsrmResult<IFsrmQuota>.Success(_quotaManager.GetQuota(path));
            }
            catch (Exception e)
            {
                FsrmResultStatus status = FsrmResultStatus.UnknownError;

                if (e.GetType() != typeof(COMException))
                    return FsrmResult<IFsrmQuota>.Failure(e.Message, status);

                string errorMessage;
                switch (e.HResult)
                {
                    case unchecked((int)0x80045301):
                        status = FsrmResultStatus.QuotaNotFound;
                        errorMessage = "The specified quota could not be found";
                        break;

                    case unchecked((int)0x80045304):
                        status = FsrmResultStatus.PathQuotaNotFound;
                        errorMessage = "The quota for the specified path could not be found";
                        break;
                    case unchecked((int)0x80045306):
                        status = FsrmResultStatus.PathTooLong;
                        errorMessage = "The content of the path parameter exceeds the maximum length of 4,000 characters";
                        break;
                    case unchecked((int)0x80070057):
                        status = FsrmResultStatus.NullParameter;
                        errorMessage = "The path parameter is NULL or the quota parameter is NULL";
                        break;

                    default:
                        status = FsrmResultStatus.UnknownError;
                        errorMessage = "Unknown error";
                        break;
                }

                return FsrmResult<IFsrmQuota>.Failure(errorMessage, status);
            }
        }


        public FsrmResult CreateOrUpdateQuota(string path, long limitInBytes, string description = null)
        {
            try
            {
                var quotaResult = GetQuotaIfExists(path);
                IFsrmQuota quota = null;

                if (quotaResult.ErrorStatus is FsrmResultStatus.QuotaNotFound || quotaResult.ErrorStatus is FsrmResultStatus.PathQuotaNotFound)
                    quota = _quotaManager.CreateQuota(path);

                if (quotaResult.IsSuccess)
                    quota = quotaResult.FsrmQuota;


                quota.QuotaLimit = limitInBytes;

                if (string.IsNullOrWhiteSpace(description) is false)
                    quota.Description = description;

                quota.Commit();

                return FsrmResult.Success();
            }
            catch (Exception e)
            {
                FsrmResultStatus status = FsrmResultStatus.UnknownError;

                if (e.GetType() != typeof(COMException))
                    return FsrmResult<IFsrmQuota>.Failure(e.Message, status);

                string errorMessage;
                switch (e.HResult)
                {
                    case unchecked((int)0x80045303):
                        status = FsrmResultStatus.PathQuotaAlreadyExists;
                        errorMessage = "The quota for the specified path already exists";
                        break;

                    case unchecked((int)0x80070057):
                        status = FsrmResultStatus.NullParameter;
                        errorMessage = "One of the quota parameters is NULL";
                        break;

                    default:
                        status = FsrmResultStatus.UnknownError;
                        errorMessage = "Unknown error";
                        break;
                }

                return FsrmResult<IFsrmQuota>.Failure(errorMessage, status);
            }
        }


        public void Dispose()
        {
            Marshal.ReleaseComObject(_quotaManager);
            Marshal.FinalReleaseComObject(_quotaManager);
        }
    }
}