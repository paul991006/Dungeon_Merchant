using UnityEngine;

public class PlayerScaleController : MonoBehaviour
{
    [SerializeField] private float baseScale = 1f;

    void Awake()
    {
        ApplyScale(baseScale);
    }

    public void ApplyScale(float scale)
    {
        baseScale = scale;
        transform.localScale = Vector3.one * baseScale;
    }

    public float GetScale()
    {
        return baseScale;
    }
}
