using UnityEngine;

public abstract class InteractableMain : MonoBehaviour
{
    //Text, který se ukáže hráčii když interaguje s objektem
    public string promptText;

    public void BaseInteract()
    {
        Interact();
    }
    //protected znamená viditelná jenom v této třídě a viditelná jenom podtřídami, které vychází z této třídy
    protected virtual void Interact()
    {
        //template
    }
}
