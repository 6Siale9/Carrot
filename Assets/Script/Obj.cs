using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj : MonoBehaviour
{
    [SerializeField] private int _nbOfSpots = 0;
    [SerializeField] private EWorkstationType _type = EWorkstationType.Work;
    [SerializeField] private float _comfort = 0f;
    private List<AISelfMade> _subscribed = new List<AISelfMade>();

    public int NbOfSpots { get => _nbOfSpots; set => _nbOfSpots = value; }
    public EWorkstationType Type { get => _type; set => _type = value; }
    public List<AISelfMade> Subscribed { get => _subscribed; set => _subscribed = value; }
    public float Comfort { get => _comfort; set => _comfort = value; }
}
