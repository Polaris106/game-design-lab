using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KanakoHealthBar : MonoBehaviour
{
    public Slider hp_slider;
    public Slider delayed_hp_slider;
    public float maxHealth = 100f;
    public float currentHealth;

    private float lerp_speed = 0.05f;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp_slider.value != currentHealth)
        {
            hp_slider.value = currentHealth;
        }
        if (delayed_hp_slider != null)
        {
            if (hp_slider.value != delayed_hp_slider.value)
            {
                delayed_hp_slider.value = Mathf.Lerp(delayed_hp_slider.value, currentHealth, lerp_speed);
            }
        }
        if (currentHealth <= 0)
        {

        }

    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("hp_slider: " + hp_slider.value);
    }
}
