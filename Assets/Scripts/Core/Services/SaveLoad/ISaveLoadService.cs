using Data.Progress;

namespace Core.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        ProgressData LoadProgress();
    }
}