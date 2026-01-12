using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneLoad : MonoBehaviour
{
    public void LoadMainScene()
    {
        GameManager.Instance.SetMode(CombatMode.Main);
        SceneManager.LoadScene("02_Main");
    }
}
