using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceSpawn : MonoBehaviour
{
    [SerializeField] private GameObject audioSourcePrefab = null;
    [SerializeField] float spawnTime = 2.0f; //at what time does the first spawn occur
    [SerializeField] float spawnDelay = 10.0f; //spawn rate in seconds

    private void Start()
    {
        InvokeRepeating("SpawnObject", spawnTime, spawnDelay);
    }

    public void SpawnObject()
    {
        Vector3 randomPosition = new Vector3(Random.Range(-2.0f, 2.0f), 0, Random.Range(-2.0f, 2.0f)); //x between -2 and 2; y = 0; z between -2 and 2
        Instantiate(audioSourcePrefab, randomPosition, Quaternion.identity);
    }
}
