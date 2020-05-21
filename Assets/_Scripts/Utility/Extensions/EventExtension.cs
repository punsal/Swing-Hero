using System;

public class EventExtension {
    public static void PrintError(Exception e) {
        UnityEngine.Debug.LogWarning(e.Source + " Sends : " + e.Message);
    }

    public static void ThrowMessage(string name) {
        throw new Exception(name + " has no subscriber.");
    }
}