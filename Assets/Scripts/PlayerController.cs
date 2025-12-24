using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement movement;
    private AutoCombatController autoCombat;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        autoCombat = GetComponent<AutoCombatController>();
    }

    void Start()
    {
        ApplyMode();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplyMode();
    }

    void ApplyMode()
    {
        if (GameManager.Instance.combatMode == CombatMode.Main)
        {
            movement.enabled = false;      //이동 불가
            autoCombat.enabled = true;     //자동공격
        }
        else if (GameManager.Instance.combatMode == CombatMode.Dungeon)
        {
            movement.enabled = true;       //이동 가능
            autoCombat.enabled = false;    //자동공격 OFF
        }
    }
}
