using System.IO;
using UnityEngine;

public class AnimationBehaviour : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sr;
    private float idleTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void WalkAnimation(Vector2 direction)
    {
        _sr.flipX = direction.x < 0;

        _animator.SetFloat("horizontal", Mathf.Abs(direction.x));
        
    }
    public void ChangeGravity()
    {
        _sr.flipY = _sr.flipY ? false : true;
    }
    public void setIdle(Vector2 vector) 
    {
        if (Mathf.Abs(vector.x) < 0.01f)
        {
            idleTime  +=Time.deltaTime; // sigue sumando mientras no hay movimiento
        }
        else
        {
            idleTime = 0f; // si se mueve, reiniciamos
        }
        _animator.SetFloat("seconds", idleTime);

    }
}
