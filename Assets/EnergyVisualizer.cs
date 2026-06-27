using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyVisualizer : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private AISelfMade _script;

    void Update()
    {
        _slider.value = _script.Energy;
    }
}
