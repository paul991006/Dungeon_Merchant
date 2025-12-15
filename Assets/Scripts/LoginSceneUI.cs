using UnityEngine;
using UnityEngine.UI;

public class LoginSceneUI : MonoBehaviour
{
    [Header("Field")]
    [SerializeField] InputField emailField;
    [SerializeField] InputField passwordField;

    [Header("AuthManager")]
    [SerializeField] AuthManager m_authManager;

    public void Login()
    {
        m_authManager.Login(emailField.text, passwordField.text);
    }

    public void SignUp()
    {
        m_authManager.SignUp(emailField.text, passwordField.text);
    }
}
