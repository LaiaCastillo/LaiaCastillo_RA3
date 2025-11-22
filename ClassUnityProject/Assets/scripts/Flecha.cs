using DialogueSystem;
using UnityEngine;

public class Flecha : MonoBehaviour
{
    private float speed;
    private float maxDistance;
    private Vector3 startPos;
    private SpawnerFlechas pool;

    public void Init(float speed, float maxDistance, SpawnerFlechas pool)
    {
        this.speed = speed;
        this.maxDistance = maxDistance;
        this.pool = pool;
        startPos = transform.position;

        // NO cambiamos escala ni rotación
        // Transform se mantiene tal cual para que el sprite no se gire
    }

    void Update()
    {
        // Movimiento hacia la izquierda en espacio mundial, sin afectar el sprite
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Devolver al pool si supera distancia o sale de pantalla
        if (Vector3.Distance(startPos, transform.position) >= maxDistance || !IsOnScreen())
            pool.ReturnArrow(this);
    }

    private bool IsOnScreen()
    {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return screenPoint.x >= 0 && screenPoint.x <= 1 && screenPoint.y >= 0 && screenPoint.y <= 1;
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
