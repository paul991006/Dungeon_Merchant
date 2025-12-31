using UnityEngine;
using UnityEngine.UI;

public class TabToNextInput : MonoBehaviour
{
    [SerializeField] InputField nextInput;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            nextInput.Select();
            nextInput.ActivateInputField();
        }
    }
}
