using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [Header("Trap Settings")]
    [SerializeField] private float activationDelay = 1f;
    [SerializeField] private float activeTime = 1.5f;
    [SerializeField] private int damage = 1;

    private Animator anim;
    private bool isActive;
    private bool playerInside;
    private SpriteRenderer sr;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();   // needed for flashing
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInside = true;

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
        anim.SetBool("activated", true);

        
        for (int i = 0; i < 4; i++)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.1f);

            sr.color = Color.white;
            yield return new WaitForSeconds(0.1f);
        }

        // Damage player if still inside
        if (playerInside)
            playerHealth.TakeDamage(damage);

        // Fire stays active
        yield return new WaitForSeconds(activeTime);

        // Turn fire OFF
        anim.SetBool("activated", false);
        isActive = false;

        // Reset color
        sr.color = Color.white;
    }
}
