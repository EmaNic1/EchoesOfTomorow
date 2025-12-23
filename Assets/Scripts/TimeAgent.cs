using System;
using UnityEngine;

public class TimeAgent : MonoBehaviour
{
    public Action onTimeTick;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        GameManager.instance.timeController.Subscribe(this);
    }

    public void Invoke()
    {
        onTimeTick?.Invoke();
    }

    private void OnDestroy()
    {
        if (GameManager.instance != null && GameManager.instance.timeController != null)
        {
            GameManager.instance.timeController.Unsubscribe(this);
        }
    }

}
