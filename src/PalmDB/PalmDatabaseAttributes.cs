using System;

namespace PalmDB
{
    /// <summary>
    /// All possible attributes of a palm database.
    /// </summary>
    [Flags]
    public enum PalmDatabaseAttributes
    {
        None = 0,
        ReadOnly = 2,
        DirtyAppInfoArea = 4,
        Backup = 8,
        OkayToInstallNewer = 16,
        ForcePalmPilotResetAfterInstall = 32,
        DontAllowCopyToOtherPilot = 64
    }
}