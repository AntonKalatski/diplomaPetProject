using Spawner;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(ZombieSpawnerEditor))]
    public class ZombieSpawnerEditor : UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(ZombieSpawner spawner, GizmoType gizmoType)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(spawner.transform.position, 1f);
        }
    }
}