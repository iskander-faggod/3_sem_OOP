using BackupsExtra.Merge;
using BackupsExtra.Settings;

namespace BackupsExtra.Serializer
{
    public class BackupSettingsSnapShot
    {
        public IClearLimitSnapShot ClearLimitSnapShot { get; init; }
        public IMergeInstruction MergeInstruction { get; init; }

        public BackupExtraSettings ToObject()
        {
            return new BackupExtraSettings(MergeInstruction, ClearLimitSnapShot.ToObject());
        }
    }
}