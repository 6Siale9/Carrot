using System.Globalization;
using TMPro;
using UnityEngine;

public class timeScript : MonoBehaviour
{
    public string myFormat;
    public TextMeshProUGUI mainClock;

    public System.TimeSpan timeSpan = new System.TimeSpan(0,0, 0,0,0);

    public System.DateTime date = new System.DateTime(1990, 04, 20);
    public float timeRate = 36;

    private void Update()
    {
        float minutes = Time.deltaTime * 1000 * timeRate;

        timeSpan += new System.TimeSpan(0, 0, 0, 0, (int)minutes);
        System.DateTime dateTime = date.Add(timeSpan);

        mainClock.text = dateTime.ToString(@myFormat, new CultureInfo("en-US"));
    }


    public void AddTime(int value)
    {
        timeSpan += new System.TimeSpan(0, 30,0);
    }
}
