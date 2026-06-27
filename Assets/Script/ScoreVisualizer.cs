using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreVisualizer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_Text _text2;
    private float _savedScore = 0;
    private float _mathTimer = 1;


    void Update()
    {
        _text.text = Mathf.Round(ObjManager.Instance.Score).ToString();
        MathLogic();
    }

    private void MathLogic()
    {
        if (_mathTimer < 0)
        {
            _mathTimer = 1;
            float a = MathWave(ObjManager.Instance.Score, _savedScore);
            Formating(a);
        }
        else
        {
            _mathTimer -= Time.deltaTime;
        }
    }

    private void Formating(float value)
    {
        if (value >= 0)
        {
            float a = value * 10;
            a = Mathf.Round(a);
            a /= 10;
            _text2.text = "+" + a + " /s";
        }
        else
        {
            float a = value * 10;
            a = Mathf.Round(a);
            a /= 10;
            _text2.text = a + " /s";
        }
    }

    private float MathWave(float currentScore, float lastScore)
    {
        float a = currentScore - lastScore;
        _savedScore = currentScore;
        return a;
    }
}
