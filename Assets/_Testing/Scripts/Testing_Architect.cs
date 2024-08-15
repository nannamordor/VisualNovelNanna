using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

namespace TESTING
{

    public class Testing_Architect : MonoBehaviour
    {
        DialogueSystem ds;
        TextArchitect architect;

        public TextArchitect.BuildMethod bm = TextArchitect.BuildMethod.instant;

        string[] lines = new string[5]
        {
            "Hey",
            "This is my first line of dialogue",
            "Guess what",
            "Kiss my ass",
            "Lov u"
        };

        // Start is called before the first frame update
        void Start()
        {
            ds = DialogueSystem.instance;
            architect = new TextArchitect(ds.dialogueContainer.dialogueText);
            architect.buildMethod = TextArchitect.BuildMethod.fade;
            architect.speed = 0.5f;
        }

        // Update is called once per frame
        void Update()
        {
            //update build method
            if (bm != architect.buildMethod)
            {
                architect.buildMethod = bm;
                architect.Stop();
            }

            //stop with S key down
            if (Input.GetKeyDown(KeyCode.S))
                architect.Stop();

            //add a longline
            string longLine = "very long line bla bla bla bla bla bla bla"; 
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (architect.isBuilding)
                {
                    if (!architect.hurryUp)
                        architect.hurryUp = true;
                    else
                        architect.ForceComplete();
                }
                else
                    architect.Build(longLine + "miao");
                    //architect.Build(lines[Random.Range(0, lines.Length)]);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                architect.Append(longLine);
                //architect.Append(lines[Random.Range(0, lines.Length)]);
            }

        }

    }
}
