using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public AvidexManager aviman;
    [SerializeField] private List<GameObject> coastBirds;  // Interactable GameObjects to spawn
    [SerializeField]private List<GameObject> mountainBirds;
    [SerializeField]private List<GameObject> forestBirds;
    [SerializeField]private List<GameObject> urbanBirds;
    public int minSpawn = 3;        // Minimum number of objects to spawn
    public int maxSpawn = 5;       // Maximum number of objects to spawn
    public float spawnRadius = 20f;      // Radius around the player where objects can spawn
    public float destructionRadius = 25f; // Radius around player to destroy objects
    public float spawnScale = 1f;
    private List<GameObject> spawnedObjects = new List<GameObject>();  // List to track spawned objects
    private GameObject player;           // Reference to the player GameObject
    public RegionChecker ecoCheck;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        SortBirds();
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
        // EcosystemType currEco = ecoCheck.currEco;
        for (int i = 0; i < numberOfObjects; i++)
        {
            // Get a random position within the radius around the player
            Vector3 spawnPosition = GetRandomPositionWithinRadius(spawnRadius);
            GameObject birdToSpawn = null;
            
            switch(ecoCheck.currEco)
            {
                case RegionChecker.EcosystemType.Beach:
                    birdToSpawn = coastBirds[Random.Range(0, coastBirds.Count)];
                    break;
                case RegionChecker.EcosystemType.Water:
                    birdToSpawn = coastBirds[Random.Range(0, coastBirds.Count)];
                    break;
                case RegionChecker.EcosystemType.Mountain:
                    birdToSpawn = mountainBirds[Random.Range(0, mountainBirds.Count)];
                    break;
                case RegionChecker.EcosystemType.Park:
                    birdToSpawn = forestBirds[Random.Range(0, forestBirds.Count)];
                    break;
                case RegionChecker.EcosystemType.Urban:
                    birdToSpawn = urbanBirds[Random.Range(0, urbanBirds.Count)];
                    break;
                default:
                    birdToSpawn = urbanBirds[Random.Range(0, urbanBirds.Count)];
                    break;
            }

            // Check for collisions when spawning so birds do not overlap
            if(!Physics.CheckSphere(spawnPosition, 2f))
            {
                // Set a random y-axis rotation
                Quaternion randomRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                // Instantiate the object at the random position
                birdToSpawn.transform.localScale = new Vector3(spawnScale, spawnScale, spawnScale);
                GameObject spawnedObject = Instantiate(birdToSpawn, spawnPosition, randomRotation);
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

    public void SortBirds()
    {
        foreach(var bird in aviman.allEntries)
        {
            switch(bird.EcosystemType)
            {
                case EcosystemType.Forest:
                    forestBirds.Add(bird.birdPrefab);
                    break;
                case EcosystemType.Coastline:
                    coastBirds.Add(bird.birdPrefab);
                    break;
                case EcosystemType.Mountain:
                    mountainBirds.Add(bird.birdPrefab);
                    break;
                case EcosystemType.Urban:
                    urbanBirds.Add(bird.birdPrefab);
                    break;
            }
        }
    }
}
