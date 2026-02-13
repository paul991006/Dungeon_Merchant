using UnityEngine;
using UnityEngine.UI;

public class ItemTooltipUI : MonoBehaviour
{
    public static ItemTooltipUI Instance;

    public Canvas rootCanvas;
    public GameObject panel;
    public GameObject smithBtnGroup;
    public Image background;
    public Image icon;
    public Text nameText;
    public Text statText;
    public Button repairBtn;
    public Button upgradeBtn;

    private CanvasGroup cg;
    private ItemInstance currentInstance;
    RectTransform rect;

    private void Awake()
    {
        Instance = this;
        rect = GetComponent<RectTransform>();

        //화면 깜빡임 방지
        cg = panel.GetComponent<CanvasGroup>();
        if (cg == null) cg = gameObject.AddComponent<CanvasGroup>();

        Hide();
    }

    public void Show(ItemData data, ItemInstance instance, Vector2 position)
    {
        if (data == null || instance == null) return;

        Color gradeColor = ItemGradeColorUtil.GetColor(instance.grade);

        currentInstance = instance;

        icon.sprite = data.icon;
        icon.preserveAspect = true;
        background.color = gradeColor;

        nameText.text = $"{data.itemName}\n({instance.grade})";
        nameText.color = gradeColor;
        statText.text =
            instance.GetStatDescription() +
            $"\n내구도 : {instance.GetDurabilityText()}" +
            $"\n가격 : {InventoryManager.CalculateFinalPrice(instance, 1f)}G";

        panel.SetActive(true);

        if (BlacksmithManager.Instance != null && BlacksmithManager.Instance.isBlacksmithMode)
        {
            smithBtnGroup.SetActive(true);
            cg.blocksRaycasts = true;
        }
        else
        {
            smithBtnGroup.SetActive(false);
            cg.blocksRaycasts = false;
        }
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    public void OnClickRepair()
    {
        if (currentInstance == null) return;
        BlacksmithManager.Instance.TryRepair(currentInstance);
        Hide();
    }

    public void OnClickUpgrade()
    {
        if (currentInstance == null) return;
        BlacksmithManager.Instance.TryUpgrade(currentInstance);
        Hide();
    }
}
