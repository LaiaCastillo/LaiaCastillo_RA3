using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] public int amount;
    [SerializeField] public string nameItem;
    [SerializeField] public string description;
    [SerializeField] public Sprite icon;

   
    public abstract void Use(Player player);
    
}
