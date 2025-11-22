using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Player: MonoBehaviour, InputSystem_Actions.IPlayerActions, MoveBehaviour
{
    private InputSystem_Actions inputA;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Vector2 _initPos = new Vector2(0, 0);
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _speed = 10f;

    [SerializeField] private Transform weaponPos;
    
    [SerializeField] private AnimationBehaviour _aB;
    [SerializeField] private HealthBehaviour _hB;
    [SerializeField] private AudioSource _aS;
    private Vector2 _currentInput;
    public event Action OpenInventoryEvent = delegate { };
    public event Action PauseGameEvent = delegate { };
    public event Action<Item> ItemCollectedEvent = delegate { };
    public event Action UseItemEvent = delegate { };
    public event Action NewSceneEvent = delegate { };
    private Vector2 checkpointPos;
    public static Player instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        initVars();
        transform.position = _initPos;

        inputA = new InputSystem_Actions();
        inputA.Player.SetCallbacks(this);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0 && instance != null)
        {
            Destroy(instance.gameObject);
            instance = null;
        }
        transform.position = _initPos;
        NewSceneEvent.Invoke();
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void initVars()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rb = GetComponent<Rigidbody2D>();
        _aB = GetComponent<AnimationBehaviour>();
        _hB = GetComponent<HealthBehaviour>();
        _aS = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        inputA.Enable();
    }
    private void OnDisable()
    {
        if (inputA != null)
        {
            inputA.Disable();
        }
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _aB.WalkAnimation(_currentInput);
        _aB.setIdle(_currentInput);
        
        Vector2 velocity = _rb.linearVelocity;
        
        velocity.x = _currentInput.x * _speed;
        _rb.linearVelocity = velocity;

    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        UseItemEvent.Invoke();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        _rb.gravityScale *= -1;
        _aB.ChangeGravity();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _currentInput = context.ReadValue<Vector2>();

    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector3 clickPoint = Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>());
            RaycastHit2D hit2D = Physics2D.Raycast(clickPoint, Vector3.forward, 12f);
            if (hit2D)
            {
                Debug.Log(hit2D.collider.gameObject.name);
                AudioClip clip = null;
                foreach (var kvp in AudioManager.Instance.clipList)
                {
                    if (AudioManager.Instance.clipList.ContainsKey(AudioClips.Yamete))
                    {
                        clip = kvp.Value;
                    }
                }
                AudioSource audioSource = GetComponent<AudioSource>();
                if (clip != null)
                {
                    audioSource.clip = clip;
                }
                audioSource.Play();
                if (_spriteRenderer.color == Color.red)
                {
                    _spriteRenderer.color = Color.black;
                }
                else
                {

                    _spriteRenderer.color = Color.red;
                }
            }
        }
        if (context.performed)
            Debug.Log(Camera.main.ScreenToWorldPoint(context.ReadValue<Vector2>()));
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Enemy2 enemy = collider.GetComponent<Enemy2>();
            if (enemy != null)
            {
                
                _hB.Hurt(enemy.damage);
                
            }
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("items"))
        {
            Item item = collider.GetComponent<Item>();
            if (item != null)
            {
                Destroy(collider.gameObject);
                ItemCollectedEvent.Invoke(collider.GetComponent<Item>());

                Debug.Log("Has conseguido un item");
            }
            else
            {
                Debug.LogWarning("El objeto recogido no tiene un componente Item.");
            }

        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("checkpoint"))
        {
            GameObject checkPoint = collider.gameObject;

            if (checkPoint != null)
            {
                checkpointPos = checkPoint.transform.position;
                _aS.Play();

                SpriteRenderer[] sprites = checkPoint.GetComponentsInChildren<SpriteRenderer>();

                foreach (SpriteRenderer sr in sprites)
                {
                    sr.color = Color.white;
                }
            }
        }
    }

    public void Heal(int amount)
    {
        _hB.Heal(amount);
    }
   
    public void Hurt(int amount)
    {
        Debug.Log(checkpointPos);
        if (checkpointPos != null)
        {
            transform.position = checkpointPos;
        }
        _hB.Hurt(amount);
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OpenInventoryEvent.Invoke();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            
            PauseGameEvent.Invoke();
        }
    }
}
