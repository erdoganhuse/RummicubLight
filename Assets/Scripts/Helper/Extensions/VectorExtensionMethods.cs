using UnityEngine;

namespace Helper.Extensions
{
    public static class VectorExtensionMethods
    {
        public static Vector3 WithX(this Vector3 source, float xValue)
        {
            return new Vector3(xValue, source.y, source.z);
        }

        public static Vector3 WithY(this Vector3 source, float yValue)
        {
            return new Vector3(source.x, yValue, source.z);
        }
        
        public static Vector3 WithZ(this Vector3 source, float zValue)
        {
            return new Vector3(source.x, source.y, zValue);
        }
    }
}