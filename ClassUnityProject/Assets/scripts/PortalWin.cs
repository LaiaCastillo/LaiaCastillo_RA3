using UnityEngine;

namespace DialogueSystem
{
    
    public class PortalWin : MonoBehaviour
    {
        [SerializeField] private GameOverManager gameOver;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (gameOver == null)
            {
                gameOver = GameManager.instance.GetComponent<GameOverManager>();
            }
                
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            gameOver.GameOver(true);
        }
    }
}
