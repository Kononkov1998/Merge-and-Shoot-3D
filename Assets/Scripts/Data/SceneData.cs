using Data.Shield;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Scene Data")]
    public class SceneData : ScriptableObject
    {
        public TransformData EnemyFirstSpawnData;
        public TransformData EnemySecondSpawnData;
        public TransformData HeroSpawnData;
        public ShieldSpawnData ShieldSpawnData;
        public CameraData CameraData;
    }
}