public static class ItemGradeMultiplier
{
    public static float Get(ItemGrade grade)
    {
        switch (grade)
        {
            case ItemGrade.Common: return 1.0f;
            case ItemGrade.Rare: return 1.2f;
            case ItemGrade.Epic: return 1.5f;
            case ItemGrade.Legendary: return 2.0f;
            default: return 1f;
        }
    }
}
