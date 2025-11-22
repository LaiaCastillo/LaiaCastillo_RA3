using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _gameObjecjtEnemy;
    public Stack<GameObject> EnemiesStack = new Stack<GameObject>();
    private float timeSpawn = 5f;
    private float _nextSpawnTime = 0f;
    
    void Update()
    {
        if (Time.time >= _nextSpawnTime)
        {
            if (EnemiesStack.Count == 0)
            {
                InstantiateEnemys();
            }
            else
            {
                Pop();
                Debug.Log("Nos gusta reciclar");
            }
            _nextSpawnTime = Time.time + timeSpawn;
        }
    }
    public void Push(GameObject enemy)
    {
        EnemiesStack.Push(enemy);
        enemy.SetActive(false);
    }
    public GameObject Pop()
    {
        GameObject go = EnemiesStack.Pop();
        go.SetActive(true);
        go.transform.position = transform.position;
        go.GetComponent<Rigidbody2D>().linearVelocityX = 5f;
        return go;
    }

    public void InstantiateEnemys()
    {
        GameObject enem = Instantiate(_gameObjecjtEnemy, transform.position, Quaternion.identity);
        enem.GetComponent<Enemy>().spawner = this;
    }
}
