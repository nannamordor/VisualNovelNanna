using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace DIALOGUE
{

    public class DialogueSystem : MonoBehaviour
    {
        public DialogueContainer dialogueContainer = new DialogueContainer();
        private ConversationManager conversationManager;
        private TextArchitect architect;
       

        public static DialogueSystem instance;

        public delegate void DialogueSystemEvent();
        public event DialogueSystemEvent onUserPrompt_Next;

        public bool isRunningConversation => conversationManager.isRunning;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                Initialize();
            }
            else
                DestroyImmediate(gameObject);


        }

        bool _initialized = false;
        private void Initialize()
        {
            if (_initialized)
                return;

            architect = new TextArchitect(dialogueContainer.dialogueText);
            conversationManager = new ConversationManager(architect);
        }

        public void OnUserPrompt_Next()
        {
            onUserPrompt_Next?.Invoke();
        }

        public void ShowSpeakerName(string speakerName = "")
        {
            if (speakerName.ToLower() != "narrator")
                dialogueContainer.nameContainer.Show(speakerName);
            else
                HideSpeakerName();
        }

        public void HideSpeakerName() => dialogueContainer.nameContainer.Hide();


        //this method will show anything we want to appear on our screen
        public void Say(string speaker, string dialogue)
        {
            List<string> conversation = new List<string>() { $"{speaker} \"{dialogue}\"" };
            Say(conversation);
        }

        //this method will give us a list of strings that is the entire conversation
        public void Say(List<string> conversation)
        {
            conversationManager.StartConversation(conversation);
        }
    }
}
