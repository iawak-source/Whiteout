using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField] PowerupSO powerup;
    PlayerController player;
    SpriteRenderer mySpriteRenderer;
    float timeLeft;
    void Start()
    {
        player = FindFirstObjectByType<PlayerController>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        timeLeft = powerup.GetTime();
    }

    void Update()
    {
        if(mySpriteRenderer.enabled == false)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                if( timeLeft <= 0)
                {
                    player.DeactivatePowerup(powerup);
                }
            }
            
        }
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Player");

        if (collision.gameObject.layer == layerIndex && mySpriteRenderer.enabled)
        {
            mySpriteRenderer.enabled = false;
            player.ActivatePowerup(powerup);
        }
    }
}
