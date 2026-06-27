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
        /*
        Vector3 end = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        Vector3 start = Camera.main.transform.position;
        Ray ray = new Ray(start, start -end );
        RaycastHit hit = new RaycastHit();
        Physics.Raycast(ray, out hit);
        AISelfMade a = hit.rigidbody.gameObject.GetComponent<AISelfMade>();
        Debug.DrawRay(start, start - end, Color.red, 2);
        */

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D raycastHit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10)), Vector2.zero);
        
        


        if (raycastHit.collider.CompareTag("LittleGuys"))
        {
            AISelfMade a = raycastHit.collider.GetComponent<AISelfMade>();
            UiElement.Instance.ActivateWorker(a);
        }
        if (raycastHit.collider.CompareTag("Workstation"))
        {
            Obj b = raycastHit.collider.GetComponent<Obj>();
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
