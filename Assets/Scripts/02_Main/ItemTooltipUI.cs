using UnityEngine;
using UnityEngine.UI;

public class ItemTooltipUI : MonoBehaviour
{
    public static ItemTooltipUI Instance;

    public Canvas rootCanvas;
    public GameObject panel;
    public Image icon;
    public Text nameText;
    public Text gradeText;
    public Text statText;
    public Text priceText;
    public Text durabilityText;

    RectTransform rect;

    private void Awake()
    {
        Instance = this;
        rect = GetComponent<RectTransform>();

        //È­¸é ±ôºýÀÓ ¹æÁö
        CanvasGroup cg = GetComponent<CanvasGroup>();
        if (cg == null) cg = gameObject.AddComponent<CanvasGroup>();
        cg.blocksRaycasts = false;

        Hide();
    }

    public void Show(ItemData data, ItemInstance instance, Vector2 position)
    {
        if (data == null || instance == null) return;

        icon.sprite = data.icon;
        icon.preserveAspect = true;

        nameText.text = data.itemName;
        gradeText.text = instance.grade.ToString();
        statText.text = instance.GetStatDescription();
        priceText.text = instance.price.ToString();
        durabilityText.text = instance.GetDurabilityText();

        panel.SetActive(true);
        SetPosition(position);
    }

    public void Hide()
    {
        panel.SetActive(false);
    }

    void SetPosition(Vector2 screenPos)
    {
        RectTransform canvasRect = rootCanvas.GetComponent<RectTransform>();

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            rootCanvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : rootCanvas.worldCamera,
            out Vector2 localPos
        );

        // ¸¶¿ì½º¿¡¼­ »ìÂ¦ ¶³¾î¶ß¸®±â
        localPos += new Vector2(20f, -20f);
        rect.anchoredPosition = localPos;

        ClampToCanvas(canvasRect);
    }

    void ClampToCanvas(RectTransform canvasRect)
    {
        Vector2 size = rect.sizeDelta;
        Vector2 pos = rect.anchoredPosition;

        Vector2 min = canvasRect.rect.min + size * rect.pivot;
        Vector2 max = canvasRect.rect.max - size * (Vector2.one - rect.pivot);

        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        rect.anchoredPosition = pos;
    }
}
