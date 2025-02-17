using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
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
    public float maxSpawnRate = 1f;

    public float targetXPosition = 2.5f;

    //repeat duration
    public float minRepeat = 10f;
    public float maxRepeat = 30f;
    private bool repeatInProgress = false;

    private float repeatChance;
    public GroundMove groundMove;


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
        repeatChance = 0.5f;
        float repeatTime = Random.Range(minRepeat, maxRepeat);

        foreach (SpawnableObject obj in objects)
        {
            if (spawnChance <= obj.spawnChance)
            {
                GameObject ground = Instantiate(obj.prefab);


                MeshRenderer meshRenderer = ground.GetComponent<MeshRenderer>();

                float leftmostOffset = meshRenderer.bounds.extents.x;
                ground.transform.position = new Vector3(transform.position.x + leftmostOffset, transform.position.y, transform.position.z);

                GroundMove newGroundMove = ground.GetComponent<GroundMove>();

                if (repeatChance > obj.repeatChance)
                {
                    StartCoroutine(NormalAction(newGroundMove));
                    // repeatInProgress = true;
                }


                break; // Exit the loop after spawning an object
            }

            Debug.Log("spawnchance is shrinking");

        }

    }


    private IEnumerator NormalAction(GroundMove localGroundMove)
    {
        float elapsedTime = 0f;
        if (localGroundMove == null) yield break;

        while (!localGroundMove.exitScreenCheck)
        {
            localGroundMove.exitScreen();

            if (localGroundMove.nextGround)
            {
                Debug.Log("new one");
                elapsedTime += Time.deltaTime;

                if(elapsedTime > 5) {
                    localGroundMove.nextGround = false;
                    Spawn();
                    yield break;
                }
            }

            if (localGroundMove.offScreen == true)
            {
                Destroy(localGroundMove.gameObject);
                yield break;
            }

            yield return null;
        }

    }

    private IEnumerator nextGround(float delay)
    {
        groundMove.nextGround = false;

        if (groundMove.repeatAction == true)
        {
            Spawn();
            yield return new WaitForSeconds(delay);
            
        }



    }

}
