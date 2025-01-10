using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPowerUpSpawner : MonoBehaviour
{
    public List<GameObject> PowerUps = new List<GameObject>(); 

    [Header("-----Probabilities-----")]
    public int PropellerHatProbability;
    public int SprigProbability;
    public int JetPackProbability;
    [Header("------Distance------")]
    public float PropellerHatSpawnDistance;
    public float SpringSpawnDistance;
    public float JetPackSpawnDistance;

    private float _lastSpawnHatItem;
    private float _lastSpawnJetpackItem;
    private float _lastSpringItem;

    private void Update()
    {
        SpawnPropellerHat();
        SpawnJetPack();
        SpawnSpring();
    }

    private void SpawnPropellerHat()
    {
        if (transform.position.y > _lastSpawnHatItem + PropellerHatSpawnDistance)
        {
            int probability = Random.Range(0, 11);
            if (probability > PropellerHatProbability)
            {
                Instantiate(PowerUps[0], new Vector2(PlatformSpawner.PlatformPosition.position.x, PlatformSpawner.PlatformPosition.position.y + 0.3f), Quaternion.identity);
            } 
            _lastSpawnHatItem += PropellerHatSpawnDistance;
        }        
    }

    private void SpawnJetPack()
    {
        if (transform.position.y > _lastSpawnJetpackItem + JetPackSpawnDistance)
        {
            int probability = Random.Range(0, 11);
            if (probability > JetPackProbability)
            {
                Instantiate(PowerUps[1], new Vector2(PlatformSpawner.PlatformPosition.position.x, PlatformSpawner.PlatformPosition.position.y + 0.3f), Quaternion.identity);
            } 
            _lastSpawnJetpackItem += JetPackSpawnDistance;
        }
    }

        private void SpawnSpring()
    {
        if (transform.position.y > _lastSpringItem + SpringSpawnDistance)
        {
            int probability = Random.Range(0, 11);
            if (probability > SprigProbability)
            {
                Instantiate(PowerUps[2], new Vector2(PlatformSpawner.PlatformPosition.position.x, PlatformSpawner.PlatformPosition.position.y + 0.3f), Quaternion.identity);
            } 
            _lastSpringItem += SpringSpawnDistance;
        }
    }
}
