using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        int LayerIndex = LayerMask.NameToLayer("Floor");
        if (collision.gameObject.layer == LayerIndex)
        {
            SceneManager.LoadScene(0);
        }
    }
}
