using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileManager
{
  //this is a class that manages load and saves and filepaths

    //here I specify directories of the texts and if I want to include blank lines FROM RESOURCES EXTERNAL TO UNITY
    public static List<string> ReadTextFile(string filePath, bool includeBlankLines = true)
    {
        //check if it's absolute
        if (!filePath.StartsWith('/'))
            filePath = FilePaths.ROOT + filePath;

        List<string> lines = new List<string>();
        try
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                while(!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    if (includeBlankLines || string.IsNullOrWhiteSpace(line))
                        lines.Add(line);
                }
            }
        }
        catch (FileNotFoundException ex)
        {
            Debug.LogError($"File not found: '{ex.FileName}'");
        }

        return lines;
    }

    //here I specify directories of the texts and if I want to include blank lines FROM TEXTS FILES WITHIN UNITY
    public static List<string> ReadTextAsset(string filePath, bool includeBlankLines = true)
    {
        TextAsset asset = Resources.Load<TextAsset>(filePath);
        if (asset == null)
        {
            Debug.LogError($"Asset not found: ' {filePath} ' ");
            return null;
        }

        return ReadTextAsset(asset, includeBlankLines);
    }

    public static List<string> ReadTextAsset(TextAsset asset, bool includeBlankLines = true)
    {
        List<string> lines = new List<string>();
        using (StringReader sr = new StringReader(asset.text))
        {
            while (sr.Peek() > -1)
            {
                string line = sr.ReadLine();
                if (includeBlankLines || !string.IsNullOrWhiteSpace(line))
                    lines.Add(line);
            }
        }

        return lines;
    }

}
