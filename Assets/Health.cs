using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    public int currentHealth { get; private set; }
    public Image[] heartImages;   

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

        // Apply damage ONCE
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        // Update UI hearts
        OnHealthChanged?.Invoke();

        if (currentHealth >= 0)
        {
            anim.SetTrigger("hurt");     // play hurt animation
            StartCoroutine(IFrames());
            UpdateHearts(); 
            print(currentHealth);  // invincibility frames
        }
        
        if (currentHealth <= 0)
        {
            anim.SetTrigger("die");
            // respawn or disable player later
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = (i < currentHealth);
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
