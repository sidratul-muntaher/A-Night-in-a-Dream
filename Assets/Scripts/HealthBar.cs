using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;
    public void  SetHealthBar(int max)
    {
        slider.maxValue = max;
        fill.color =  gradient.Evaluate(1f);

    }

    public void DecreaseHealth(int h)
    {
        slider.value = h;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
