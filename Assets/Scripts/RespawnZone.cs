using UnityEngine;

// Drop this on a trigger volume placed at the bottom of a pit / off-map area.
// When the player walks (or falls) into the trigger, they get teleported to
// the assigned spawn point.
//
// Setup:
//   1. Create an empty GameObject called "RespawnPoint" at the top of your hill.
//      Position it where you want the player to reappear (slightly above ground).
//   2. GameObject -> 3D Object -> Cube. Stretch it big enough to cover the
//      whole pit floor. Disable its Mesh Renderer (invisible).
//   3. Tick "Is Trigger" on the BoxCollider.
//   4. Add this script. Drag your RespawnPoint into the "Spawn Point" slot.
//   5. Make sure the player is tagged "Player".
public class RespawnZone : MonoBehaviour
{
    [Tooltip("Where the player gets teleported when this trigger fires.")]
    public Transform spawnPoint;

    [Tooltip("Optional sound to play on respawn.")]
    public AudioClip respawnSound;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (spawnPoint == null)
        {
            Debug.LogWarning("[RespawnZone] No Spawn Point assigned.");
            return;
        }

        // CharacterController fights direct position changes. Disable it
        // briefly while we move the player, then re-enable.
        CharacterController cc = other.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        other.transform.position = spawnPoint.position;
        other.transform.rotation = spawnPoint.rotation;

        if (cc != null) cc.enabled = true;

        // If the player has a Rigidbody (e.g. roller-ball setup), zero out velocity
        // so they don't keep their fall momentum after teleporting.
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        if (respawnSound != null)
        {
            AudioSource.PlayClipAtPoint(respawnSound, spawnPoint.position);
        }
    }
}
