using System.Collections.Generic;
using Data.Progress;
using UnityEngine;

namespace Core.Services.Progress
{
    public class ProgressService : IProgressService
    {
        private readonly List<ISavedProgressReader> _progressReaders;
        private readonly List<ISavedProgressWriter> _progressWriters;

        public ProgressData Progress { get; set; }

        public ProgressService()
        {
            _progressReaders = new List<ISavedProgressReader>();
            _progressWriters = new List<ISavedProgressWriter>();
        }

        public void RegisterSavedProgress(GameObject gameObject)
        {
            foreach (ISavedProgressReader reader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
                _progressReaders.Add(reader);

            foreach (ISavedProgressWriter writer in gameObject.GetComponentsInChildren<ISavedProgressWriter>())
                _progressWriters.Add(writer);
        }

        public void RegisterSavedProgress<T>(T obj)
        {
            if (obj is ISavedProgressReader reader)
                _progressReaders.Add(reader);

            if (obj is ISavedProgressWriter writer)
                _progressWriters.Add(writer);
        }

        public void Cleanup()
        {
            _progressReaders.Clear();
            _progressWriters.Clear();
        }

        public void LoadReadersProgress()
        {
            foreach (ISavedProgressReader reader in _progressReaders)
                reader.LoadProgress(Progress);
        }

        public void UpdateProgress()
        {
            foreach (ISavedProgressWriter progressWriter in _progressWriters)
                progressWriter.UpdateProgress(Progress);
        }
    }
}