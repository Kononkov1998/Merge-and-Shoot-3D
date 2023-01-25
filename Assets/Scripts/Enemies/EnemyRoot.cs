using UnityEngine;

namespace Enemies
{
    public class EnemyRoot : MonoBehaviour
    {
        [field: SerializeField] public EnemyAttack Attack { get; private set; }
        [field: SerializeField] public EnemyDeath Death { get; private set; }
        [field: SerializeField] public EnemyHealth Health { get; private set; }
        [field: SerializeField] public EnemyMovement Movement { get; private set; }
        [field: SerializeField] public ShootTarget ShootTarget { get; private set; }
        [field: SerializeField] public HealthBar HealthBar { get; private set; }
    }
}