using UnityEngine;

namespace DialogueSystem
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private SceneLoadManager sceneLoader;

        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            sceneLoader = GameManager.instance.GetComponent<SceneLoadManager>();

            sceneLoader.LoadNextScene();
        }
    }
}
