using System;
using System.Collections;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    [SerializeField] DisableControls disableControls;
    [SerializeField] Charater charater;
    [SerializeField] DayTimeController dayTimeController;

    void Awake()
    {
        disableControls = GetComponent<DisableControls>();
        charater = GetComponent<Charater>();
        dayTimeController = GameManager.instance.timeController;
    }

    internal void DoSleep()
    {
        StartCoroutine(SleepRoutine());
        Debug.Log("im sleeping");
    }

    IEnumerator SleepRoutine()
    {
        ScreenTint screenTint = GameManager.instance.screenTint;

        disableControls.DisableControl();

        screenTint.Tint();
        yield return new WaitForSeconds(2f);

        charater.FullHeal();
        charater.Rest(100);

        dayTimeController.SkipToMorning();

        screenTint.UnTint();
        yield return new WaitForSeconds(2f);

        disableControls.EnableControl();

        yield return null;
    }
}
