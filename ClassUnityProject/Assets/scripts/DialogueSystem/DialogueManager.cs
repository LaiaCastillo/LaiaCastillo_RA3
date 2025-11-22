using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private DialogueUI dialogueUI;
        [SerializeField] private float typeSpeed = 0.05f;
        [SerializeField] private InputAction clickAction;
        [SerializeField] private AudioSource audioSource;

        public bool isDialogInProgress { get; set; } = false;

        public static DialogueManager Instance { get; private set; }
        private Queue<DialogueTurn> dialogueTurnsQueue;

        private void Awake()
        {
            Instance = this;
            dialogueUI.HideDialogBox();
        }

        public void StartDialogue(DialogueRoundSO dialogue)
        {
            if (isDialogInProgress)
            {
                Debug.Log("Ya te estan hablando sordo");
                return;
            }
            isDialogInProgress = true;
            Cursor.visible = isDialogInProgress;
            Cursor.lockState = isDialogInProgress ? CursorLockMode.None : CursorLockMode.Locked;
            dialogueTurnsQueue = new Queue<DialogueTurn>(dialogue.DialogueTurnList);
            StartCoroutine(DialogueCoroutine());
        }

        private IEnumerator DialogueCoroutine()
        {
            dialogueUI.ShowDialogueBox();
            clickAction.Enable();
            while (dialogueTurnsQueue.Count > 0)
            {
                var CurrentTurn = dialogueTurnsQueue.Dequeue();

                dialogueUI.SetCharacterInfo(CurrentTurn.character);
                dialogueUI.ClearDialogue();
                yield return StartCoroutine(TypeSentence(CurrentTurn));
                yield return new WaitUntil(() => clickAction.triggered);


                yield return null;
            }
            dialogueUI.HideDialogBox();
            isDialogInProgress = false;
            Cursor.visible = isDialogInProgress;
            Cursor.lockState = isDialogInProgress ? CursorLockMode.None : CursorLockMode.Locked;
        }

        private IEnumerator TypeSentence(DialogueTurn dialogueTurn)
        {
            var typingSeconds = new WaitForSeconds(typeSpeed);
            foreach (char letter in dialogueTurn.DialogueLine.ToCharArray())
            {
                dialogueUI.AppendToDialogueArea(letter);
                if(!char.IsWhiteSpace(letter)) audioSource.Play();
                yield return typingSeconds;
            }
        }

      
    }
}
