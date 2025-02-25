using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    private CharacterController character;
    private AudioSource[] audioSources;
    private Vector3 direction;

    public float jumpForce = 8f;
    public float gravity = 9.81f * 2f;
    public float fastGravity = 100f;
    public float currentGravity;
    public int jumpCount;
    public bool sprinting;
    public bool isFastFalling;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
        audioSources = GetComponents<AudioSource>();
        Debug.Log("gravity: " + gravity);
        Debug.Log("fast gravity:" + fastGravity);
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update()
    {
        direction += currentGravity * Time.deltaTime * Vector3.down;

        if (checkFalling() == true)
        {
            GameManager.Instance.GameOver();
        }
        if (character.isGrounded)
        {
            direction = Vector3.down;
            jumpCount = 2;


            isFastFalling = false;
            
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            direction = Vector3.up * jumpForce;
            jumpCount -= 1;

            switch (jumpCount)
            {
                case 1:
                    audioSources[0].Play();
                    Debug.Log("one jump");
                    break;
                case 0:
                    audioSources[1].Play();
                    Debug.Log("two jump");
                    break;
            }

        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sprinting = true;
        }
        else
        {
            sprinting = false;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !character.isGrounded)
        {
            isFastFalling = true;
            Debug.Log("fast falling");
        }
        handleFastFall();

        character.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle Detected");
            GameManager.Instance.GameOver();
        }
    }

    public bool isSprinting()
    {
        return sprinting;
    }
    private void handleFastFall()
    {
        if (isFastFalling == true)
        {
            currentGravity = fastGravity;
        }
        else
        {
            currentGravity = gravity;

        }
    }

    private bool checkFalling()
    {
        if (transform.position.y < -2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
