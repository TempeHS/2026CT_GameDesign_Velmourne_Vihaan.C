using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    public int currentHealth { get; private set; }
    public Image[] heartImages;

    [Header("iFrames")]
    [SerializeField] private float invincibilityDuration = 1f;
    [SerializeField] private int numberOfFlashes = 4;
    private SpriteRenderer spriteRend;

    private Animator anim;
    private bool isInvincible;

    public System.Action OnHealthChanged;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible) return;
        if (currentHealth <= 0) return;

        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        OnHealthChanged?.Invoke();

        if (currentHealth >= 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(IFrames());
            UpdateHearts();
            print(currentHealth);
        }

        if (currentHealth <= 0)
        {
            anim.SetTrigger("die");
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

        // FLASHING EFFECT 
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);   // red + transparent
            yield return new WaitForSeconds(invincibilityDuration / (numberOfFlashes * 2));

            spriteRend.color = Color.white;               // normal
            yield return new WaitForSeconds(invincibilityDuration / (numberOfFlashes * 2));
        }

        isInvincible = false;
    }
}
