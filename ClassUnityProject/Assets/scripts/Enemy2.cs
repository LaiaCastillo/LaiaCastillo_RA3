using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float limitRight = 5f;
    [SerializeField] private float limitLeft = -5f;
    [SerializeField] private float health = 5;
    [SerializeField] public int damage = 1;
    public EnemySpawner spawner;

    private int direction = 1; // 1 = derecha, -1 = izquierda
    [SerializeField] private float damageCooldown = 1f; // tiempo entre ataques
    private float lastDamageTime = -Mathf.Infinity;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);

        if (transform.position.x >= limitRight && direction > 0)
        {
            direction = -1;
        }
        else if (transform.position.x <= limitLeft && direction < 0)
        {
            direction = 1;
        }
        if (this.health <= 0)
        {
            Die();
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (Time.time - lastDamageTime >= damageCooldown)
            {
                Debug.Log("Has atacado a un enemigo");
                this.health--;
                lastDamageTime = Time.time;
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
