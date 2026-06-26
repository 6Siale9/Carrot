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
        _worker.SetActive(false);
        _currentWorker.FindNearestWorkstation(EWorkstationType.Work);
        ObjManager.Instance.Score -= 10; // ------------------------------------------------------------------ A modifier pour eviter de partir en negatif
        _currentWorker = null;
    }
    public void BtnStick()
    {
        _worker.SetActive(false);
        _currentWorker.FindNearestWorkstation(EWorkstationType.Work);
        _currentWorker.Appreciation -= 2;
        _currentWorker = null;
    }
    public void BtnSize()
    {

    }
    public void BtnComfort()
    {

    }
    public void ActivateWorker(AISelfMade worker)
    {
        _worker.SetActive(true);
        _currentWorker = worker;
    }
    public void ActivateWorkstation(Obj workstation)
    {
        _workstation.SetActive(true);
        _currentWorkstation = workstation;
    }

}
