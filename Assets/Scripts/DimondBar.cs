using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DimondBar : MonoBehaviour
{
     [SerializeField] Slider slider ;


    public void OnStartDimond(int dimond)
    {
        slider.minValue = dimond;
    }

    public void DimondOnUpdate(int dimond)
    {
        slider.value = dimond;
    }
}
