using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DIALOGUE;

namespace TESTING
{
    public class TestParsing : MonoBehaviour
    {
        [SerializeField] private TextAsset fileToRead = null;

        // Start is called before the first frame update
        void Start()
        {
            SendFileToParse();

        }

       void SendFileToParse()
        {
            List<string> lines = FileManager.ReadTextAsset(fileToRead);

            foreach(string line in lines)
            {
                //check and skip empty lines
                if (line == string.Empty)
                    continue;

                DIALOGUE_LINE dl = DialogueParser.Parse(line);
            }
        }
    }
}
