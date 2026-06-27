using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCursor : MonoBehaviour
{
    [SerializeField] private float _size = 0;
    private bool _uiActive = false;
    [SerializeField] private cursorController _cursorC;

    public bool UiActive { get => _uiActive; set => _uiActive = value; }

    private void Update()
    {
        CheckForInput();
        GetOnCursor();
    }

    private void GetOnCursor()
    {
        gameObject.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
    }

    private void CheckForInput()
    {
        if (!UiActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_cursorC.ClickMode != ModeOfCursor.Default)
                {
                    Click();
                }
                else
                {
                    CheckForObject();
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                UnClick();
            }
        }
    }

    private void CheckForObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D[] raycastHit = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);

        AISelfMade a = FindWorker(raycastHit);

        if (a != null)
        {
            UiElement.Instance.ActivateWorker(a);
            return;
        }

        Obj o = FindWorkstation(raycastHit);

        if (o != null)
        {
            UiElement.Instance.ActivateWorkstation(o);
        }
    }

    private AISelfMade FindWorker(RaycastHit2D[] raycastHit)
    {
        for (int i = 0; i < raycastHit.Length; i++)
        {
            if (raycastHit[i].collider != null)
            {
                if (raycastHit[i].collider.CompareTag("LittleGuys"))
                {
                    return raycastHit[i].collider.GetComponent<AISelfMade>();
                }
            }
        }
        return null;
    }

    private Obj FindWorkstation(RaycastHit2D[] raycastHit)
    {
        for (int i = 0; i < raycastHit.Length; i++)
        {
            if (raycastHit[i].collider != null)
            {
                if (raycastHit[i].collider.CompareTag("Workstation"))
                {
                    return raycastHit[i].collider.GetComponent<Obj>();
                }
            }
        }
        return null;
    }

    private void Click()
    {
        for (int i = 0; i < ObjManager.Instance.Ais.Count; i++)
        {
            if (Vector3.Distance(transform.position, ObjManager.Instance.Ais[i].transform.position) < _size)
            {
                ObjManager.Instance.Ais[i].StartFollowing(transform);
                if (_cursorC.ClickMode == ModeOfCursor.Carrot)
                {
                    ObjManager.Instance.Score -= 2.5f;
                }
                else if (_cursorC.ClickMode == ModeOfCursor.Stick)
                {
                    ObjManager.Instance.Ais[i].Appreciation -= 3;
                }
            }
        }
    }

    private void UnClick()
    {
        for (int i = 0; i < ObjManager.Instance.Ais.Count; i++)
        {
            ObjManager.Instance.Ais[i].ReturnToBasic();
        }
    }
}
