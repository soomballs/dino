using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }

    public SpawnableObject[] objects;
    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;
    public GameObject TSpawn;
    public Player player;

    private GroundSpawn tItem;

    private void Start()
    {
        tItem = TSpawn.GetComponent<GroundSpawn>(); // Get reference to GroundSpawn script
        InvokeRepeating(nameof(CheckAndSpawn), 0f, 0.5f); // Check every 0.5 seconds
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void CheckAndSpawn()
    {
        if (tItem == null) return;

        if (tItem.isSpawning && !IsInvoking(nameof(Spawn))) // Only spawn if allowed
        {
            Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
        }
    }

    private void Spawn()
    {
        if (tItem == null || !tItem.isSpawning) return; // Stop if spawning is not allowed

        float spawnChance = Random.value;

        foreach (SpawnableObject obj in objects)
        {
            if (spawnChance < obj.spawnChance)
            {
                GameObject obstacle = Instantiate(obj.prefab);
                obstacle.transform.position += transform.position;
                break;
            }

            spawnChance -= obj.spawnChance;
        }

        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
}
