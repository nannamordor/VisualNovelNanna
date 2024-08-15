using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFiles : MonoBehaviour
{

    //n.b. Files, from external resources -> I have to specify the extension in the name ex textFile.txt
    //n.b. Assets, from within Unity -> I have to NOT specify the extension in the name ex textFile
    //private string fileName = "testFile";

    [SerializeField] private TextAsset fileName;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Run());   
    }
    
    //ctrl K U per decommentare, ctrl K C per commentare in blocco
    //Load .txt from external path

    //IEnumerator Run()
    //{
    //    List<string> lines = FileManager.ReadTextFile(fileName, true);

    //    foreach (string line in lines)
    //        Debug.Log(line);

    //    yield return null;
    //}

    //Load .txt from internal resources
    IEnumerator Run()
    {
        List<string> lines = FileManager.ReadTextAsset(fileName, true);

        foreach (string line in lines)
            Debug.Log(line);

        yield return null;
    }
}
