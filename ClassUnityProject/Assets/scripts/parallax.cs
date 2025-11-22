using UnityEngine;

public class parallax : MonoBehaviour { 
    [SerializeField] private float parallaxMuiltiplier = 0.02f;

    [SerializeField] Material parallaxMat;
    [SerializeField] private Transform playerPos;
    [SerializeField] private float lastPlayerX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    
    void Start()
    {
        parallaxMat = GetComponent<Renderer>().material;
        playerPos = Player.instance.transform;
        lastPlayerX = playerPos.position.x;
    }

    void Update()
    {
        float deltaX = playerPos.position.x - (lastPlayerX != 0 ? lastPlayerX : 0);
        parallaxMat.mainTextureOffset += new Vector2(deltaX * parallaxMuiltiplier, 0);
        lastPlayerX = playerPos.position.x;
    }
}
