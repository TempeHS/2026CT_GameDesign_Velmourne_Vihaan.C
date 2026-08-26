using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    public int currentHealth { get; private set; }

    private Animator anim;
    private bool isInvincible;

    public System.Action OnHealthChanged;   // UI heart update callback

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;
        if (currentHealth <= 0) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        OnHealthChanged?.Invoke();   // update hearts

        if (currentHealth > 0)
        {
            // hurt removed for now
            StartCoroutine(IFrames());
            currentHealth--;
        }
        else
        {
            anim.SetTrigger("die");
            // respawn or disable player later
        }
    }

    private System.Collections.IEnumerator IFrames()
    {
        isInvincible = true;

        // flashing will be added later
        yield return new WaitForSeconds(1f);

        isInvincible = false;
    }
}
