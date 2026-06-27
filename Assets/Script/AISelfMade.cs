using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
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
    private string _name = "";

    public float Appreciation { get => _appreciation; set => _appreciation = value; }
    public float Energy { get => _energy; set => _energy = value; }
    public string Name { get => _name; set => _name = value; }

    #region Base
    private void Start()
    {
        ObjManager.Instance.Ais.Add(this);
        Energy = Random.Range(0f, 10f);
        _energyGainMult = Random.Range(0.1f, 2f);
        _energyLossMult = Random.Range(0.1f, 2f);
        _pauseClope = ObjManager.Instance.PauseClope;
        FindNearestWorkstation();
        _name = ObjManager.Instance.Names[Random.Range(0, ObjManager.Instance.Names.Count)] + " " + ObjManager.Instance.Names[Random.Range(0, ObjManager.Instance.Names.Count)];

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
            FindNearestWorkstation();
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

    public void FindNearestWorkstation()
    {
        List<Obj> list = new List<Obj>();
        if (_energy > 0)
        {
            for (int i = 0; i < ObjManager.Instance.Objs.Count; i++)
            {
                if (ObjManager.Instance.Objs[i].Type != EWorkstationType.Rest && ObjManager.Instance.Objs[i].Subscribed.Count < ObjManager.Instance.Objs[i].NbOfSpots)
                {
                    list.Add(ObjManager.Instance.Objs[i]);
                }
            }
        }
        else
        {
            for (int i = 0; i < ObjManager.Instance.Objs.Count; i++)
            {
                if (ObjManager.Instance.Objs[i].Type == EWorkstationType.Rest && ObjManager.Instance.Objs[i].Subscribed.Count < ObjManager.Instance.Objs[i].NbOfSpots)
                {
                    list.Add(ObjManager.Instance.Objs[i]);
                }
            }
        }

        Obj o = _pauseClope;
        float d = 0;
        if (list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (Vector3.Distance(transform.position, list[i].transform.position) < d || d == 0)
                {
                    d = Vector3.Distance(transform.position, list[i].transform.position);
                    o = list[i];
                }
            }
            Subscribe(o);
        }
        else
        {
            _aiDestSet.target = _pauseClope.transform;
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
            ObjManager.Instance.Score += (_appreciation / 10) * Time.deltaTime;
            Energy -= Time.deltaTime * (1 + (1 * (_workStation.Comfort / 10)));
            if (Energy < 0)
            {
                Unsubscribe();
                _working = false;
                FindNearestWorkstation();
            }
        }
    }

    private void RestLogic()
    {
        if (_resting)
        {
            Energy += Time.deltaTime * (1 + (1 * (_workStation.Comfort / 10)));
            if (Energy > 10)
            {
                Unsubscribe();
                _resting = false;
                FindNearestWorkstation();
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
