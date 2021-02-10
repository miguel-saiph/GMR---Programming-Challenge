using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using LitJson;

public class DynamicTableGenerator : MonoBehaviour
{
    private string path;
    [SerializeField] private string fileName = "JsonChallenge.json";
    private JsonData itemData;
    [SerializeField] private Text titleText;
    [SerializeField] private Transform header;
    [SerializeField] private Transform rowSpace;
    
    void Awake()
    {
        path = Application.dataPath + "/StreamingAssets";
        
        CreateTable();
    }

    public void CreateTable() {

        DeleteTable();
        string filePath = path + "/" + fileName;
        string dataAsJson = File.ReadAllText (filePath);
        itemData = JsonMapper.ToObject(dataAsJson);

        JsonObject data = JsonUtility.FromJson<JsonObject>(dataAsJson);

        titleText.text = data.Title;

        for (int i = 0; i < data.ColumnHeaders.Length; i++) {   
            //Debug.Log(data.ColumnHeaders[i]);
            CreateColumnHeader(data.ColumnHeaders[i]);
        }

        Debug.Log(itemData[2].Count);
        for (int i = 0; i < itemData[2].Count; i++){
            CreateRow(itemData[2][i]);
        }
        
        //string json = JsonUtility.ToJson(data.Data[0]);
        //JsonData member = JsonMapper.ToObject(data.Data[0]);
        string json = JsonUtility.ToJson(data.Data);
    }

    private void DeleteTable() {
        
        titleText.text = "";
        
        foreach (Transform child in header) {
            Destroy(child.gameObject);
        }
        
        foreach (Transform child in rowSpace) {
            Destroy(child.gameObject);
        }
    }

    private void CreateColumnHeader(string name) {
        
        // Creates the header column
        GameObject headerColumn = new GameObject("Header", typeof(Text));
        headerColumn.transform.SetParent(header, false);

        // Set the name of the column
        Text headerText = headerColumn.GetComponent<Text>();
        headerText.text = name;

        // Set the style of the text
        headerText.GetComponent<RectTransform>().sizeDelta = new Vector2(130, 60);
        headerText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        headerText.fontSize = 20;
        headerText.fontStyle = FontStyle.Bold;
        headerText.alignment = TextAnchor.MiddleCenter;
    }

    private void CreateRow(JsonData data) {
        
        // Creates the row parent
        GameObject row = new GameObject("Row", typeof(HorizontalLayoutGroup));
        row.transform.SetParent(rowSpace, false);
        row.GetComponent<RectTransform>().sizeDelta = new Vector2(550, 60);
        row.GetComponent<HorizontalLayoutGroup>().childControlWidth = false;
        row.GetComponent<HorizontalLayoutGroup>().childControlHeight = false;

        for (int i = 0; i < data.Count; i++) {
            GameObject dataColumn = new GameObject("DataColumn", typeof(Text));
            dataColumn.transform.SetParent(row.transform, false);

            // Set the name of the column
            Text dataText = dataColumn.GetComponent<Text>();
            dataText.text = data[i].ToString();

            // Set the style of the text
            dataText.GetComponent<RectTransform>().sizeDelta = new Vector2(120, 60);
            dataText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            dataText.fontSize = 18;
            dataText.alignment = TextAnchor.MiddleCenter;
            dataText.color = new Color(0.1960784f, 0.1960784f, 0.1960784f);
        }
        
    }
}