using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject requestPanel;
    public GameObject inventoryPanel;

    private void Awake()
    {
        Instance = this;
    }

    public void ToggleRequestPanel()
    {
        requestPanel.SetActive(!requestPanel.activeSelf);
    }

    public void OpenInventory()
    {
        inventoryPanel.SetActive(true);
    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
    }
}
