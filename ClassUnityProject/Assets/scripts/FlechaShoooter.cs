using DialogueSystem;
using UnityEngine;

public class FlechaShooter : MonoBehaviour
{
    [SerializeField] private SpawnerFlechas spawner;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float arrowSpeed = -10f;
    [SerializeField] private float maxDistance = 20f;

    [SerializeField] private float fireRate = 1f; // disparar cada segundo
    private float nextFireTime;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            SpawnArrow();
        }
    }

    void SpawnArrow()
    {
        if (spawner != null)
        {
            Vector3 dir = transform.right; 
            Quaternion rot = Quaternion.LookRotation(Vector3.forward, dir); // 2D
            spawner.GetArrow(shootPoint.position, rot, arrowSpeed, maxDistance);
        }
    }
}
