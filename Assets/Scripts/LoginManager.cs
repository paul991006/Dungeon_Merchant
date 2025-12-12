using Firebase;
using Firebase.Auth;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    private void Awake()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var status = task.Result;
            if (status == DependencyStatus.Available)
            {
                Debug.Log("Firebase 초기화 성공!");
            }
            else
            {
                Debug.LogError($"Firebase 초기화 실패: {status}");
            }
        });
    }
}
