using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;

public class TestScript : MonoBehaviour
{
    private string path;
    private JsonData itemData;
    
    void Awake()
    {
        path = Application.dataPath + "/StreamingAssets";
        string filePath = path + "/JsonChallenge.json";
        string dataAsJson = File.ReadAllText (filePath);
        Debug.Log(dataAsJson);
        itemData = JsonMapper.ToObject(dataAsJson);
        Debug.Log(itemData[0]);
        //LocalizationData loadedData = JsonUtility.FromJson<LocalizationData> (dataAsJson);
    }
}
