using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawn : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject


    {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
        [Range(0f, 1f)]
        public float repeatChance;

    }

    public SpawnableObject[] objects;
    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    public float targetXPosition = 2.5f;

    //repeat duration
    public float minRepeat = 10f;
    public float maxRepeat = 30f;
    private bool repeatInProgress = false;


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
        if (repeatInProgress == true) { return; }

        Debug.Log("repeatInProgess: " + repeatInProgress);

        float spawnChance = Random.value;
        float repeatChance = Random.value;
        float repeatTime = Random.Range(minRepeat, maxRepeat);

        foreach (SpawnableObject obj in objects)
        {
            if (spawnChance < obj.spawnChance)
            {
                GameObject ground = Instantiate(obj.prefab);
                ground.transform.position = transform.position;

                if (repeatChance < obj.repeatChance)
                {
                    // Get the GroundMove script attached to the spawned object
                    GroundMove groundMove = ground.GetComponent<GroundMove>();

                    if (groundMove != null && repeatChance < obj.repeatChance)
                    {
                        // Start the MoveUntilPosition coroutine in the GroundMove script
                        StartCoroutine(groundMove.MoveUntilPosition(targetXPosition, repeatTime));
                        StartCoroutine(RepeatTimer(repeatTime));
                        Debug.Log("this is the duration: " + repeatTime);

                    }
                }

                break; // Exit the loop after spawning an object
            }

            Debug.Log("spawnchance is shrinking");
            spawnChance -= obj.spawnChance;
        }

        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }

private IEnumerator RepeatTimer(float time)
{
    repeatInProgress = true;
    
    // Adjust the timing logic to allow overlap
    float spawnBackupTime = time - 5f; // Send backup 10 seconds before current finishes
    bool backupSpawned = false;
    
    float elapsedTime = 0f;

    while (elapsedTime < time)
    {
        // Send the next terrain object early
        if (!backupSpawned && elapsedTime >= spawnBackupTime)
        {
            backupSpawned = true;
            Debug.Log("SENDING BACKUP");
            Invoke(nameof(Spawn), 1.5f);  // Directly call Spawn() rather than using Invoke for better control
        }

        Debug.Log($"Repeat delay: {time - elapsedTime:F2} seconds remaining");
        yield return new WaitForSeconds(1f);
        elapsedTime += 1f;
    }

    Debug.Log("Repeat delay finished. Spawning resumes.");
    repeatInProgress = false;
    Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
}

}
