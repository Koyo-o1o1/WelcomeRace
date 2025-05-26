using UnityEngine;
using UnityEngine.UI;

public class HealthBar_UI : MonoBehaviour
{
    private Entity entity => GetComponentInParent<Entity>();
    private Character_Stats myStats => GetComponentInParent<Character_Stats>();
    private RectTransform myTransform;
    private Slider slider;




    private void Start()
    {
        myTransform = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();



        UpdateHealthUI();
    }



    private void UpdateHealthUI()
    {
        slider.maxValue = myStats.GetMaxHealthValue();
        slider.value = myStats.currentHealth;
    }


    private void Update()
    {
        UpdateHealthUI();
    }


    private void OnEnable()
    {
        //FlipUI‚àŒÄ‚Ño‚·
        entity.onFlipped += FlipUI;
        myStats.onHealthChanged += UpdateHealthUI;
    }




    private void OnDisable()
    {
        if (entity != null)
        {
            entity.onFlipped -= FlipUI;
        }

        if (myStats != null)
        {
            myStats.onHealthChanged -= UpdateHealthUI;
        }
    }

    //HPƒo[”½“]
    private void FlipUI() => myTransform.Rotate(0, 180, 0);
}
