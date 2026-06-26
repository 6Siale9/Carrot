using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using UnityEngine;

public class AISelfMade : MonoBehaviour
{
    [SerializeField] private AIDestinationSetter _aiDestSet;
    private bool _following = false;
    [Range(0, 10)]
    [SerializeField] private float _energy = 1f;
    private Obj _pauseClope;
    private Obj _workStation;
    private bool _working = false;
    private bool _resting = false;
    private float _appreciation = 10f;
    [SerializeField] private float _energyLossMult = 1;
    [SerializeField] private float _energyGainMult = 1;

    public float Appreciation { get => _appreciation; set => _appreciation = value; }
    public float Energy { get => _energy; set => _energy = value; }

    #region Base
    private void Start()
    {
        ObjManager.Instance.Ais.Add(this);
        Energy = Random.Range(0f, 10f);
        _energyGainMult = Random.Range(0.1f, 2f);
        _energyLossMult = Random.Range(0.1f, 2f);
        _pauseClope = ObjManager.Instance.PauseClope;
        FindNearestWorkstation(EWorkstationType.Work);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Obj>() == _workStation)
        {
            if (_workStation.Type == EWorkstationType.Work)
            {
                StartWorking();
            }
            else if (_workStation.Type == EWorkstationType.Rest)
            {
                StartResting();
            }
        }
    }

    private void Update()
    {
        WorkLogic();
        RestLogic();
        LeaveLogic();
    }
    #endregion Base

    #region Actions
    public void StartFollowing(Transform cursor)
    {
        Unsubscribe();
        _following = true;
        _aiDestSet.target = cursor;
    }

    public void ReturnToBasic()
    {
        if (_following)
        {
            FindNearestWorkstation(EWorkstationType.Work);
            _following = false;
        }
    }

    private void Subscribe(Obj obj)
    {
        _workStation = obj;
        _workStation.Subscribed.Add(this);
        _aiDestSet.target = obj.transform;
    }

    private void Unsubscribe()
    {
        _workStation?.Subscribed.Remove(this);
        _workStation = null;
        _aiDestSet.target = null;
    }

    public void FindNearestWorkstation(EWorkstationType type)
    {
        List<Obj> list = new List<Obj>();
        for (int i = 0; i < ObjManager.Instance.Objs.Count; i++)
        {
            if (ObjManager.Instance.Objs[i].Type == type && ObjManager.Instance.Objs[i].Subscribed.Count < ObjManager.Instance.Objs[i].NbOfSpots)
            {
                list.Add(ObjManager.Instance.Objs[i]);
            }
        }
        float d = 0;
        Obj o = _pauseClope;
        if (list.Count == 0)
        {
            _aiDestSet.target = o.transform;
        }
        else
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (Vector3.Distance(transform.position, ObjManager.Instance.Objs[i].transform.position) > d || d == 0)
                {
                    d = Vector3.Distance(transform.position, ObjManager.Instance.Objs[i].transform.position);
                    o = list[i];
                }
            }
            Subscribe(o);
        }
    }
    #endregion Actions

    #region WorkAndRest
    private void StartWorking()
    {
        _working = true;
    }

    private void StartResting()
    {
        _resting = true;
        _appreciation = Mathf.Clamp(_appreciation + 1, 0, 10);
    }

    private void WorkLogic()
    {
        if (_working)
        {
            ObjManager.Instance.Score += (_appreciation / 10) * Time.deltaTime / 5;
            Energy -= Time.deltaTime * _energyLossMult;
            if (Energy < 0)
            {
                Unsubscribe();
                _working = false;
                FindNearestWorkstation(EWorkstationType.Rest);
            }
        }
    }

    private void RestLogic()
    {
        if (_resting)
        {
            Energy += Time.deltaTime * (_appreciation / 10) * _energyGainMult;
            if (Energy > 10)
            {
                Unsubscribe();
                _resting = false;
                FindNearestWorkstation(EWorkstationType.Work);
            }
        }
    }

    private void LeaveLogic()
    {
        if (_appreciation <= 0)
        {
            Destroy(gameObject);
        }
    }
    #endregion WorkAndRest
}
