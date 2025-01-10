using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOSpawnScript : MonoBehaviour
{
    public float BorderPositionX;
    public float SpawnDistanceY;
    public GameObject UFOPrefab;
    public int ProbabilityNumber;
    public ScoreCounter ScoreCounter;

    private float _lastUFOPosition;

    private void Start()
    {
        _lastUFOPosition = transform.position.y;
    }

    private void Update()
    {
        if(transform.position.y >= _lastUFOPosition + SpawnDistanceY)
        {
            int probability = Random.Range(0, 11);
            if(probability > ProbabilityNumber)
            {
                var ufoInstance = Instantiate(UFOPrefab, new Vector2(Random.Range(-BorderPositionX, BorderPositionX), transform.position.y), Quaternion.identity);
                var monster = ufoInstance.GetComponent<Monsters>();
                if(monster != null)
                {
                    ScoreCounter.RegisterMonster(monster);
                }
            }
            _lastUFOPosition = transform.position.y;
        }
    }
}
