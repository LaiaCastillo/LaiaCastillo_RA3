using UnityEngine;

public class Spine : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            var playerObj = Player.instance;
            Player player = playerObj.GetComponent<Player>();
            if ( player != null)
            {
                player.Hurt(1);
            }
            
        }
        
    }
}
