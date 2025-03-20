using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public partial class DataManager : MonoBehaviour // Data Field
{

}
public partial class DataManager : MonoBehaviour // Initialize
{
    private void Allocate()
    {

    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {

    }
}
public partial class DataManager : MonoBehaviour // Property
{
    #region Json

    public void LoadJson<T>(string path, Dictionary<string, T> dataDict) where T : BaseInformation
    {
        dataDict.Clear();
        TextAsset jsonFile = Resources.Load<TextAsset>($"Json/{path}");
        Wrapper<T> jsonData = ParseJson<T>(jsonFile.text);

        foreach (T data in jsonData.array)
        {
            dataDict.Add(data.index, data);
        }
    }
    private Wrapper<T> ParseJson<T>(string jsonData) where T : BaseInformation
    {
        if (string.IsNullOrEmpty(jsonData))
            Debug.LogWarning("JsonData Is Null Or Empty");

        return JsonUtility.FromJson<Wrapper<T>>(jsonData);
    }


    #endregion
    #region Csv
    public void LoadCsv<T>(string path, Dictionary<string, T> dataDict) where T : BaseInformation, new()
    {
        dataDict.Clear();
        TextAsset csvFile = Resources.Load<TextAsset>($"Csv/{path}");
        if (csvFile != null)
        {
            string[] csvValueArray = csvFile.text.Split('\n');
            string[] csvFieldName = csvValueArray[0].Split(',');

            for (int i = 1; i < csvValueArray.Length; i++)
            {
                string[] csvValue = csvValueArray[i].Split(',');
                dataDict.Add(csvValue[0], ParseCsv<T>(csvFieldName, csvValue));
            }
        }
    }

    private T ParseCsv<T>(string[] csvFieldName, string[] csvValue) where T : BaseInformation, new()
    {
        T data = new T();
        FieldInfo[] fieldInfoArray = typeof(T).GetFields();
        string[] arrayData;

        for (int i = 0; i < csvFieldName.Length; i++)
        {
            string currentCsvKey = csvFieldName[i].Trim();
            string currentCsvValue = csvValue[i].Trim();
            FieldInfo currentField = null;
            try
            {
                currentField = fieldInfoArray.SingleOrDefault(info => info.Name == currentCsvKey);

                if (currentField != null)
                {
                    if (!currentField.FieldType.IsArray)
                        currentField.SetValue(data, Convert.ChangeType(currentCsvValue[i], currentField.FieldType));

                    else
                    {
                        Type arrayType = currentField.FieldType.GetElementType();
                        arrayData = currentCsvValue.Split(' ');
                        Array array = Array.CreateInstance(arrayType, arrayData.Length);
                        for (int j = 0; j < array.Length; j++)
                        {
                            try
                            {
                                var element = Convert.ChangeType(arrayData[j], arrayType);
                                array.SetValue(element, j);
                            }
                            catch
                            {
                                Debug.LogWarning($"Converting Error / Cant Convert [{arrayData[j]}] to [{arrayType.Name}]");
                            }
                        }
                        currentField.SetValue(data, array);
                    }
                }
                else
                    Debug.LogWarning($"No Matching Filed Found For Key : {currentCsvKey}");
            }
            catch
            {
                Debug.LogWarning($"Field Name Is Duplicate [name :{currentField.Name}]");
            }
        }
        return data;
    }


    #endregion
}