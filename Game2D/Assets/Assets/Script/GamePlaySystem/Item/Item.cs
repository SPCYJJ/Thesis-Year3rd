using UnityEngine;

public abstract class Item : MonoBehaviour
{
    public string itemID;
    public string itemName;

    public abstract void Use(Player player);
}
