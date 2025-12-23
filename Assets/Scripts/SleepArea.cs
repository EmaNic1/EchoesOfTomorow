using UnityEngine;

public class SleepArea : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Sleep sleep = other.GetComponent<Sleep>();
        if(sleep != null)
        {
            sleep.DoSleep();
        }
    }
}
