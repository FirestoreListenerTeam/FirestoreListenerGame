using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsSpawner : MonoBehaviour
{
    public int poolSize = 10;
    public float secondsSpawn = 2.0f;

    private GameObject lightPrefab = null;
    private GameObject[] lights = null;
    private Queue<Transform> lightsQueue = new Queue<Transform>();

    void Start()
    {
        lights = new GameObject[poolSize];

        for (uint i = 0; i < poolSize; ++i)
        {
            lights[i] = Instantiate(lightPrefab, Vector3.zero, Quaternion.identity) as GameObject;

            Transform objectTransform = lights[i].GetComponent<Transform>();
            objectTransform.parent = transform;

            lightsQueue.Enqueue(objectTransform);
            lights[i].SetActive(false);
        }
    }

    void Update()
    {
        
    }

    void AddLight(Vector3 position, Quaternion rotation)
    {
        if (lightsQueue.Count == 0)
            return;

        Transform spawnedEntity = lightsQueue.Dequeue();

        spawnedEntity.gameObject.SetActive(true);
        spawnedEntity.position = position;
        spawnedEntity.rotation = rotation;

        // Put the object back to the end of the queue
        lightsQueue.Enqueue(spawnedEntity);
    }
}
