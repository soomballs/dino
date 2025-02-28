using UnityEngine;

public class Powerup : MonoBehaviour
{
    public PowerupEffect powerupEffect;

    private float leftEdge;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.position += 1.5f * GameManager.Instance.gameSpeed * Time.deltaTime * Vector3.left;
        }
        else
        {
            transform.position += GameManager.Instance.gameSpeed * Time.deltaTime * Vector3.left;
        }
        

        if (transform.position.x < leftEdge) {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            powerupEffect.apply();

            Destroy(gameObject);
        }
    }

}
