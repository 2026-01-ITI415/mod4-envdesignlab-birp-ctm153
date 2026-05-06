using UnityEngine;
using UnityEngine.SceneManagement;

// Drop this on a Cube in front of a doorway. When the player walks into the
// trigger volume, the named scene loads.
//
// Setup:
//   1. GameObject -> 3D Object -> Cube. Position and scale to cover the doorway.
//   2. On the BoxCollider, tick "Is Trigger".
//   3. Disable (uncheck) the Mesh Renderer so the cube is invisible at runtime.
//   4. Add this script. Set "Target Scene Name" to the scene you want to load
//      (must match the scene file name AND be added to File -> Build Settings).
//   5. Make sure your First Person Controller is tagged "Player".
public class SceneTrigger : MonoBehaviour
{
    [Tooltip("Exact scene name as it appears in Build Settings.")]
    public string targetSceneName;

    [Tooltip("Optional: identifier so the next scene knows which door you came from.")]
    public string spawnPointID = "";

    bool fired = false;

    void OnTriggerEnter(Collider other)
    {
        if (fired) return;
        if (!other.CompareTag("Player")) return;

        fired = true;

        // Remember which door the player used so the destination scene
        // can spawn them in the matching position.
        if (!string.IsNullOrEmpty(spawnPointID))
        {
            PlayerPrefs.SetString("LastSpawnPoint", spawnPointID);
        }

        SceneManager.LoadScene(targetSceneName);
    }
}
