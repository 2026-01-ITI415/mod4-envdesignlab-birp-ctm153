using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

// Attach this script to any GameObject in your bootstrap scene (the scene at index 0 in Build Settings).
// It reads the ?scene= URL parameter at startup and loads the matching scene.
//
// URLs:
//   https://yourname.github.io/game/?scene=1  -> loads the terrain scene ("3d")
//   https://yourname.github.io/game/?scene=2  -> loads the game objects scene ("SampleScene")
//   No parameter (or anything else)           -> loads the terrain scene ("3d") by default

public class SceneLoader : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string GetURLParameter(string name);

    // Edit these two strings if you ever rename your scenes.
    private const string TerrainSceneName = "3d";
    private const string GameObjectsSceneName = "SampleScene";

    void Start()
    {
        string scene = "1"; // default to scene 1

        #if UNITY_WEBGL && !UNITY_EDITOR
            scene = GetURLParameter("scene");
        #endif

        if (scene == "2")
        {
            SceneManager.LoadScene(GameObjectsSceneName);
        }
        else
        {
            SceneManager.LoadScene(TerrainSceneName);
        }
    }
}
