using Backups.Tools;
using BackupsExtra.Algorithms;
using BackupsExtra.Merge;

namespace BackupsExtra.Settings
{
    public class BackupExtraSettings
    {
        private IMergeInstruction _instruction;
        private IExtraAlgorithm _algorithm;

        public BackupExtraSettings(IMergeInstruction instruction, IExtraAlgorithm algorithm)
        {
            _instruction = instruction
                           ?? throw new BackupsException("Current instruction is invalid");
            _algorithm = algorithm
                         ?? throw new BackupsException("Current algorithm is invalid");
        }

        public IMergeInstruction GetMergeInstruction() => _instruction;
        public IExtraAlgorithm GetExtraAlgorithm() => _algorithm;

        public void SetMergeInstruction(IMergeInstruction instruction)
        {
            _instruction = instruction ?? throw new BackupsException("Invalid instruction to set");
        }

        public void SetExtraAlgorithm(IExtraAlgorithm algorithm)
        {
            _algorithm = algorithm ?? throw new BackupsException("Invalid algorithm to set");
        }
    }
}