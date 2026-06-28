using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiElement : MonoBehaviour
{
    private static UiElement _instance;
    public static UiElement Instance { get => _instance; set => _instance = value; }

    [SerializeField] private GameObject _worker;
    [SerializeField] private GameObject _workstation;
    [SerializeField] private DebugCursor _cursor;

    [SerializeField] private List<TMP_Text> _workerText = new List<TMP_Text>();

    private AISelfMade _currentWorker;
    private Obj _currentWorkstation;

    private void Awake()
    {
        if (UiElement.Instance == null)
        {
            UiElement.Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void BtnFire()
    {
        _cursor.UiActive = false;
        _worker.SetActive(false);
        if (_currentWorker.WorkStation != null)
        {
            _currentWorker.Unsubscribe();
        }
        Destroy(_currentWorker.gameObject);
        ObjManager.Instance.Ais.Remove(_currentWorker);
        _currentWorker = null;
    }
    public void BtnSize()
    {
        if (ObjManager.Instance.Score > 10 && _currentWorkstation.NbOfSpots < _currentWorkstation.MaxNbOfSports)
        {
            _cursor.UiActive = false;
            _workstation.SetActive(false);
            _currentWorkstation.NbOfSpots += 1;
            ObjManager.Instance.Score -= 10;
            _currentWorker = null;
        }
    }
    public void BtnComfort()
    {
        if (ObjManager.Instance.Score > 150 && _currentWorkstation.Comfort < 3)
        {
            _cursor.UiActive = false;
            _workstation.SetActive(false);
            _currentWorkstation.Comfort += 5f;
            ObjManager.Instance.Score -= 150;
            _currentWorker = null;
        }
    }
    public void BtnClose()
    {
        _workstation.SetActive(false);
        _cursor.UiActive = false;
        _worker.SetActive(false);
        _currentWorker = null;
        _currentWorkstation = null;
    }
    public void ActivateWorker(AISelfMade worker)
    {
        _cursor.UiActive = true;
        _worker.SetActive(true);
        _currentWorker = worker;

        _workerText[0].text = "Name : " + _currentWorker.Name;
        _workerText[1].text = "Lazyness : " + Mathf.Round(_currentWorker.EnergyLossMult * 100);
        _workerText[2].text = "Motivation : " + Mathf.Round(_currentWorker.EnergyGainMult * 100);
    }
    public void ActivateWorkstation(Obj workstation)
    {
        _cursor.UiActive = true;
        _workstation.SetActive(true);
        _currentWorkstation = workstation;
    }

}
