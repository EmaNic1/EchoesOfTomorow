using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ToolHit : MonoBehaviour
{
    public virtual void Hit()
    {

    }

    /// <summary>
    /// ar gali but pataikyta(metodas perrasomas kitose klasese)
    /// </summary>
    /// <param name="canBeHit"></param>
    /// <returns></returns>
    public virtual bool CanBeHit(List<ResourceNodeType> canBeHit)
    {
        return true;
    }
}
