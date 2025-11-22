using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraView : MonoBehaviour
{
    [SerializeField] private float fixedWidth = 20f;
    private GameObject player;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private float smoothTime = 0.2f;
    private Camera cam;

    void Awake()
    {
        Debug.Log("Awake");
        cam = GetComponent<Camera>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        AssignPlayer();
        if (player == null)
        {
            Debug.LogError("CameraView no pudo encontrar al Player en Start");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AssignPlayer();
        if (player == null)
        {
            Debug.LogError("CameraView no pudo encontrar al Player en SceneLoaded");
        }
    }

    private void AssignPlayer()
    {
        if (Player.instance != null)
        {
            player = Player.instance.gameObject;
            Debug.Log("CameraView asignó el Player correctamente");
        }
    }

    void LateUpdate()
    {
        if (player == null) return;

        float ratio = (float)Screen.width / Screen.height;
        cam.orthographicSize = fixedWidth / (2f * ratio);

        Vector3 targetPos = player.transform.position;
        targetPos.z = -10;


        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPos,
            ref velocity,
            smoothTime
        );

    }
}
