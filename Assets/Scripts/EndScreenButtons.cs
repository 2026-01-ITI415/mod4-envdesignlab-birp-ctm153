using UnityEngine;
using UnityEngine.SceneManagement;

// Hook these methods into your end-screen buttons via the Button's OnClick() event.
// Drop this script onto any GameObject in the scene (the EndCanvas itself works).
public class EndScreenButtons : MonoBehaviour
{
    // Reload the current scene from scratch.
    public void RestartLevel()
    {
        // Time was frozen on level end — un-freeze before reloading.
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.name);
    }

    // Quit the application. No-op in the WebGL build (browsers can't self-close),
    // but works in the Unity editor and in standalone builds.
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
