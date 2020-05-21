using UnityEngine;
using UnityEditor;

public class CreateStateWindow : EditorWindow {
    string stateName = "NewState";

    Texture2D happyLogo;
    Texture2D headerTexture;

    Rect headerRect;
    Rect logoRect;

    Rect bodyRect;

    [MenuItem("HappyGame/Create New State")]
    public static void ShowWindow() {
        CreateStateWindow window = GetWindow<CreateStateWindow>("Create New State");
        window.minSize = window.maxSize = new Vector2(235, 250);
    }

    private void OnEnable() {
        InitTextures();
    }

    private void OnGUI() {
        DrawLayouts();
        DrawHeader();
        DrawBody();
    }

    void InitTextures() {
        headerTexture = new Texture2D(1, 1);
        headerTexture.SetPixel(0, 0, Color.black);
        headerTexture.Apply();

        happyLogo = Resources.Load<Texture2D>("HappyGameCompany/Images/default_icon_64");
    }

    void DrawLayouts() {
        headerRect.x = 0;
        headerRect.y = 0;
        headerRect.width = Screen.width;
        headerRect.height = 64f;

        logoRect.x = 0;
        logoRect.y = 0;
        logoRect.width = 64;
        logoRect.height = 64;

        bodyRect.x = 0;
        bodyRect.y = 64f;
        bodyRect.width = Screen.width;
        bodyRect.height = 186f;
    }

    void DrawHeader() {
        GUILayout.BeginArea(headerRect);

        GUI.DrawTexture(headerRect, headerTexture);
        GUI.DrawTexture(logoRect, happyLogo);

        //TODO: Replace this with a texture perhaps
        EditorGUI.LabelField(new Rect(70, 16, position.width, 32), "STATE\nCREATOR");


        GUILayout.EndArea();
    }

    void DrawBody() {
        GUILayout.BeginArea(bodyRect);

        GUILayout.Space(10);
        stateName = EditorGUILayout.TextField("State Name", stateName);
        GUILayout.Space(10);
        ScriptKeywordProcessor.onEnterEnabled = EditorGUILayout.Toggle("onEnter", ScriptKeywordProcessor.onEnterEnabled);
        ScriptKeywordProcessor.onExecuteEnabled = EditorGUILayout.Toggle("onExecute", ScriptKeywordProcessor.onExecuteEnabled);
        ScriptKeywordProcessor.onExitEnabled = EditorGUILayout.Toggle("onExit", ScriptKeywordProcessor.onExitEnabled);
        GUILayout.Space(10);
        ScriptKeywordProcessor.onPlayerInputEnabled = EditorGUILayout.Toggle("onPlayerInput", ScriptKeywordProcessor.onPlayerInputEnabled);
        GUILayout.Space(15);
        if (GUILayout.Button("Create", GUILayout.Height(40))) {
            ScriptCreator.CreateScriptAsset("Assets/HappyGameCompany/ScriptTemplates/NewStateTemplate.cs.txt", "Assets/_Scripts/State Machine/States/" + stateName + ".cs");
            this.Close();
        }

        GUILayout.EndArea();
    }
}
