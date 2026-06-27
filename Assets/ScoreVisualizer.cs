using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreVisualizer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    void Update()
    {
        _text.text = Mathf.Round(ObjManager.Instance.Score).ToString();
    }
}
