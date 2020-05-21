using UnityEngine;
using UnityEditor;

public class CreateControllerWindow : EditorWindow {
    string controllerName = "NewController";

    Texture2D happyLogo;
    Texture2D headerTexture;

    Rect headerRect;
    Rect logoRect;

    Rect bodyRect;

    [MenuItem("HappyGame/Create New Controller")]
    public static void ShowWindow() {
        CreateControllerWindow window = GetWindow<CreateControllerWindow>("Create New Controller");
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
        EditorGUI.LabelField(new Rect(70, 16, position.width, 32), "CONTROLLER\nCREATOR");


        GUILayout.EndArea();
    }

    void DrawBody() {
        GUILayout.BeginArea(bodyRect);

        GUILayout.Space(10);
        controllerName = EditorGUILayout.TextField("Controller Name", controllerName);
        GUILayout.Space(107);
        if (GUILayout.Button("Create", GUILayout.Height(40))) {
            ScriptCreator.CreateScriptAsset("Assets/HappyGameCompany/ScriptTemplates/NewControllerTemplate.cs.txt", "Assets/_Scripts/Controllers/" + controllerName + ".cs");
            this.Close();
        }

        GUILayout.EndArea();
    }
}
