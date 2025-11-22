using DialogueSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] bool UNITY_EDITOR = true;
    [SerializeField] private SceneLoadManager sceneLoadManager;
    public static GameManager instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        sceneLoadManager = GetComponent<SceneLoadManager>();
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        // me estaba dando errores y no me da la vida DontDestroyOnLoad(gameObject);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 0 && instance != null)
        {
            Destroy(instance.gameObject);
            instance = null;
        }
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReestartGame()
    {
        Time.timeScale = 1f;
        sceneLoadManager.LoadScene(1);
    }
    public void ExitGame()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Detiene el modo Play
#else
            Application.Quit(); // Detiene la aplicación en build
#endif
        }
        else
        {
            sceneLoadManager.LoadScene(0);
        }
        
        

    }
}
