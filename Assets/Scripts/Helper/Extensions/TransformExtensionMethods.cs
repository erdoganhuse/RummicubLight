using UnityEngine;

namespace Helper.Extensions
{
    public static class TransformExtensionMethods
    {
        public static void LocalReset(this Transform source)
        {
            source.localPosition = Vector3.zero;
            source.localEulerAngles = Vector3.zero;
            source.localScale = Vector3.one;
        }
        
        public static void SetPosX(this Transform source, float posX)
        {
            source.position = source.position.WithX(posX);
        }

        public static void SetPosY(this Transform source, float posY)
        {
            source.position = source.position.WithY(posY);
        }
        
        public static void SetPosZ(this Transform source, float posZ)
        {
            source.position = source.position.WithZ(posZ);
        }
        
        public static void SetLocalPosX(this Transform source, float posX)
        {
            source.localPosition  = source.localPosition.WithX(posX);
        }

        public static void SetLocalPosY(this Transform source, float posY)
        {
            source.localPosition  = source.localPosition.WithY(posY);
        }
        
        public static void SetLocalPosZ(this Transform source, float posZ)
        {
            source.localPosition = source.localPosition.WithZ(posZ);
        }        
    }
}
