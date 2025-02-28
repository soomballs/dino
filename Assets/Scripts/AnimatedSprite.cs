using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites;
    private SpriteRenderer spriteRenderer;
    private int frame;
    private int selectedOption;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption", 0);
        Debug.Log("Selected Option from OnEnable: " + selectedOption);

        if(selectedOption == 0){
            Invoke(nameof(AnimateBase), 0f);
        }
        else if (selectedOption == 1){
            Invoke(nameof(AnimateShades), 0f);
        }
        else if(selectedOption == 2)
        {
            Invoke(nameof(AnimateScar), 0f);
        }

    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void AnimateBase()
    {
        frame++;

        if (frame >= 2) {
            frame = 0;
        }

        if(frame >= 0 && frame < 2)
        {
            spriteRenderer.sprite = sprites[frame];
        }

        Invoke(nameof(AnimateBase), 1f / GameManager.Instance.gameSpeed);
    }

    private void AnimateShades()
    {
        frame++;

        if (frame >= 2) {
            frame = 0;
        }

        if(frame >= 0 && frame <= 1)
        {
            spriteRenderer.sprite = sprites[frame + 2];
        }

        Invoke(nameof(AnimateShades), 1f / GameManager.Instance.gameSpeed);
        
    }

    private void AnimateScar()
    {
        frame++;

        if (frame >= 2) {
            frame = 0;
        }

        if(frame >= 0 && frame <= 1)
        {
            spriteRenderer.sprite = sprites[frame + 4];
        }

        Invoke(nameof(AnimateScar), 1f / GameManager.Instance.gameSpeed);
        
    }

}
