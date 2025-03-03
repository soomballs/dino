using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float projSpeed = 0.5f;
    //[SerializeField] Collision coll;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPos = new Vector3(0, 0, 0);
        transform.position = startPos;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += GameManager.Instance.gameSpeed * Time.deltaTime * (Vector3.right * projSpeed);
        Vector3 projPos = this.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (!other.CompareTag("Player"))
        {
            Debug.Log("fireball collided");
            Destroy(this);
        }
        

        

    }

}
