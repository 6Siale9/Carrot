using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCursor : MonoBehaviour
{
    [SerializeField] private float _size = 0;
    private bool _uiActive = false;

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
                //Click();
                CheckForObject();
            }
            if (Input.GetMouseButtonUp(0))
            {
                //UnClick();
            }
        }
    }

    private void CheckForObject()
    {
        Vector3 end = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        Vector3 start = Camera.main.transform.position;
        Ray ray = new Ray(start, end - start);
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(ray, out hit);
        AISelfMade a = hit.rigidbody.gameObject.GetComponent<AISelfMade>();
        if (a != null)
        {
            UiElement.Instance.ActivateWorker(a);
        }
        Obj b = hit.collider.GetComponent<Obj>();
        if (b != null)
        {
            UiElement.Instance.ActivateWorkstation(b);
        }
    }

    private void Click()
    {
        for (int i = 0; i < ObjManager.Instance.Ais.Count; i++)
        {
            if (Vector3.Distance(transform.position, ObjManager.Instance.Ais[i].transform.position) < _size)
            {
                ObjManager.Instance.Ais[i].StartFollowing(transform);
                ObjManager.Instance.Ais[i].Appreciation -= 2;
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
