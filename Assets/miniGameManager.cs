using TMPro;
using UnityEngine;

public class miniGameManager : MonoBehaviour
{
    [SerializeField] private float clockInterval = 3f;
    private float clockTime = 0f;
    public TextMeshProUGUI timerText;


    private int betValue = 1000;
    private int randomPourcentage = 0;
    public void randomPourcentageGenerator()
    {
        randomPourcentage = Random.Range(-20, 20);
        randomPourcentage = randomPourcentage / 100;
        randomPourcentage = randomPourcentage + 1;

        betValue = betValue * randomPourcentage;
        Debug.Log(betValue);
    }

    public void clock()
    {
        clockTime += Time.deltaTime;
        timerText.text = clockTime.ToString();
        if(clockTime >= clockInterval)
        {
            randomPourcentageGenerator();
            clockTime = 0f;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        clockTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        clock();
    }
}
