using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healthGain = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController.maxHealth >= playerController.health + 1)
            {
                playerController.health += healthGain;
                if (playerController.health > playerController.maxHealth)
                {
                    playerController.health = playerController.maxHealth;
                }
                Destroy(gameObject);
                playerController.body.sprite = playerController.damagedSprites[Mathf.Clamp(Mathf.FloorToInt(playerController.health / playerController.maxHealth * (playerController.damagedSprites.Length - 1)), 0, playerController.damagedSprites.Length - 1)];
            }
        }
    }
}
