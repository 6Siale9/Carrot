using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<Button> _btns = new List<Button>();

    [SerializeField] private List<GameObject> _toActivate = new List<GameObject>();
    [SerializeField] private List<GameObject> _toDestroy = new List<GameObject>();

    [SerializeField] private float _cost = 20;
    private float _costMult = 2;

    [SerializeField] private List<string> _list = new List<string>();

    [SerializeField] private AstarPath _pathfinder;

    private void Next()
    {
        GameObject a = _toActivate[0];
        GameObject b = _toActivate[1];
        _toActivate.Remove(a);
        _toActivate.Remove(b);
        a.SetActive(true);
        b.SetActive(true);
        GameObject c = _toDestroy[0];
        _toDestroy.Remove(c);
        Destroy(c);
    }

    public void Btn()
    {
        if (ObjManager.Instance.Score >= _cost)
        {
            Destroy(_btns[0].gameObject);
            _btns.RemoveAt(0);
            ObjManager.Instance.Score -= _cost;
            if (_btns.Count != 0)
            {
                _cost *= _costMult;
                _costMult *= 2;
                _btns[0].gameObject.SetActive(true);
                TMP_Text text = _btns[0].GetComponentInChildren<TMP_Text>();
                text.text = _list[0] + " : " + _cost;
            }
            Next();
            _list.RemoveAt(0);
            _pathfinder.Scan();
        }
    }
}
