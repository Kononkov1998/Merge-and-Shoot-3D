using UnityEngine;

namespace Hero
{
    public class Aim : MonoBehaviour
    {
        [SerializeField] private Transform _aimTargetHelper;
        private Transform _target;

        public void SelectTarget(Transform target) => 
            _target = target;

        private void Update()
        {
            if (_target != null)
                _aimTargetHelper.position = _target.position;
        }
    }
}