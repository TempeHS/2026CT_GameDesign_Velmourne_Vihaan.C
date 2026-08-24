using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    public int currentHealth;

    public Image[] heartImages;   

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHearts();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            LoseHeart();
        }
    }

    void LoseHeart()
    {
        if (currentHealth > 0)
        {
            currentHealth--;
            UpdateHearts();
        }
    }

    void UpdateHearts()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = (i < currentHealth);
        }
    }
}
