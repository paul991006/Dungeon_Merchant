using UnityEngine;
using UnityEngine.UI;

public class PasswordToggle : MonoBehaviour
{
    [SerializeField] InputField passwordInput;
    [SerializeField] Button toggleButton;

    bool isPasswordVisible = false;

    void Start()
    {
        toggleButton.onClick.AddListener(TogglePasswordVisibility);
        SetPasswordHidden();
    }

    void TogglePasswordVisibility()
    {
        isPasswordVisible = !isPasswordVisible;

        if (isPasswordVisible) SetPasswordVisible();
        else SetPasswordHidden();
    }

    void SetPasswordVisible()
    {
        passwordInput.contentType = InputField.ContentType.Standard;
        passwordInput.ForceLabelUpdate();
    }

    void SetPasswordHidden()
    {
        passwordInput.contentType = InputField.ContentType.Password;
        passwordInput.ForceLabelUpdate();
    }
}
