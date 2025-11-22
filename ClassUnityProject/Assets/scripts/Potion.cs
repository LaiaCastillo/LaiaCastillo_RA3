using UnityEngine;

public class Potion : Item
{
    [SerializeField] private int healthAmount = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        name = "Potion";
        description = "A strange liquíd that seems to heal you some health";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Use(Player player)
    {
        Debug.Log("You used a Potion");
        player.Heal(healthAmount);
    }
}
