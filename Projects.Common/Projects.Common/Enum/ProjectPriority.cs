using System.ComponentModel;

namespace Projects.Common.Enum
{
    public enum ProjectPriority
    {
        [DefaultValue(Normal)]
        High,
        Normal,
        Low,
    }
}
