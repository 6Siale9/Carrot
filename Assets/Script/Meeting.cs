using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meeting : MonoBehaviour
{
    [SerializeField] private List<Obj> _meetingPlaces = new List<Obj>();
    public void Btn()
    {
        if (ObjManager.Instance.Ais.Count > _meetingPlaces.Count)
        {
            List<AISelfMade> ais = ObjManager.Instance.Ais;
            List<AISelfMade> aisReady = new List<AISelfMade>();
            for (int i = 0; i < ais.Count; i++)
            {
                if (ais[i].Working)
                {
                    aisReady.Add(ais[i]);
                }
            }
            for (int i = 0; i < aisReady.Count; i++)
            {

            }
        }
    }
}
