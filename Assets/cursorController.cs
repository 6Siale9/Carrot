using UnityEngine;

public class cursorController : MonoBehaviour
{
    public static cursorController Instance { get; private set; }

    [SerializeField] private Texture2D cursorTextureDefault;
    [SerializeField] private Texture2D cursorTextureCarrot;
    [SerializeField] private Texture2D cursorTextureStick;

    [SerializeField] private Vector2 clickPosition = Vector2.zero;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }





    void Start()
    {
        Cursor.SetCursor(cursorTextureDefault, clickPosition, CursorMode.Auto);
    }





    public void setToMode(ModeOfCursor modeOfCursor)
    {
        switch (modeOfCursor)
        {
            case ModeOfCursor.Default:
                Cursor.SetCursor(cursorTextureDefault, clickPosition, CursorMode.Auto);
                break;
            case ModeOfCursor.Carrot:
                Cursor.SetCursor(cursorTextureCarrot, clickPosition, CursorMode.Auto);
                break;
            case ModeOfCursor.Stick:
                Cursor.SetCursor(cursorTextureStick, clickPosition, CursorMode.Auto);
                break;
            default:
                Cursor.SetCursor(cursorTextureDefault, clickPosition, CursorMode.Auto);
                break;
        }
    }
}



 public enum ModeOfCursor
{
     Default,
    Carrot,
    Stick
}