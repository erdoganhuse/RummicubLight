using UnityEngine;

namespace Helper.Extensions
{
    public static class ColorExtensionMethods
    {
        public static Color WithAlpha(this Color source, float alpha)
        {
            return new Color(source.r, source.g, source.b, alpha);
        }
    }
}