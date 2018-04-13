using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public class FileManager : Singleton<FileManager> {

    readonly string outputJsonFilePath = Application.dataPath + "/Output/Json/";

    private FileManager() {}

    public T LoadJson<T>(string path) {
        string filePath = Application.dataPath + path;

        if (!File.Exists(filePath))
            Debug.LogError("文件路径:" + filePath + "不存在!!!");
        
        string jsonData = File.ReadAllText(filePath);
        TextAsset jsonText = new TextAsset(jsonData);
        //T jsonObj = JsonUtility.FromJson<T>(jsonText.text);
        T jsonObj = JsonConvert.DeserializeObject<T>(jsonText.text);
        return jsonObj;
    }

    public void OutPutJsonFile(string name, string text) {
        StreamWriter writer;
        FileInfo fileInfo = new FileInfo(outputJsonFilePath + name);
        if (!fileInfo.Exists)
            writer = fileInfo.CreateText();
        else{
            fileInfo.Delete();
            writer = fileInfo.CreateText();
        }

        writer.WriteLine(text);
        writer.Close();
        writer.Dispose();
    }
}
