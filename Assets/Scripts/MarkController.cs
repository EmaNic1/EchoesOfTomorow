using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// vizualiai rodo zymekli
/// </summary>

public class MarkController : MonoBehaviour
{
    [SerializeField] GameObject mark;

    GameObject currentTraget;

    /// <summary>
    /// pazymeti nauja objekta
    /// </summary>
    /// <param name="target"></param>
    public void Mark(GameObject target)
    {
        //jeigu zymeklis jau vietoje
        if(currentTraget == target)
        {
            return;//nieko nedaro
        }
        //priskiria nauja target
        currentTraget = target;
        //paima jo pzicija
        Vector3 position = target.transform.position;
        Mark(position);//pazymi nauja pozicija
    }

    public void Mark(Vector3 position)
    {
        //ijungiamas ir perkeliamas zymeklis
        mark.SetActive(true);
        mark.transform.position = position + new Vector3(0, 0.4f, 0.02f);
    }

    public void Hide()
    {
        //isjungiamas zymeklis
        currentTraget = null;
        mark.SetActive(false);
    }
}
