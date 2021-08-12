using UnityEngine;

namespace Extensions
{
    public static class Vector
    {
        public static Vector3 RandomPointOnSquare(this Vector3 center, float a, float b)
        {
            return new Vector3(Random.Range(center.x - a, center.x + a), center.y,
                Random.Range(center.z - b, center.z + b));
        }
    }
}