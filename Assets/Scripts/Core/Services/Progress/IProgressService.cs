using Data.Progress;
using UnityEngine;

namespace Core.Services.Progress
{
    public interface IProgressService : IService
    {
        public ProgressData Progress { get; set; }
        void RegisterSavedProgress(GameObject gameObject);
        void Cleanup();
        void LoadReadersProgress();
        void UpdateProgress();
        void RegisterSavedProgress<T>(T obj);
    }
}