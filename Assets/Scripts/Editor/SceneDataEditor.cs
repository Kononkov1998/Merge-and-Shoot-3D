using Data;
using Data.Shield;
using Enemies;
using Hero;
using Shields;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(SceneData))]
    public class SceneDataEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var sceneData = (SceneData) target;

            if (GUILayout.Button("Construct"))
            {
                FillEnemiesSpawnData(sceneData);
                FillHeroSpawnData(sceneData);
                FillShieldSpawnData(sceneData);
                FillCameraData(sceneData);
            }

            EditorUtility.SetDirty(target);
        }

        private static void FillEnemiesSpawnData(SceneData sceneData)
        {
            EnemySpawnPoint[] spawnPoints = FindObjectsOfType<EnemySpawnPoint>();
            sceneData.EnemyFirstSpawnData = new TransformData {Position = spawnPoints[0].transform.position};
            sceneData.EnemySecondSpawnData = new TransformData {Position = spawnPoints[1].transform.position};
        }

        private static void FillHeroSpawnData(SceneData sceneData)
        {
            Transform spawnPoint = FindObjectOfType<HeroSpawnPoint>().transform;
            sceneData.HeroSpawnData = new TransformData
            {
                Position = spawnPoint.position,
                Rotation = spawnPoint.rotation
            };
        }

        private static void FillShieldSpawnData(SceneData sceneData)
        {
            var spawnPoint = FindObjectOfType<ShieldSpawnPoint>();
            Transform spawnPointTransform = spawnPoint.transform;

            sceneData.ShieldSpawnData = new ShieldSpawnData()
            {
                Transform = new TransformData
                {
                    Position = spawnPointTransform.position,
                    Rotation = spawnPointTransform.rotation
                },
                InactivePosition = spawnPoint.InactiveShieldPosition
            };
        }

        private static void FillCameraData(SceneData sceneData) =>
            sceneData.CameraData.Position = Camera.main.transform.position;
    }
}