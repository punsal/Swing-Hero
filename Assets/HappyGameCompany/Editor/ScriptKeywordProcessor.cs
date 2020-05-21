// Edited from https://gist.github.com/JoaoBorks/af59720a4baba84f080e2df686cacba2
using UnityEngine;
using UnityEditor;

internal sealed class ScriptKeywordProcessor : UnityEditor.AssetModificationProcessor {
    public static bool onEnterEnabled = true;
    public static bool onExecuteEnabled = true;
    public static bool onExitEnabled = true;
    public static bool onPlayerInputEnabled = false;

    public static void OnWillCreateAsset(string path) {
        path = path.Replace(".meta", "");
        int index = path.LastIndexOf(".");
        if (index < 0)
            return;

        string file = path.Substring(index);
        if (file != ".cs" && file != ".js")
            return;

        index = Application.dataPath.LastIndexOf("Assets");
        path = Application.dataPath.Substring(0, index) + path;

        if (!System.IO.File.Exists(path))
            return;

        string fileContent = System.IO.File.ReadAllText(path);

        int index2 = path.LastIndexOf("/");
        string name = path.Substring(index2 + 1);

        if (name.Contains(".cs")) {
            name = name.Replace(".cs", "");
        }
        else if (name.Contains(".js")) {
            name = name.Replace(".js", "");
        }

        name = name.Replace("State", "");

        //Add desired keyword here
        fileContent = fileContent.Replace("#NAME#", name);
        fileContent = fileContent.Replace("#CREATIONDATE#", System.DateTime.Now.ToString("dd/MM/yy"));

        fileContent = ProcessOptionalFunctionality(fileContent, "#ONENTER#", onEnterEnabled);
        fileContent = ProcessOptionalFunctionality(fileContent, "#ONEXECUTE#", onExecuteEnabled);
        fileContent = ProcessOptionalFunctionality(fileContent, "#ONEXIT#", onExitEnabled);
        fileContent = ProcessOptionalFunctionality(fileContent, "#ONPLAYERINPUT#", onPlayerInputEnabled);
        System.IO.File.WriteAllText(path, fileContent);
        AssetDatabase.Refresh();
    }

    static string ProcessOptionalFunctionality(string scriptTemplate, string keyword, bool enabled) {
        int keywordCount = CountStringOccurrences(scriptTemplate, keyword);
        if (keywordCount % 2 != 0) {
            Debug.LogWarning("NewStateTemplate has an erroneous " + keyword + " count");
            scriptTemplate = scriptTemplate.Replace(keyword, "");
        }
        else if (!enabled) {
            while (keywordCount > 0) {
                int index1 = scriptTemplate.IndexOf(keyword);
                int index2 = scriptTemplate.IndexOf(keyword, index1 + 1);
                scriptTemplate = scriptTemplate.Remove(index1, index2 + keyword.Length - index1);
                keywordCount -= 2;
            }
        }
        else {
            scriptTemplate = scriptTemplate.Replace(keyword, "");
        }

        return scriptTemplate;
    }

    static int CountStringOccurrences(string text, string pattern) {
        //Loop through all instances of the string 'text'.
        int count = 0;
        int i = 0;
        while ((i = text.IndexOf(pattern, i)) != -1) {
            i += pattern.Length;
            count++;
        }
        return count;
    }
}