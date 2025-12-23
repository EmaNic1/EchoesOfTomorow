using UnityEngine;

public class Store : Interactable
{
    public ItemContainer storeContent;

    public float buyFromPlayer = 0.5f;
    public float sellToPlayerMultip = 1.5f;

    public override void Interact(Charater charater)
    {
        Traiding traiding = charater.GetComponent<Traiding>();

        if(traiding == null) { return; }

        traiding.BeginTraiding(this);
    }
}
