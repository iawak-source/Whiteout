using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float restartDelay = 1.5f;
    [SerializeField] ParticleSystem crashParticles;
    void OnTriggerEnter2D(Collider2D collision)
    {
        int LayerIndex = LayerMask.NameToLayer("Floor");
        if (collision.gameObject.layer == LayerIndex)
        {
            crashParticles.Play();
            Invoke("ReloadScene", restartDelay);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
