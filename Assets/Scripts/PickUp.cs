using UnityEngine;

// Roll-a-Ball-style pickup, adapted for a First Person Controller.
// The trigger lives on the pickup itself instead of the player so we don't
// have to modify the Standard Assets FPC script.
//
// Setup:
//  1. Create a Cube, scale it ~0.5, add a yellow material.
//  2. Add this script + Rotator script.
//  3. Tick "Is Trigger" on the BoxCollider.
//  4. Tag the pickup "Pick Up" (create the tag if it doesn't exist).
//  5. Tag your First Person Controller "Player".
//  6. Drag into Project window to make a prefab. Reuse it across the level.
public class PickUp : MonoBehaviour
{
    [Tooltip("Optional: ping a sound when collected.")]
    public AudioClip collectSound;

    void OnTriggerEnter(Collider other)
    {
        // Diagnostic: prints to Console every time ANYTHING enters the trigger.
        Debug.Log($"[PickUp] Trigger entered by: {other.name} (tag: {other.tag})");

        if (!other.CompareTag("Player"))
        {
            Debug.Log("[PickUp] Ignored — not tagged Player.");
            return;
        }

        // Tell the LevelManager (if present) that an item was collected.
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.CollectItem();
            Debug.Log($"[PickUp] Collected. Total now: {LevelManager.Instance.Collected}/{LevelManager.Instance.totalItems}");
        }
        else
        {
            Debug.LogWarning("[PickUp] No LevelManager.Instance found — add a LevelManager GameObject to the scene.");
        }

        if (collectSound != null)
        {
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
        }

        // Roll-a-Ball deactivated rather than destroyed; either works.
        gameObject.SetActive(false);
    }
}
