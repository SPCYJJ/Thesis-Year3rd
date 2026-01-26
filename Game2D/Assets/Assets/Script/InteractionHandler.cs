using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    public void Interact(GameObject target)
    {
        target.SendMessage("OnInteract", SendMessageOptions.DontRequireReceiver);
    }
}
