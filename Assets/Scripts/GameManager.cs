using UnityEngine;
using Firebase.Auth;

public enum CombatMode
{
    Off,
    Main,
    Dungeon
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public ItemDatabase itemDatabase;

    public CombatMode combatMode;

    public string userId;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            userId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;
        }
    }

    public void SetMode(CombatMode mode)
    {
        combatMode = mode;
    }
}
