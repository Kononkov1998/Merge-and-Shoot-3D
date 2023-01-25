using UnityEngine;

namespace Hero
{
    public class HeroRoot : MonoBehaviour
    {
        [field: SerializeField] public Aim Aim { get; private set; }
        [field: SerializeField] public HeroAttack Attack { get; private set; }
        [field: SerializeField] public HealthBar HealthBar { get; private set; }
        [field: SerializeField] public HeroDeath Death { get; private set; }
    }
}