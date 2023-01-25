using Core.Services.Progress;
using Data.Progress;
using UnityEngine;

namespace Core.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService
    {
        private const string ProgressKey = "Progress";

        private readonly IProgressService _progressService;

        public SaveLoadService(IProgressService progressService) =>
            _progressService = progressService;

        public void SaveProgress()
        {
            _progressService.UpdateProgress();
            PlayerPrefs.SetString(ProgressKey, JsonUtility.ToJson(_progressService.Progress));
        }

        public ProgressData LoadProgress() =>
            JsonUtility.FromJson<ProgressData>(PlayerPrefs.GetString(ProgressKey));
    }
}