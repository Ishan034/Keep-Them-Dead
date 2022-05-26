using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private float currentHealth;

    [SerializeField] private float startHealth;
    [SerializeField] private Slider healthBar;
    [SerializeField] private TMP_Text healthText;


    private void Start()
    {
        currentHealth = startHealth;
        healthText.text = currentHealth.ToString();
    }

    private void Update()
    {
        healthBar.value = currentHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (startHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        currentHealth = startHealth;
    }
}
