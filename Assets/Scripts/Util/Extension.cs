using UnityEngine;

public static class Extension
{
    public static bool Contain(this LayerMask layerMask, int layer)
    {
        return ((1 << layer) & layerMask) != 0;
    }
    /// <summary>
    /// 제작 : 찬규 
    /// 헥사값 컬러 반환( 코드 순서 : RGBA )
    /// </summary>
    /// <param name="hexCode">16진수의 헥사코드값</param>
    /// <returns></returns>
    public static Color HexColor(string hexCode)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hexCode, out color))
        {
            return color;
        }

        Debug.Log("[UnityExtension::HexColor]invalid hex code - " + hexCode);
        return Color.white;
    }
}
