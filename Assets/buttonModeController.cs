using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class buttonModeController : MonoBehaviour
{
    [SerializeField] private ModeOfCursor modeOfCursor;


    public void carrotButton()
    {
        cursorController.Instance.setToMode(ModeOfCursor.Carrot);
    }

    public void defaultButton()
    {
        cursorController.Instance.setToMode(ModeOfCursor.Default);
    }

    public void stickButton()
    {
        cursorController.Instance.setToMode(ModeOfCursor.Stick);
    }
}
