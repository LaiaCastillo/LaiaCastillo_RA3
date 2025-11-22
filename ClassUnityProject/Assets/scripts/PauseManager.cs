using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField] public Player player;
    private bool isPaused = false;
    [SerializeField] private GameObject pausedUI;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        StartCoroutine(RegisterPlayer());
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (player != null)
            player.PauseGameEvent -= PauseGame;
    }
    IEnumerator RegisterPlayer()
    {
        while (Player.instance == null)
            yield return null;

        player = Player.instance;
        player.PauseGameEvent += PauseGame;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        EnsureEventSystemExists();
    }

    private void EnsureEventSystemExists()
    {
        if (FindAnyObjectByType<EventSystem>() == null)
        {
            GameObject es = new GameObject("EventSystem");

            es.AddComponent<EventSystem>();
            es.AddComponent<UnityEngine.InputSystem.UI.InputSystemUIInputModule>();

        }
    }

    public void PauseGame()
    {
        isPaused = !isPaused;
        pausedUI?.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;

        Cursor.visible = isPaused;
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }
}
