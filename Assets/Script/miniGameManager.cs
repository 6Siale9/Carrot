using System.Security;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class miniGameManager : MonoBehaviour
{
    [SerializeField] private float clockInterval = 3f;
    private float clockTime = 0f;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI betText;
    public TextMeshProUGUI Pourcentage;


    public GameObject miniGameContainer;
    [SerializeField] public string inputText;
    [SerializeField] private TMP_Text reactionTextBox;
    [SerializeField] private GameObject reactionGroup;
    [SerializeField] private float inputParsedAsFloat;

    [SerializeField] private float maxTimeSpent = 30f;
    [SerializeField] private int minOdds = -5;
    [SerializeField] private int maxOdds = 15;
    private float currentTimeSpent;

    [SerializeField] private ObjManager score;   
    private bool betIsActive = false;
    private float startingBetValue = 0;
    private float betValue = 0;
    private float randomPourcentage = 0;
    
 

    public void appear()
    {
        miniGameContainer.SetActive(true);
    }

    public void disappear()
    {
        miniGameContainer.SetActive(false);
    }
    public void grabFromInputField (string input)
    {
        inputText = input;
        inputParsedAsFloat = float.Parse(input);
        startingBetValue = inputParsedAsFloat;
    }
    public void clock()
    {
        if (betIsActive == true)
        {
            clockTime += Time.deltaTime;
            timerText.text = clockTime.ToString();
            if(clockTime >= clockInterval)
            {
                randomPourcentageGenerator();
                clockTime = 0f;
            }
            currentTimeSpent += Time.deltaTime;
            if (currentTimeSpent >= maxTimeSpent)
            {
                stopBet();
                currentTimeSpent = 0;
            }
        }

    }
    public void randomPourcentageGenerator()
    {
        randomPourcentage = Random.Range(minOdds, maxOdds);
        if (randomPourcentage > 0)
        {
        Pourcentage.color = Color.green;
        Pourcentage.text = "+" + randomPourcentage.ToString() + "%";
        }

        if (randomPourcentage == 0)
        {
        Pourcentage.color = Color.gray;
        Pourcentage.text = randomPourcentage.ToString() + "%";
        }

        if (randomPourcentage < 0)
        {
        Pourcentage.color = Color.red;
        Pourcentage.text = randomPourcentage.ToString() + "%";
        }

        randomPourcentage = randomPourcentage / 100;

        //betValue = betValue + (startingBetValue * randomPourcentage);
        betValue = betValue * (1 + randomPourcentage);
        betText.text = betValue.ToString("F0");
        

    }
    public void stopBet()
    {
        Pourcentage.text = "0";
        betIsActive = false;
        score._score += betValue;
        startingBetValue = 0;
        disappear();
    }

    public void startBet()
    {
        betValue = startingBetValue;
        score._score -= betValue;

        betIsActive = true;
        Pourcentage.text = "0";
        betText.text = betValue.ToString();
        clockTime = 3f;
    }

    void Start()
    {
        clockTime = 0f;
        Pourcentage.text = "0";
        betText.text = "0";
    }

    public void setBetValue()
    {
        if (betIsActive == false)
        {
            startingBetValue += 250;
            betText.text = startingBetValue.ToString();
        }

        else
        {
            return;
        }
 
    }
    void Update()
    {
        clock();
    }
}
