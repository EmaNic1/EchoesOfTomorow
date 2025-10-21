using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DayTimeController : MonoBehaviour
{
    const float secondInDay = 86400f;

    [SerializeField] Color nightColor;
    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Color dayColor = Color.white;
    float time;
    [SerializeField] float timeScale = 60f;
    [SerializeField] Light2D globalLight;

    [SerializeField] TextMeshProUGUI text;
    private int days;

    float hours
    {
        get { return time / 3600f; }
    }
    float minutes
    {
        get { return time % 3600f / 60f; }
    }

    private void Update()
    {
        time += Time.deltaTime * timeScale;
        int hh = (int)hours;
        int mm = (int)minutes;
        text.text = hh.ToString("00") + ":" + mm.ToString("00");
        float v = nightTimeCurve.Evaluate(hours);
        Color c = Color.Lerp(dayColor, nightColor, v);
        globalLight.color = c;
        if(time > secondInDay)
        {
            NextDay();
        }
    }

    private void NextDay()
    {
        time = 0;
        days += 1;

    }

}
