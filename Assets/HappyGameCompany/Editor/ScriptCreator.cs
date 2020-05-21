using UnityEditor;

public class ScriptCreator {
    //[MenuItem("HappyGame/Create New State")]
    //[MenuItem("Assets/Create/HappyGame/Create New State")]
    static void CreateNewState() {
        CreateScriptAsset("Assets/HappyGameCompany/ScriptTemplates/NewStateTemplate.cs.txt", "Assets/_Scripts/State Machine/States/NewState.cs");
    }

    //[MenuItem("HappyGame/Create New Controller")]
    //[MenuItem("Assets/Create/HappyGame/Create New Controller")]
    static void CreateNewController() {
        CreateScriptAsset("Assets/HappyGameCompany/ScriptTemplates/NewControllerTemplate.cs.txt", "Assets/_Scripts/Controllers/NewController.cs");
    }

    public static void CreateScriptAsset(string templatePath, string newScriptPath) {
        typeof(UnityEditor.ProjectWindowUtil)
            .GetMethod("CreateScriptAsset", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
            .Invoke(null, new object[] { templatePath, newScriptPath });
    }
}
