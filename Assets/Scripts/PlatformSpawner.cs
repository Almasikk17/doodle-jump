using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject PlatformPrefab;
    public float SpawnBorderX;
    public float SpawnDistanceY;
    public int StartPlatormsCount;
    public Vector2 StartSpawnPosition;
    public float NewSpawnPosition;
    [Header("---------BreakingPlatformConfig---------")]
    public GameObject DestroyPlatformPrefab;
    public float SpawnIntervalY;
    public int BreakingPlatformsCount;
    public static Transform PlatformPosition => _lastPlatform;
    [Header("-----------MovePlatformConfig-----------")]
    public GameObject MovePlatformPrefab;
    public float SpawnLongY;
    public float SpawnMovePlatformBorderX;
    public float MinusSpawnMovePlatformBorderX;
    [Header("----------------BlackHole---------------")]
    public GameObject BlackHolePrefab;
    public float SpawnBHLong;
    public float SpawnBlacHolePosition;

    private static Transform _lastPlatform;
    private float _lastPlatformSpawnPosition;
    private float _lastBreakingPlatformSpawnPosition;
    private float _lastMovePlatformPosition;
    private float _lastBlackHolePosition;

    private void Start()
    {
        _lastPlatformSpawnPosition = transform.position.y;

        for(int i = 0; i <  StartPlatormsCount; i++)
        {
            float positionX = Random.Range(-SpawnBorderX, SpawnBorderX);
            float positionY = StartSpawnPosition.y + SpawnDistanceY * i;
            Vector2 spawnPosition = new Vector2(positionX, positionY);
            _lastPlatform = Instantiate(PlatformPrefab, spawnPosition, Quaternion.identity).transform;
        }

        /*for(int i = 0; i <  2; i++)
        {
            float positionX = Random.Range(-SpawnBorderX, SpawnBorderX);
            float positionY = StartSpawnPosition.y + SpawnDistanceY * i;
            Vector2 spawnPosition = new Vector2(positionX, positionY);
            _lastPlatform = Instantiate(DestroyPlatformPrefab, spawnPosition, Quaternion.identity).transform;
        }*/
    }

    private void Update()
    {
        if(transform.position.y > _lastPlatform.position.y + SpawnDistanceY)
        { 
            float positionX = Random.Range(-SpawnBorderX, SpawnBorderX);
            float positionY = _lastPlatform.position.y + SpawnDistanceY;
            Vector2 spawnPosition = new Vector2(positionX, positionY);
            _lastPlatform = Instantiate(PlatformPrefab, spawnPosition, Quaternion.identity).transform;
        }
        _lastPlatformSpawnPosition = transform.position.y;

        if (transform.position.y > _lastBreakingPlatformSpawnPosition + SpawnIntervalY)
        {
            for(int i = 0; i < BreakingPlatformsCount; i++)
            {
                float positionX = Random.Range(-SpawnBorderX, SpawnBorderX);
                float positionY = transform.position.y + Random.Range(1.5f, 5f);
                Vector2 spawnPosition = new Vector2(positionX, positionY);
                Instantiate(DestroyPlatformPrefab, spawnPosition, Quaternion.identity);
            }
            _lastBreakingPlatformSpawnPosition += SpawnIntervalY;
        }

        if (transform.position.y > _lastMovePlatformPosition + SpawnIntervalY)
        {
            for(int i = 0; i < Random.Range(0, 3); i++)
            {
                float positionX = Random.Range(MinusSpawnMovePlatformBorderX,SpawnMovePlatformBorderX);
                float positionY = transform.position.y + Random.Range(1.5f, 5f);
                Vector2 spawnPosition = new Vector2(positionX, positionY);
                Instantiate(MovePlatformPrefab, spawnPosition, Quaternion.identity);
            }
            _lastMovePlatformPosition += SpawnLongY;
        }

        if (transform.position.y > _lastBlackHolePosition + SpawnIntervalY)
        {
            for(int i = 0; i < Random.Range(0, 2); i++)
            {
                float positionX = Random.Range(-SpawnBlacHolePosition,SpawnBlacHolePosition);
                float positionY = transform.position.y + Random.Range(1.5f, 5f);
                Vector2 spawnPosition = new Vector2(positionX, positionY);
                Instantiate(BlackHolePrefab, spawnPosition, Quaternion.identity);
            }
            _lastBlackHolePosition += SpawnBHLong;
        }
    }
}
