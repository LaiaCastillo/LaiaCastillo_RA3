using System.Collections.Generic;
using UnityEngine;

namespace DialogueSystem
{
    public class SpawnerFlechas : MonoBehaviour
    {
        [SerializeField] private Flecha arrowPrefab;
        [SerializeField] private int poolSize = 20;

        private Queue<Flecha> pool = new Queue<Flecha>();

        void Awake()
        {
            for (int i = 0; i < poolSize; i++)
            {
                Flecha arrow = Instantiate(arrowPrefab, transform);
                arrow.gameObject.SetActive(false);
                pool.Enqueue(arrow);
            }
        }

        public Flecha GetArrow(Vector3 pos, Quaternion rot, float speed, float maxDistance)
        {
            Flecha arrow;

            if (pool.Count > 0)
                arrow = pool.Dequeue();
            else
                arrow = Instantiate(arrowPrefab); // por si te quedas sin flechas

            arrow.transform.position = pos;
            arrow.transform.rotation = rot;
            arrow.gameObject.SetActive(true);
            arrow.Init(speed, maxDistance, this);

            return arrow;
        }

        public void ReturnArrow(Flecha arrow)
        {
            arrow.gameObject.SetActive(false);
            pool.Enqueue(arrow);
        }
    }
}
