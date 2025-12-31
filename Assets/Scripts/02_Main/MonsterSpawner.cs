using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] monsterPrefabs; //4종 몬스터 프리팹
    public Transform battlePoint;
    public Transform spawnPoint;

    void OnEnable()
    {
        Monster.OnMonsterDead += SpawnRandomMonster;
    }

    void OnDisable()
    {
        Monster.OnMonsterDead -= SpawnRandomMonster;
    }

    void Start()
    {
        SpawnRandomMonster(); //처음 시작 몬스터
    }

    void SpawnRandomMonster()
    {
        int idx = Random.Range(0, monsterPrefabs.Length);
        GameObject monster = Instantiate(
            monsterPrefabs[idx],
            spawnPoint.position,
            Quaternion.identity
        );

        MonsterMovement move = monster.GetComponent<MonsterMovement>();
        if (move != null)
        {
            move.targetPoint = battlePoint;
        }
    }
}
