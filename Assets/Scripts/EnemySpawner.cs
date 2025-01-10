using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float BorderPositionX;
    public float SpawnDistanceY;
    public GameObject MonsterPrefab;
    public int ProbabilityNumber;
    public ScoreCounter ScoreCounter;

    private float _lastSpawnMonsterPositionY;

    private void Start()
    {
        _lastSpawnMonsterPositionY = transform.position.y;
    }

    private void Update()
    {
        if(transform.position.y >= _lastSpawnMonsterPositionY + SpawnDistanceY)
        {
            int probability = Random.Range(0, 11);
            if(probability > ProbabilityNumber)
            {
                var monsterInstance = Instantiate(MonsterPrefab, new Vector2(PlatformSpawner.PlatformPosition.position.x, PlatformSpawner.PlatformPosition.position.y + 0.35f), Quaternion.identity);
                var monsterToRegister = monsterInstance.GetComponent<Monsters>();
                if(monsterToRegister != null)
                {
                    ScoreCounter.RegisterMonster(monsterToRegister);
                }
            }
            _lastSpawnMonsterPositionY = transform.position.y;
        }
    }
}
