using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meeting : MonoBehaviour
{
    [SerializeField] private List<Obj> _meetingPlaces = new List<Obj>();
    [SerializeField] private GameObject _btn;

    private float _timer = 300;
    private bool _meetingActivated = false;

    public void Btn()
    {
        if (ObjManager.Instance.Ais.Count > _meetingPlaces.Count)
        {
            SoundManager.Instance.PlayMeeting();
            for (int i = 0; i < _meetingPlaces.Count; i++)
            {
                _meetingPlaces[i].NbOfSpots = 1;
            }
            _btn.SetActive(false);
            _meetingActivated = true;
            _timer = 300;
            ObjManager.Instance.MeetingMult = 2;
        }
    }

    private void Update()
    {
        if (_meetingActivated && _timer > 0)
        {
            _timer -= Time.deltaTime;
            SetMeetingValue();
        }
        else if (_meetingActivated)
        {
            EndMeeting();
        }
    }

    private void SetMeetingValue()
    {
        float f = 0;
        for (int i = 0; i < _meetingPlaces.Count; i++)
        {
            for (int a = 0; a < _meetingPlaces[i].Subscribed.Count; a++)
            {
                f += 1.2f;
            }
        }
        ObjManager.Instance.MeetingMult = 1 + f;
    }

    private void EndMeeting()
    {
        SoundManager.Instance.PlayMeeting();
        _btn.SetActive(true);
        _meetingActivated = false;
        ObjManager.Instance.MeetingMult = 1;
        for (int i = 0; i < _meetingPlaces.Count; i++)
        {
            _meetingPlaces[i].NbOfSpots = 0;
            List<AISelfMade> list = new List<AISelfMade>();
            for (int a = 0; a < _meetingPlaces[i].Subscribed.Count; a++)
            {
                list.Add(_meetingPlaces[i].Subscribed[a]);
            }
            for (int a = 0; a < list.Count; a++)
            {
                list[a].Unsubscribe();
                list[a].FindNearestWorkstation();
            }
        }
    }
}
