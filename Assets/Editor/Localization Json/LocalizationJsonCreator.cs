using System;
using UnityEditor;
using System.IO;
using System.Threading.Tasks;

public class LocalizationJsonCreator : Editor
{
    [MenuItem("Localization/Create Json")]
    static async Task CreateJsonAsync()
    {
        string innerJson = "";
        foreach (string i in Enum.GetNames(typeof(LocalizationKeys)))
        {
            innerJson += string.Format("\"{0}\":\"{1}\",", i, i.ToLower());
        }
        string iJson = innerJson.Remove(innerJson.Length - 1);
        string json = "{" + iJson + "}";

        await File.WriteAllTextAsync(Path.Combine("Assets", "Resources", "LocalizationKey.json"), json);

        AssetDatabase.Refresh();
    }
}