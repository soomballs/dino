using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 startPos = new Vector3(-6, -0.1f, 0);
        transform.position = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += GameManager.Instance.gameSpeed * Time.deltaTime * Vector3.right;
    }


}
