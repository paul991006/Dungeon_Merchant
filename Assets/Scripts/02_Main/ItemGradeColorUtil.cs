using UnityEngine;

public static class ItemGradeColorUtil
{
    public static Color GetColor(ItemGrade grade)
    {
        switch (grade)
        {
            case ItemGrade.Common:
                return Color.white;
            case ItemGrade.Rare:
                return Color.green;
            case ItemGrade.Epic:
                return Color.magenta;
            case ItemGrade.Unique:
                return new Color(1f, 0.85f, 0.2f); // ³ë¶û
            case ItemGrade.Legendary:
                return Color.red;
            default:
                return Color.white;
        }
    }
}
