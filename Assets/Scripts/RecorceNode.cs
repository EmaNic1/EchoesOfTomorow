using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// mazgas
/// </summary>

[RequireComponent(typeof(BoxCollider2D))]//automatiskai pridedamas box collider
public class RecorceNode : ToolHit
{
    [SerializeField] GameObject pickUpDrop;//prefab nukrenta ant zemes
    [SerializeField] int dropCount = 5;
    [SerializeField] float spread = 0.7f;
    [SerializeField] ResourceNodeType nodeType;//koks resurso tipas


    public override void Hit()
    {
        // Ciklas tęsiasi tol, kol yra „dropCount“.
        // dropCount-- – sumažina „dropCount“ skaičių.
        while (dropCount > 0)
        {
            dropCount--;

            // pradine padetis yra ta kurioje stovi naikinamas objektas
            Vector3 position = transform.position;

            // position.x ir position.y keičiami atsitiktinai, kad elementai nebūtų vienoje vietoje, o būtų išmėtyti po objektą.
            position.x += spread * UnityEngine.Random.value - spread / 2;
            position.y += spread * UnityEngine.Random.value - spread / 2;

            // pozicija nustatoma pagal apskaičiuotą poziciją
            GameObject go = Instantiate(pickUpDrop);
            go.transform.position = position;
        }

        // Destroy the object
        Destroy(gameObject);
    }

    /// <summary>
    /// tikrina ar su irankiu gali hitinti objekta
    /// </summary>
    /// <param name="canBeHit"></param>
    /// <returns></returns>
    public override bool CanBeHit(List<ResourceNodeType> canBeHit)
    {
        return canBeHit.Contains(nodeType);
    }
}