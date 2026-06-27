using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiElement : MonoBehaviour
{
    private static UiElement _instance;
    public static UiElement Instance { get => _instance; set => _instance = value; }

    [SerializeField] private GameObject _worker;
    [SerializeField] private GameObject _workstation;
    [SerializeField] private DebugCursor _cursor;

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

    public void BtnCarrot()
    {
        _cursor.UiActive = false;
        _worker.SetActive(false);
        _currentWorker.FindNearestWorkstation(EWorkstationType.Work);
        ObjManager.Instance.Score -= 10; // ------------------------------------------------------------------ A modifier pour eviter de partir en negatif
        _currentWorker = null;
    }
    public void BtnStick()
    {
        _cursor.UiActive = false;
        _worker.SetActive(false);
        _currentWorker.FindNearestWorkstation(EWorkstationType.Work);
        _currentWorker.Appreciation -= 2;
        _currentWorker = null;
    }
    public void BtnSize()
    {
        _cursor.UiActive = false;
        _workstation.SetActive(false);
        _currentWorkstation.NbOfSpots += 1;
        ObjManager.Instance.Score -= 10; // ------------------------------------------------------------------ A modifier pour eviter de partir en negatif
        _currentWorker = null;
    }
    public void BtnComfort()
    {
        _cursor.UiActive = false;
        _workstation.SetActive(false);
        _currentWorkstation.Comfort += 1f;
        ObjManager.Instance.Score -= 10; // ------------------------------------------------------------------ A modifier pour eviter de partir en negatif
        _currentWorker = null;
    }
    public void ActivateWorker(AISelfMade worker)
    {
        _cursor.UiActive = true;
        _worker.SetActive(true);
        _currentWorker = worker;
    }
    public void ActivateWorkstation(Obj workstation)
    {
        _cursor.UiActive = true;
        _workstation.SetActive(true);
        _currentWorkstation = workstation;
    }

}
