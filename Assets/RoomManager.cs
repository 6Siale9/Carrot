using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private Button _btnReception;
    [SerializeField] private Button _btn2;

    [SerializeField] private List<GameObject> _toActivate = new List<GameObject>();
    [SerializeField] private List<GameObject> _toDestroy = new List<GameObject>();

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

    public void BtnReception()
    {
        Destroy(_btnReception.gameObject);
        _btn2.gameObject.SetActive(true);
        Next();
    }
}
