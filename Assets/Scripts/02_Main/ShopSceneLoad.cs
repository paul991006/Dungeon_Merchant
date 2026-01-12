using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopSceneLoad : MonoBehaviour
{
    public void LoadShopScene()
    {
        GameManager.Instance.SetMode(CombatMode.Off);
        SceneManager.LoadScene("04_Shop");
    }
}
