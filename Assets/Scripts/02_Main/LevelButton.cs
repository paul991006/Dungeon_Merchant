using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] GameObject lockIcon;
    [SerializeField] GameObject clearMark;

    public void SetState(bool unlocked, bool cleared)
    {
        button.interactable = unlocked;
        lockIcon.SetActive(!unlocked);
        clearMark.SetActive(cleared);
    }
}
