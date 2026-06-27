using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjManager : MonoBehaviour
{
    [SerializeField] private List<Obj> _objs = new List<Obj>();
    private List<AISelfMade> _ais = new List<AISelfMade>();
    [SerializeField] private Obj _pauseClope;
    private float _score = 50f;

    private static ObjManager instance;

    public static ObjManager Instance { get => instance; set => instance = value; }
    public List<Obj> Objs { get => _objs; set => _objs = value; }
    public List<AISelfMade> Ais { get => _ais; set => _ais = value; }
    public Obj PauseClope { get => _pauseClope; set => _pauseClope = value; }
    public float Score { get => _score; set => _score = value; }

    private void Awake()
    {
        if (ObjManager.Instance == null)
        {
            ObjManager.Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
