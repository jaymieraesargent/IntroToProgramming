using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerPrefs : MonoBehaviour
{
    public string exampleName;

    public void Save(string name)
    {
        PlayerPrefs.SetString("Character",name);
    }
    public void Load(string name)
    {
        if(PlayerPrefs.HasKey(name))
        {
            exampleName = PlayerPrefs.GetString(name);
        }
    }
}
