using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] GameObject lockIcon;

    public void SetState(bool unlocked)
    {
        button.interactable = unlocked;
        lockIcon.SetActive(!unlocked);
    }
}
