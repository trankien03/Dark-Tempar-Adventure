using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] TMP_Text healthBarText;
    [SerializeField] Slider healthBarSlider;

    DamageableForPlayer playerDamageable;


    // Start is called before the first frame update
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerDamageable = player.GetComponent<DamageableForPlayer>();

    }
    void Start()
    {
       healthBarSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.maxHealth);
       healthBarText.text = "Health: " + playerDamageable.Health + "/" + playerDamageable.maxHealth;
    }

    private void OnEnable()
    {
        playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth/maxHealth;
    }

    private void OnPlayerHealthChanged(float newHealth, float maxHealth)
    {
        healthBarSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        if (newHealth >=0) healthBarText.text = "Health: " + newHealth + "/" + maxHealth;
        else healthBarText.text = "Health: 0/" + maxHealth;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
