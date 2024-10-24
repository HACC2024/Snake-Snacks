using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    
    public GameObject[] birdsToSpawn;  // Array of interactable GameObjects to spawn
    public int minSpawn = 3;        // Minimum number of objects to spawn
    public int maxSpawn = 5;       // Maximum number of objects to spawn
    public float spawnRadius = 20f;      // Radius around the player where objects can spawn
    public float destructionRadius = 25f; // Radius around player to destroy objects
    public float spawnScale = 3f;
    private List<GameObject> spawnedObjects = new List<GameObject>();  // List to track spawned objects
    private GameObject player;           // Reference to the player GameObject

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if(spawnedObjects.Count == 0)
        {
            // Randomly determine how many objects to spawn
            int numberOfObjects = Random.Range(minSpawn, maxSpawn);
            // Spawn the objects within the radius
            SpawnObjectsAroundPlayer(numberOfObjects);
        }
        // If spawned objects are outside player range, they will be destroyed
        CheckDestroy();
    }

    void SpawnObjectsAroundPlayer(int numberOfObjects)
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            // Get a random position within the radius around the player
            Vector3 spawnPosition = GetRandomPositionWithinRadius(spawnRadius);

            // RANDOMLY SELECTS, CHANGE LATER TO BIRDS SPECIFIC TO ECOSYSTEM TYPE
            GameObject birdToSpawn = birdsToSpawn[Random.Range(0, birdsToSpawn.Length)];

            // Check for collisions when spawning so birds do not overlap
            if(!Physics.CheckSphere(spawnPosition, 2f))
            {
                // Instantiate the object at the random position
                birdToSpawn.transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
                GameObject spawnedObject = Instantiate(birdToSpawn, spawnPosition, Quaternion.identity);
                spawnedObjects.Add(spawnedObject);
            }
            
        }
    }

    Vector3 GetRandomPositionWithinRadius(float radius)
    {
        // Get a random point inside a circle in 2D (for X, Z coordinates) and adjust for 3D
        Vector2 randomPoint = Random.insideUnitCircle * radius;

        // Convert the 2D random point to 3D, relative to the player's position
        Vector3 spawnPosition = new Vector3(randomPoint.x, 0, randomPoint.y) + player.transform.position;

        return spawnPosition;
    }

    void CheckDestroy()
    {
        // Loop through all spawned objects
        for (int i = spawnedObjects.Count - 1; i >= 0; i--)
        {
            if (spawnedObjects[i] != null)
            {
                float distance = Vector3.Distance(spawnedObjects[i].transform.position, player.transform.position);
                // Destroy the object if it's outside the destruction radius
                if (distance > destructionRadius)
                {
                    Destroy(spawnedObjects[i]);
                    spawnedObjects.RemoveAt(i);  // Remove from the list
                }
            }
        }
    }
}
