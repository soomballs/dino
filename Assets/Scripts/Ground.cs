using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Ground : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        float speed = GameManager.Instance.gameSpeed / transform.localScale.x;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= 1.5f;
        }
        meshRenderer.material.mainTextureOffset += speed * Time.deltaTime * Vector2.right;
    }

}
