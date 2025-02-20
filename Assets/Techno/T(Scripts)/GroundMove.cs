using System.Collections;
using UnityEngine;
[RequireComponent(typeof(MeshRenderer))]
public class GroundMove : MonoBehaviour
{
    private float leftEdge;

    public bool nextGround = false;
    [Range(0f, 1f)]
    public float repeatChance;
    private bool repeatState = true;
    public bool land = false;

    private MeshRenderer meshRenderer;

    private void OnEnable()
    {

        float repeatVal = Random.value;
        if (repeatVal > repeatChance)
        {
            Debug.Log("Repeat now");
            repeatState = true;
        }
    }

    private void Start()
    {
        land = true;
        //float randY = Random.Range(-0.5f, 2.0f);
        float randY = 0.17f;
        if (GameManager.Instance.terrainReturn == true)
        {
            randY = 0.17f;
            repeatState = true;

        }
        nextGround = false;
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 13f;
        meshRenderer = GetComponent<MeshRenderer>();
        transform.position = new Vector3(transform.position.x, randY, transform.position.z);
    }

    private void Update()
    {
        if (repeatState)
        {
            if (transform.position.x < 2.5f)
            {
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
                meshMovement();
                StartCoroutine(repeatDelay(10f));
            }
            else
            {
                transform.position += GameManager.Instance.gameSpeed * Time.deltaTime * Vector3.left;
            }
        }
        else
        {
            transform.position += GameManager.Instance.gameSpeed * Time.deltaTime * Vector3.left;
        }



        if ((transform.position.x < -8f) && (transform.position.x > -10f))
        {
            nextGround = true;

        }

        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            //Debug.Log("projectile detected");
            Destroy(gameObject);
        }

        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("ground is touching thing");
            //Debug.Log("Obstacle Detected");
            //   GameManager.Instance.GameOver();
        }
    }


    public void meshMovement()
    {
        float speed = GameManager.Instance.gameSpeed / transform.localScale.x;
        Vector2 offset = meshRenderer.material.mainTextureOffset;
        offset += speed * Time.deltaTime * Vector2.right;
        meshRenderer.material.mainTextureOffset = offset;
    }

    private IEnumerator repeatDelay(float duration)
    {
        //float timer = 0f; // Timer to track how long the loop has been running

        yield return new WaitForSeconds(duration);
        /*     while (timer < duration) {
                meshMovement(); // Call your movement function
                timer += Time.deltaTime; // Increment the timer by the time passed since the last frame
                yield return null; // Wait for the next frame
            }
         */
        repeatState = false; // Set repeatState to false after the duration has passed
    }


}
