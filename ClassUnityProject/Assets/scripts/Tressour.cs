using System;
using UnityEngine;

public class Tressour : MonoBehaviour
{
    [SerializeField] private new string[] items;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    { }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player")) { 
            var r = UnityEngine.Random.Range(0, items.Length);
            string itemDropped = items[r];
            Debug.Log("Has abierto un Tesoro con: " + itemDropped);

        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
