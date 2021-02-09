using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TestScript : MonoBehaviour
{
    private string path;
    
    void Awake()
    {
        path = Application.dataPath + "/StreamingAssets";
        string filePath = path + "/JsonChallenge.json";
        string dataAsJson = File.ReadAllText (filePath);
        Debug.Log(dataAsJson);
        //LocalizationData loadedData = JsonUtility.FromJson<LocalizationData> (dataAsJson);
    }
}
