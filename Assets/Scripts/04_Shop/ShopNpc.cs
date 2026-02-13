using UnityEngine;

public class ShopNpc : MonoBehaviour
{
    public Transform stopPoint;
    public float moveSpeed = 3f;

    bool hasArrived;

    void Update()
    {
        if (!hasArrived)
        {
            transform.position = Vector3.MoveTowards(transform.position, stopPoint.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, stopPoint.position) < 0.05f)
            {
                hasArrived = true;
            }
        }
    }

    private void OnMouseDown()
    {
        if (hasArrived)
        {
            UIManager.Instance.ToggleRequestPanel();
        }
    }
}
