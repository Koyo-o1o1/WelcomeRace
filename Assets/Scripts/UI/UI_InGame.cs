using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStates;
    [SerializeField] private Slider slider;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (playerStates != null)
        {
            playerStates.onHealthChanged += UpdateHealthUI;
        }
    }

    private void UpdateHealthUI()
    {
        slider.maxValue=playerStates.GetMaxHealthValue();
        slider.value=playerStates.currentHealth;
    }
}
