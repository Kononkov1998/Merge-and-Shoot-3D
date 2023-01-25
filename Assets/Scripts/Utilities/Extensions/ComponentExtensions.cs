using UnityEngine;

namespace Utilities.Extensions
{
    public static class ComponentExtensions
    {
        public static bool IsPrefab(this Component value)
        {
            return value.gameObject.scene.rootCount == 0;
        }
    }
}