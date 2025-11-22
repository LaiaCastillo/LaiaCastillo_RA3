using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float limitRight = 5f;
    [SerializeField] private float limitLeft = -5f;
    [SerializeField] private float stepDown = 1f;
    public EnemySpawner spawner;

    private int direction = 1; // 1 = derecha, -1 = izquierda

    void Update()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
        
        if (transform.position.x >= limitRight && direction > 0)
        {
            direction = -1;
            StepDown();
        }
        else if (transform.position.x <= limitLeft && direction < 0)
        {
            direction = 1;
            StepDown();
        }
    }

    private void StepDown()
    {
        transform.position = new Vector2(transform.position.x, transform.position.y - stepDown);
    }
}


