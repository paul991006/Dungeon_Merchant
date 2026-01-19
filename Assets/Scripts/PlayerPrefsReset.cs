using UnityEngine;

public class PlayerPrefsReset : MonoBehaviour
{
    void Awake()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs 전체 삭제됨");
    }
}
