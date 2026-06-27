using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeArchive : MonoBehaviour
{
    private int _archiveLvl = 1;
    private float _cost = 100;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Obj _workstation;

    private void Start()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        _text.text = "New position at archive for only " + _cost + " ?! What a deal !";
    }

    public void Btn()
    {
        if (_archiveLvl < 5)
        {
            _archiveLvl += 1;
            _workstation.NbOfSpots += 1;
            _cost *= 2;
            UpdateText();
        }
        else
        {
            _workstation.NbOfSpots += 1;
            Destroy(gameObject);
        }
    }
}
