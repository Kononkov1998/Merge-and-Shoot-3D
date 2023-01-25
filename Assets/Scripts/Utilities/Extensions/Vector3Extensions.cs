using UnityEngine;

namespace Utilities.Extensions
{
    public static class Vector3Extensions
    {
        public static float SqrMagnitudeTo(this Vector3 from, Vector3 to)
        {
            return Vector3.SqrMagnitude(to - from);
        }

        public static Vector3 RandomPointOnLine(this Vector3 from, Vector3 to)
        {
            return from + Random.Range(0, 1f) * (to - from);
        }

        public static void SetX(this ref Vector3 vector3, float value)
        {
            vector3.Set(value, vector3.y, vector3.z);
        }

        public static void SetY(this ref Vector3 vector3, float value)
        {
            vector3.Set(vector3.x, value, vector3.z);
        }

        public static void SetZ(this ref Vector3 vector3, float value)
        {
            vector3.Set(vector3.x, vector3.y, value);
        }
    }
}