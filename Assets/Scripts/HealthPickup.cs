using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healthGain = 1;

    private Transform startTransform;

    public AudioSource pickupSound;
    public AudioSource rejectSound;

    void Start()
    {
        startTransform = transform;
    }

    private void Update() {
        transform.position = startTransform.position;
        transform.position += Vector3.up * 6 * Mathf.Sin(Time.time * 10) * Time.deltaTime; // Move the pickup up and down
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController.maxHealth >= playerController.health + 1)
            {
                pickupSound.Play();
                playerController.health += healthGain;
                if (playerController.health > playerController.maxHealth)
                {
                    playerController.health = playerController.maxHealth;
                }
                Destroy(gameObject);
                playerController.body.sprite = playerController.damagedSprites[Mathf.Clamp(Mathf.FloorToInt(playerController.health / playerController.maxHealth * (playerController.damagedSprites.Length - 1)), 0, playerController.damagedSprites.Length - 1)];
            } else {
                rejectSound.Play();
            }
        }
    }
}
