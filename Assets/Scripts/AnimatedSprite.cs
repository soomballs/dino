using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] REGsprites;
    public Sprite[] Glasssprites;
    public Sprite[] Scarsprites;
    
    private Sprite[] sprites; // This will store the selected sprite array.
    private SpriteRenderer spriteRenderer;
    private int charID = 0; 
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the selected skin from PlayerPrefs
        charID = PlayerPrefs.GetInt("selectedOption", 0); // Default to 0 if not found

        // Assign the correct sprite array based on the selected character
        switch (charID)
        {
            case 0:
                sprites = REGsprites;
                break;
            case 1:
                sprites = Glasssprites;
                break;
            case 2:
                sprites = Scarsprites;
                break;
            default:
                sprites = REGsprites; // Fallback option
                break;
        }
    }

    private void OnEnable()
    {
        if (sprites != null && sprites.Length > 0)
        {
            Invoke(nameof(Animate), 0f);
        }
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        if (sprites == null || sprites.Length == 0) return; // Safety check

        frame++;

        if (frame >= sprites.Length)
        {
            frame = 0;
        }

        spriteRenderer.sprite = sprites[frame];

        // Adjust animation speed based on GameManager speed
        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);
    }
}
