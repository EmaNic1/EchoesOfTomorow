using UnityEngine;
using UnityEngine.Rendering;

public class MarkController : MonoBehaviour
{
    [SerializeField] GameObject mark;

    GameObject currentTraget;


    public void Mark(GameObject target)
    {
        if(currentTraget == target)
        {
            return;
        }
        currentTraget = target;

        Vector3 position = target.transform.position;
        Mark(position);
    }

    public void Mark(Vector3 position)
    {
        mark.SetActive(true);
        mark.transform.position = position + new Vector3(0, 0.4f, 0.02f);
    }

    public void Hide()
    {
        currentTraget = null;
        mark.SetActive(false);
    }
}
