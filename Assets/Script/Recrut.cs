using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Recrut : MonoBehaviour
{
    [SerializeField] private GameObject _guy;
    [SerializeField] private TMP_Text _text;

    private void Update()
    {
        _text.text = "Recrut a new guy : " + ObjManager.Instance.Ais.Count * 10;
    }

    public void Btn()
    {
        if (ObjManager.Instance.Score >= ObjManager.Instance.Ais.Count * 10)
        {
            ObjManager.Instance.Score -= ObjManager.Instance.Ais.Count * 10;
            Instantiate(_guy);
        }
    }
}
