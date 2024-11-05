using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;

public class SaveTextFile : MonoBehaviour
{
    private void Awake()
    {
        LoadTextFile("Keys");
    }
    public void SaveAndCreateTextFile(string fileName)
    {
        //path 
        string savePath = Application.dataPath + $"/{fileName}.txt";
        //check of file is at path
        if(!File.Exists(savePath))
        {
            //if not make file
            File.WriteAllText(savePath, fileName);
        }
        //gather content and  format
        string content = "";
        foreach (var key in Keybinds.keys)
        {
            content += $"{key.Key}:{key.Value}~";
        }
        //Replace override
        File.WriteAllText (savePath, content);
    }

    public void LoadTextFile(string fileName)
    {
        //path
        string savePath = Application.dataPath + $"/{fileName}.txt";
        //check to see if file exists
        if (File.Exists(savePath))
        {
            //read the content of the file
            string content = File.ReadAllText (savePath);
            //split the data where the ~ is and hold in a collection
            string[] keyValuePair = content.Split('~');
            //Clear keys in bindings
            Keybinds.keys.Clear();
            //process each key pair
            foreach (string pair in keyValuePair)
            {
                //if the string is empty continue
                if (string.IsNullOrWhiteSpace(pair)) continue;
                //split the string at : 
                string[] keyValue = pair.Split(":");
                //add to dictionary
                if (keyValue.Length == 2)
                {
                    Keybinds.keys.Add(keyValue[0], (KeyCode)Enum.Parse(typeof(KeyCode), keyValue[1]));
                }
            }
        }
        else
        {
            Debug.LogWarning("MISSING TEXT FILE TO LOAD!");
        }
    }
}
