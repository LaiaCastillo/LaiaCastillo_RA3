using UnityEngine;



public class Enemy1 : MonoBehaviour
{
    [Header("Puntos de patrulla")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    [Header("Velocidad de movimiento")]
    [SerializeField] private float speed = 2f;

    private Transform currentTarget;

    void Start()
    {
        if (pointA == null || pointB == null)
        {
            Debug.LogError("Enemy1: Asigna los puntos A y B");
            enabled = false;
            return;
        }

        // Empezamos moviéndonos hacia B
        currentTarget = pointB;
        LookAtTarget();
    }

    void Update()
    {
        // Solo mover en X
        float newX = Mathf.MoveTowards(transform.position.x, currentTarget.position.x, speed * Time.deltaTime);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        // Si llegamos al target, cambiamos al otro
        if (Mathf.Abs(transform.position.x - currentTarget.position.x) < 0.01f)
        {
            currentTarget = currentTarget == pointA ? pointB : pointA;
            LookAtTarget();
        }
    }

    private void LookAtTarget()
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (currentTarget.position.x > transform.position.x ? 1 : -1);
        transform.localScale = scale;
    }

private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            var playerObj = Player.instance;
            Player player = playerObj.GetComponent<Player>();
            if (player != null)
            {
                player.Hurt(1);
            }

        }

    }
}
