using UnityEngine;

public class cursorController : MonoBehaviour
{
    public static cursorController Instance { get; private set; }

    [SerializeField] private Texture2D cursorTextureDefault;
    [SerializeField] private Texture2D cursorTextureCarrot;
    [SerializeField] private Texture2D cursorTextureStick;

    [SerializeField] private Vector2 clickPosition = Vector2.zero;

    [SerializeField] private ModeOfCursor clickMode = ModeOfCursor.Default;

    public ModeOfCursor ClickMode { get => clickMode; set => clickMode = value; }

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
        setToMode(ModeOfCursor.Carrot);
    }





    public void setToMode(ModeOfCursor modeOfCursor)
    {
        switch (modeOfCursor)
        {
            case ModeOfCursor.Default:
                Cursor.SetCursor(cursorTextureDefault, clickPosition, CursorMode.Auto);
                ClickMode = ModeOfCursor.Default;
                break;
            case ModeOfCursor.Carrot:
                Cursor.SetCursor(cursorTextureCarrot, clickPosition, CursorMode.Auto);
                ClickMode = ModeOfCursor.Carrot;
                break;
            case ModeOfCursor.Stick:
                Cursor.SetCursor(cursorTextureStick, clickPosition, CursorMode.Auto);
                ClickMode = ModeOfCursor.Stick;
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