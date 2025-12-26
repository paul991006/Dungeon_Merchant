using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoginInputHandler : MonoBehaviour
{
    [SerializeField] InputField emailInput;
    [SerializeField] InputField passwordInput;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GameObject current = EventSystem.current.currentSelectedGameObject;

            if (current == emailInput.gameObject || current == passwordInput.gameObject)
            {
                // 전체 선택 방지
                ClearSelection();

                TryLogin();
            }
        }
    }

    void ClearSelection()
    {
        emailInput.caretPosition = emailInput.text.Length;
        passwordInput.caretPosition = passwordInput.text.Length;
        EventSystem.current.SetSelectedGameObject(null);
    }

    void TryLogin()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            return;

        AuthManager.Instance.Login(email, password);
    }
}
