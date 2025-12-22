using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AuthManager : MonoBehaviour
{
    public static AuthManager Instance { get; private set; }

    FirebaseAuth m_auth;
    DatabaseReference m_dbRef;

    [SerializeField] LoginSceneUI m_MainUI;
    [SerializeField] GameObject warningPanel;
    [SerializeField] GameObject successPanel;
    [SerializeField] Text warningText;
    [SerializeField] Text successText;

    Coroutine warningCoroutine;
    Coroutine successCoroutine;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            Initialized();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Initialized()
    {
        m_auth = FirebaseAuth.DefaultInstance;
        m_dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void Login(string email, string password)
    {
        m_auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                FirebaseUser user = task.Result.User;
                LoadScene();
            }
            else
            {
                ShowWarning("이메일 또는 비밀번호가 올바르지 않습니다.");
                return;
            }
        });
    }

    public void SignUp(string email, string password)
    {
        m_auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                HandleSignUpError(task.Exception);
                return;
            }

            FirebaseUser newUser = task.Result.User;

            CreateUserData(newUser);
        });
    }

    void CreateUserData(FirebaseUser user)
    {
        string userId = user.UserId;

        var userData = new Dictionary<string, object>()
        {
            { "아이디", user.Email },
            { "lastLogin", DateTimeOffset.UtcNow.ToUnixTimeSeconds() },
            { "currency", new Dictionary<string, object> { { "essence", 0 } } },
            { "playerStats", new Dictionary<string, object>
                {
                    { "hpLevel", 0 },
                    { "atkLevel", 0 },
                    { "aspLevel", 0 },
                    { "defLevel", 0 }
                }
            }
        };

        m_dbRef.Child("users").Child(userId).UpdateChildrenAsync(userData).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompletedSuccessfully) ShowSuccess("회원가입에 성공했습니다!");
            else ShowWarning("회원가입에 실패했습니다.");
        });
    }

    void HandleSignUpError(Exception exception)
    {
        if (exception is AggregateException aggregateException)
        {
            foreach (var inner in aggregateException.InnerExceptions)
            {
                if (inner is FirebaseException firebaseEx)
                {
                    AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                    switch (errorCode)
                    {
                        case AuthError.EmailAlreadyInUse:
                            ShowWarning("이미 존재하는 이메일입니다.");
                            return;

                        case AuthError.InvalidEmail:
                            ShowWarning("이메일 형식이 올바르지 않습니다.");
                            return;

                        case AuthError.WeakPassword:
                            ShowWarning("비밀번호는 6자 이상이어야 합니다!");
                            return;

                        default:
                            Debug.LogError("SignUp ErrorCode: " + errorCode);
                            ShowWarning("회원가입에 실패했습니다.");
                            return;
                    }
                }
            }
        }

        // FirebaseException 자체를 못 찾았을 때
        ShowWarning("알 수 없는 오류가 발생했습니다.");
    }

    void ShowWarning(string message)
    {
        HideSuccess();
        if (warningCoroutine != null) StopCoroutine(warningCoroutine);

        warningCoroutine = StartCoroutine(WarningRoutine(message));
    }

    IEnumerator WarningRoutine(string message)
    {
        warningText.text = message;
        warningPanel.SetActive(true);

        yield return new WaitForSeconds(3f);

        HideWarning();
    }

    public void HideWarning()
    {
        if (warningCoroutine != null)
        {
            StopCoroutine(warningCoroutine);
            warningCoroutine = null;
        }

        warningPanel.SetActive(false);
    }

    void ShowSuccess(string message)
    {
        HideWarning();

        if (successCoroutine != null) StopCoroutine(successCoroutine);

        successCoroutine = StartCoroutine(SuccessRoutine(message));
    }

    IEnumerator SuccessRoutine(string message)
    {
        successText.text = message;
        successPanel.SetActive(true);

        yield return new WaitForSeconds(1.5f);

        HideSuccess();
    }

    public void HideSuccess()
    {
        if (successCoroutine != null)
        {
            StopCoroutine(successCoroutine);
            successCoroutine = null;
        }

        successPanel.SetActive(false);
    }


    void LoadScene()
    {
        SceneManager.LoadSceneAsync("02_Main");
    }
}