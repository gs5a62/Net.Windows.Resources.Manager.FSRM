using FsrmLib;

namespace WindowsResourcesManager
{
    public class FsrmResult
    {
        internal FsrmResult(IFsrmQuota fsrmQuota)
        {
            IsSuccess = true;
        }

        internal FsrmResult()
        {
            IsSuccess = true;
        }

        internal FsrmResult(string errorMessage, FsrmResultStatus errorStatus)
        {
            ErrorMessage = errorMessage;
            ErrorStatus = errorStatus;
        }

        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }
        public FsrmResultStatus ErrorStatus { get; private set; }

        internal static FsrmResult Success() => new FsrmResult();
        internal static FsrmResult Failure(string errorMessage, FsrmResultStatus errorStatus) => new FsrmResult(errorMessage, errorStatus);
    }

    public class FsrmResult<IFsrmQuota> : FsrmResult
    {
        internal FsrmResult(IFsrmQuota fsrmQuota) : base()
        {
            FsrmQuota = fsrmQuota;
        }

        internal FsrmResult(string errorMessage, FsrmResultStatus errorStatus) : base(errorMessage, errorStatus)
        {
        }

        public IFsrmQuota FsrmQuota { get; private set; }

        internal static FsrmResult<IFsrmQuota> Success(IFsrmQuota fsrmQuota) => new FsrmResult<IFsrmQuota>(fsrmQuota);
        internal static new FsrmResult<IFsrmQuota> Failure(string errorMessage, FsrmResultStatus errorStatus) => new FsrmResult<IFsrmQuota>(errorMessage, errorStatus);

    }
}
