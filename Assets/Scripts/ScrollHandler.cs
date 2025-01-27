using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollHandler : MonoBehaviour
{

    public float scroll;
    [SerializeField] GameObject credits;
    [SerializeField] GameObject scrollBar;
    // Start is called before the first frame update
    void Start()
    {
        scroll = 0;
    }

    // Update is called once per frame
    void Update()
    {
        credits.transform.position = new Vector3(0, scroll, 0);
    }

    
}
