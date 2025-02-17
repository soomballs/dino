using UnityEngine;

public class GroundMove : MonoBehaviour
{
    private float leftEdge;

    public bool nextGround = false;

    private void Start()
    {
        nextGround = false;
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 13f;
    }

    private void Update()
    {
        transform.position += GameManager.Instance.gameSpeed * Time.deltaTime * Vector3.left;

        if ((transform.position.x < -8f) && (transform.position.x > -10f))
        {
            nextGround = true;
            Debug.Log("Almost gone");

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
            Debug.Log("projectile detected");
            Destroy(gameObject);
        }

        if (other.CompareTag("Player"))
        {
            Debug.Log("Obstacle Detected");
            //   GameManager.Instance.GameOver();
        }
    }


}
