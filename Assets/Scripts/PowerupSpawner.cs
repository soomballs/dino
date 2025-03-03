using System.Collections;
using UnityEngine;

public class PowerupSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }

    public SpawnableObject[] powerups;
    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;
    public GameObject TSpawn;
    public Player player;
    private bool shutDown = false;

    private GroundSpawn tItem;

    private void Start()
    {
        tItem = TSpawn.GetComponent<GroundSpawn>(); // Get reference to GroundSpawn script
            if (tItem != null)
    {
        tItem.OnStopSpawning += HandleSpawningStopped; // Subscribe to the event
    }
    }

    private void OnEnable()
    {
     Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));        
    }

    private void OnDisable()
    {
        CancelInvoke();
    }


    private void Spawn()
    {

        float spawnChance = Random.value;

        foreach (SpawnableObject obj in powerups)
        {
            if (spawnChance < obj.spawnChance)
            {
                GameObject obstacle = Instantiate(obj.prefab);
                obstacle.transform.position += transform.position;
                if(shutDown) {
                    Destroy(obstacle);
                }
                break;
            }

            spawnChance -= obj.spawnChance;
        }

        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
        
    }



    private void HandleSpawningStopped()
{
    Debug.Log("no go"); // Print message when spawning stops
    CancelInvoke();
    StartCoroutine(shutCheck(1f));


}

private IEnumerator shutCheck(float duration) {
    yield return new WaitForSeconds(duration);
    GameManager.Instance.noMore = true;
    shutDown = true;
    StartCoroutine(spawnCheck(0.5f));
}

private IEnumerator spawnCheck(float duration) {
    yield return new WaitForSeconds(duration);
    shutDown = false;
    GameManager.Instance.noMore = false;
    Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
}



}