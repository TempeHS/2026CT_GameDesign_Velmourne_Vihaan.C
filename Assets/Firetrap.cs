using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [Header("Trap Settings")]
    [SerializeField] private float activationDelay = 1f;   // time before fire turns on
    [SerializeField] private float activeTime = 1.5f;       // how long fire stays on
    [SerializeField] private int damage = 1;

    private Animator anim;
    private bool isActive;          // fire is currently burning
    private bool playerInside;      // player is standing on the trap

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;

            // Start trap cycle only when player steps on it
            if (!isActive)
                StartCoroutine(ActivateTrap(collision.GetComponent<Health>()));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInside = false;
    }

    private System.Collections.IEnumerator ActivateTrap(Health playerHealth)
    {
        // Delay before fire turns on
        yield return new WaitForSeconds(activationDelay);

        // Turn fire ON
        isActive = true;
        anim.SetBool("fireOn", true);

        // Damage player if still inside
        if (playerInside)
            playerHealth.TakeDamage(damage);

        // Fire stays active
        yield return new WaitForSeconds(activeTime);

        // Turn fire OFF
        anim.SetBool("fireOn", false);
        isActive = false;
    }
}
