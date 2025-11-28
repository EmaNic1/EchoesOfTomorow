using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class DayTimeController : MonoBehaviour
{
    const float secondInDay = 86400f;
    const float phaseLenght = 900f; // 15 min sekundemis

    [SerializeField] Color nightColor;
    [SerializeField] AnimationCurve nightTimeCurve;
    [SerializeField] Color dayColor = Color.white;
    float time;
    [SerializeField] float timeScale = 60f;//
    [SerializeField] float startAtTime = 28800f; // ryte sekundem
    [SerializeField] Light2D globalLight;

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI day;
    private int days = 1;

    List<TimeAgent> agents;

    private void Awake()
    {
        agents = new List<TimeAgent>();
    }

    private void Start()
    {
        time = startAtTime;
        UpdateDayText(); // iš karto atnaujina
    }

    public void Subscribe(TimeAgent agent)
    {
        agents.Add(agent);
    }

    public void Unsubscribe(TimeAgent agent)
    {
        agents.Remove(agent);
    }

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
        TimeValueCalculation();
        DayLight();

        if (time > secondInDay)
        {
            NextDay();
        }

        TimeAgents();
    }

    private void TimeValueCalculation()
    {
        int hh = (int)hours;
        int mm = (int)minutes;
        text.text = hh.ToString("00") + ":" + mm.ToString("00");
        UpdateDayText();
    }

    private void UpdateDayText()
    {
        day.text = days.ToString();
    }

    private void DayLight()
    {
        float v = nightTimeCurve.Evaluate(hours);
        Color c = Color.Lerp(dayColor, nightColor, v);
        globalLight.color = c;
    }

    int oldPhase = 0;

    private void TimeAgents()
    {
        int currentPhase = (int)(time / phaseLenght);
        if(oldPhase != currentPhase)
        {
            oldPhase = currentPhase;
            for (int i = 0; i < agents.Count; i++)
            {
                agents[i].Invoke();
            }
        }
    }

    private void NextDay()
    {
        time = 0;
        days += 1;
        UpdateDayText(); // atnaujina tekstą iš karto
    }

}
