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
    private float _archiveBonus = 1;
    [SerializeField] private List<GameObject> _toInstantiate = new List<GameObject>();

    public float Appreciation { get => _appreciation; set => _appreciation = value; }
    public float Energy { get => _energy; set => _energy = value; }
    public string Name { get => _name; set => _name = value; }
    public float EnergyLossMult { get => _energyLossMult; set => _energyLossMult = value; }
    public float EnergyGainMult { get => _energyGainMult; set => _energyGainMult = value; }
    public bool Working { get => _working; set => _working = value; }
    public Obj WorkStation { get => _workStation; set => _workStation = value; }


    #region Base
    private void Start()
    {
        ObjManager.Instance.Ais.Add(this);
        Energy = Random.Range(0f, 10f);
        EnergyGainMult = Random.Range(0.1f, 2f);
        EnergyLossMult = Random.Range(0.1f, 2f);
        _pauseClope = ObjManager.Instance.PauseClope;
        FindNearestWorkstation();
        _name = ObjManager.Instance.Names[Random.Range(0, ObjManager.Instance.Names.Count)] + " " + ObjManager.Instance.Names[Random.Range(0, ObjManager.Instance.Names.Count)];
        GameObject g = _toInstantiate[Random.Range(0, _toInstantiate.Count)];
        Instantiate(g, transform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Obj>() != null)
        {
            Obj obj = collision.GetComponent<Obj>();
            if (obj == WorkStation)
            {
                switch (obj.Type)
                {
                    case EWorkstationType.Work:
                        StartWorking();
                        break;
                    case EWorkstationType.Rest:
                        StartResting();
                        _archiveBonus = 1;
                        break;
                    case EWorkstationType.Meeting:
                        break;
                    case EWorkstationType.Archive:
                        if (!_working)
                        {
                            _appreciation -= 6;
                        }
                        StartWorking();
                        RoundAndRound(WorkStation);
                        _archiveBonus += 1;
                        break;
                    case EWorkstationType.Casino:
                        break;
                    case EWorkstationType.PauseClope:
                        break;
                }
            }
            else if (_workStation != null)
            {
                if (WorkStation.Type == EWorkstationType.Archive && obj.Type == EWorkstationType.Archive && _working)
                {
                    RoundAndRound(collision.GetComponent<Obj>());
                    _archiveBonus += 1;
                }
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
    private void RoundAndRound(Obj ooo)
    {
        List<Obj> list = new List<Obj>();
        for (int i = 0; i < ObjManager.Instance.Objs.Count; i++)
        {
            if (ObjManager.Instance.Objs[i].Type == EWorkstationType.Archive)
            {
                list.Add(ObjManager.Instance.Objs[i]);
            }
        }
        list.Remove(ooo);
        _aiDestSet.target = list[Random.Range(0, list.Count)].transform;
    }

    public void StartFollowing(Transform cursor)
    {
        if (WorkStation != null)
        {
            Unsubscribe();
        }
        _following = true;
        Working = false;
        _resting = false;
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

    public void Subscribe(Obj obj)
    {
        WorkStation = obj;
        WorkStation.Subscribed.Add(this);
        _aiDestSet.target = obj.transform;
    }

    public void Unsubscribe()
    {
        WorkStation.Subscribed.Remove(this);
        WorkStation = null;
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
        Working = true;
    }

    private void StartResting()
    {
        _resting = true;
        _appreciation = Mathf.Clamp(_appreciation + 1, 0, 10);
    }

    private void WorkLogic()
    {
        if (Working)
        {
            float bonus = WorkStation.Comfort / 10;
            ObjManager.Instance.Score += (_appreciation / 10) * Time.deltaTime * ObjManager.Instance.MeetingMult * _archiveBonus * (1 + bonus);
            bonus = 1 - bonus;
            bonus -= (_archiveBonus / 10);
            bonus = Mathf.Clamp(bonus, 0.1f, 1);
            Energy -= Time.deltaTime * bonus;
            if (Energy < 0)
            {
                if (WorkStation != null)
                {
                    Unsubscribe();
                }
                Working = false;
                FindNearestWorkstation();
            }
        }
    }

    private void RestLogic()
    {
        if (_resting)
        {
            Energy += Time.deltaTime * (1 + (3 * (WorkStation.Comfort / 10)));
            if (Energy > 10)
            {
                if (WorkStation != null)
                {
                    Unsubscribe();
                }
                _resting = false;
                FindNearestWorkstation();
            }
        }
    }

    private void LeaveLogic()
    {
        if (_appreciation <= 0)
        {
            ObjManager.Instance.Ais.Remove(this);
            if (WorkStation != null)
            {
                Unsubscribe();
            }
            Destroy(gameObject);
        }
    }
    #endregion WorkAndRest
}
