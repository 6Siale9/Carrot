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

    [SerializeField] private List<float> _costs = new List<float>();

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
        if (ObjManager.Instance.Score >= _costs[0])
        {
            SoundManager.Instance.PlayBuy();
            Destroy(_btns[0].gameObject);
            _btns.RemoveAt(0);
            ObjManager.Instance.Score -= _costs[0];
            if (_btns.Count != 0)
            {
                _costs.RemoveAt(0);
                _btns[0].gameObject.SetActive(true);
                TMP_Text text = _btns[0].GetComponentInChildren<TMP_Text>();
                text.text = _list[0] + " : " + _costs[0];
                _list.RemoveAt(0);
            }
            Next();
            _pathfinder.Scan();
        }
    }
}
