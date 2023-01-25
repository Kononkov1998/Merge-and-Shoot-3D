using UnityEngine;

namespace Shields
{
    public class ShieldSpawnPoint : MonoBehaviour
    {
        [field: SerializeField] public Vector3 InactiveShieldPosition { get; private set; }
    }
}