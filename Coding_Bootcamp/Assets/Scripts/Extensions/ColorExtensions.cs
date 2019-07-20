using UnityEngine;

public static class ColorExtensions
{
    //Get Hex Code
    public static string ToHex(this Color color)
    {
        return "#" + ColorUtility.ToHtmlStringRGBA(color);
    }
}
