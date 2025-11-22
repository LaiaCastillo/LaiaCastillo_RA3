using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private HealthBehaviour hB;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject winScreenUI;

    private void Awake()
    {
        StartCoroutine(AssignHealthBehaviourWhenReady());
    }

    private void OnDisable()
    {
        if (hB != null)
            hB.GameOverEvent -= GameOver;
    }

    private IEnumerator AssignHealthBehaviourWhenReady()
    {
        // Espera hasta que Player.instance exista
        while (Player.instance == null)
        {
            yield return null; // espera un frame
        }
        hB = Player.instance.GetComponent<HealthBehaviour>();
        if (hB != null)
        {
            hB.GameOverEvent += GameOver;
        }
        else
        {
            Debug.LogWarning("GameOverManager: HealthBehaviour no encontrado en Player");
        }
    }

    public void GameOver(bool win)
    {
        Time.timeScale = 0f;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (win && winScreenUI != null)
            winScreenUI.SetActive(true);
        else if (!win && gameOverUI != null)
            gameOverUI.SetActive(true);
    }
}
