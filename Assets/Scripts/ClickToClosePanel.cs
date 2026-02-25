using UnityEngine;
using UnityEngine.EventSystems;

public class ClickToClosePanel : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);
    }
}