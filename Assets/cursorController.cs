using UnityEngine;

public class cursorController : MonoBehaviour
{
    public static cursorController Instance { get; private set; }

    [SerializeField] private Texture2D cursorTextureDefault;
    [SerializeField] private Texture2D cursorTextureCarrot;
    [SerializeField] private Texture2D cursorTextureStick;

    [SerializeField] private Vector2 clickPosition = Vector2.zero;

    private ModeOfCursor clickMode = ModeOfCursor.Carrot;


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
                clickMode = ModeOfCursor.Default;
                break;
            case ModeOfCursor.Carrot:
                Cursor.SetCursor(cursorTextureCarrot, clickPosition, CursorMode.Auto);
                clickMode = ModeOfCursor.Carrot;
                break;
            case ModeOfCursor.Stick:
                Cursor.SetCursor(cursorTextureStick, clickPosition, CursorMode.Auto);
                clickMode = ModeOfCursor.Stick;
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