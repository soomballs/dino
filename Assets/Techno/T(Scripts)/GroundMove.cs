using UnityEngine;
using System.Collections;

public class GroundMove : MonoBehaviour
{
    private float leftEdge;
    private bool repeatAction = false;

    private MeshRenderer meshRenderer;

    public void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 12f;
    }

    public IEnumerator MoveUntilPosition(float targetPos, float duration)
    {
        repeatAction = true;

        // Move the object until it reaches the target position
        while (transform.position.x > targetPos)
        {
            transform.position += GameManager.Instance.gameSpeed * Time.deltaTime * Vector3.left;
            yield return null; // Wait until the next frame
        }


        Debug.Log("stop now");
        transform.position = transform.position;
        StartCoroutine(OffSetXTimer(duration));
        // Stop movement once the position is reached
    }

    private void Update()
    {
        if (!repeatAction) // Only move normally if not executing the coroutine
        {
            transform.position += GameManager.Instance.gameSpeed * Time.deltaTime * Vector3.left;
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
            // GameManager.Instance.GameOver();
        }
    }

    private IEnumerator OffSetXTimer(float duration)
    {
        float elapsedTime = 0f;
        float speed = GameManager.Instance.gameSpeed / transform.localScale.x;

        while (elapsedTime < duration)
        {
            Vector2 offset = meshRenderer.material.mainTextureOffset;
            offset += speed * Time.deltaTime * Vector2.right;
            meshRenderer.material.mainTextureOffset = offset;

            elapsedTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        repeatAction = false;
    }


}
