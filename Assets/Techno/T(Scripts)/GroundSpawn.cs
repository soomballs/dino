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
    }
    public SpawnableObject[] objects;
    public List<GroundMove> activeGrounds = new List<GroundMove>(); // Track all ground pieces

    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    public bool isSpawning = true;

    private void OnEnable()
    {
        if(GameManager.Instance != null && (GameManager.Instance.terrainReturn == true)) {
        isSpawning = true;
        Invoke(nameof(Spawn), 2f);
        }
    }


    private void OnDisable()
    {
        CancelInvoke();
    }

    public void Spawn()
    {

        isSpawning = true;
        float spawnChance = 0f; //usual Random.value
        //also refer to tutorial concerning reducing the object spawn chance?

        foreach (SpawnableObject obj in objects)
        {
            if (spawnChance < obj.spawnChance)
            {
                GameObject ground = Instantiate(obj.prefab);
                GroundMove newGroundMove = ground.GetComponent<GroundMove>();

                if (newGroundMove != null)
                {
                    activeGrounds.Add(newGroundMove); // Store the new ground object
                }

                MeshRenderer meshRenderer = ground.GetComponent<MeshRenderer>();
                float leftmostOffset = meshRenderer.bounds.extents.x;
                ground.transform.position = new Vector3(transform.position.x + leftmostOffset, transform.position.y, transform.position.z);

                break;
            }
        }
    }

    private void Update()
    {
        for (int i = activeGrounds.Count - 1; i >= 0; i--)
        {
            if (activeGrounds[i] != null && activeGrounds[i].nextGround)
            {
                activeGrounds[i].nextGround = false; // Reset to avoid multiple spawns
                //isSpawning =  false;
                Debug.Log(activeGrounds.Count);
                Invoke(nameof(Spawn), 0.5f);
                //isSpawning = true;
                Debug.Log(activeGrounds.Count);
                activeGrounds.RemoveAt(i); // Remove once it's processed
                Debug.Log(activeGrounds.Count);
            }
        }
    }
}
