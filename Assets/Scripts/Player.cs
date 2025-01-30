using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private CharacterController character;
    private Vector3 direction;

    public float jumpForce = 8f;
    public float gravity = 9.81f * 2f;
    public int jumpCount;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update()
    {
        direction += gravity * Time.deltaTime * Vector3.down;

        if (character.isGrounded)
        {
            direction = Vector3.down;
            jumpCount = 2;

            
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            direction = Vector3.up * jumpForce;
            jumpCount -= 1;
            Debug.Log("Jump count = " + jumpCount);
            
        }

        character.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")) {
            Debug.Log("Obstacle Detected");
            GameManager.Instance.GameOver();
        }
    }

}
