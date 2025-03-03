using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        Vector3 startPos = player.transform.position;
        Instantiate(projectile);
    }

    
}
