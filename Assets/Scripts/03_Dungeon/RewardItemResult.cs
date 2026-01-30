[System.Serializable]
public class RewardItemResult
{
    public ItemData item;
    public ItemGrade grade;

    public RewardItemResult(ItemData item, ItemGrade grade)
    {
        this.item = item;
        this.grade = grade;
    }
}