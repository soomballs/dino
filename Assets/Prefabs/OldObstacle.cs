using UnityEngine;
using UnityEngine.SceneManagement;

public class OldObstacle : MonoBehaviour
{
    private float leftEdge;

    private void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
    }

    private void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if(currentScene == "Game") {
           
            transform.position += OldGameManage.Instance.gameSpeed * Time.deltaTime * Vector3.left;
        }


        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }

}