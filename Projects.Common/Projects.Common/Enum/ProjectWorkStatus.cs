using System.ComponentModel;

namespace Projects.Common.Enum
{
    public enum ProjectWorkStatus
    {
        [DefaultValue(Running)]
        Completed,
        Running,
        Pending,
        NotStarted,
        Canceled
    }
}
