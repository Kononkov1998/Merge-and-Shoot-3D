using Enemies;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class EnemySpawnerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(EnemySpawnPoint _spawnPoint, GizmoType gizmo)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_spawnPoint.transform.position, 0.5f);
        }
    }
}