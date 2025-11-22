using UnityEngine;

namespace DialogueSystem
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private DialogueRoundSO dialogue;
        [SerializeField] private AudioSource _aS;
        [ContextMenu("Trigger Dialogue")]
        private void Start()
        {
            try
            {
                _aS = GetComponent<AudioSource>();
            }
            catch 
            {
                _aS = null;
            }

            
        }
        public void TriggerDialogue()
        {
            DialogueManager.Instance.StartDialogue(dialogue);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (_aS != null)
                {
                    Debug.Log("Audio");
                    _aS.Play();
                }
                TriggerDialogue();
            }
        }
    }
}
