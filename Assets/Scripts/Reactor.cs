using UnityEngine;
using TMPro; // Swap to UnityEngine.UI.Text if you don't use TextMeshPro.

// Drop this on a GameObject that the player walks into to finish the level.
// It only fires when LevelManager reports AllCollected == true.
//
// Setup:
//   1. Create the reactor mesh (Cylinder, ProBuilder shape, whatever).
//   2. Add a Collider, tick "Is Trigger".
//   3. (Optional) Make a child Point Light + child Particle System.
//      Drag them into the Armed Light / Armed Particles slots so they
//      switch on the moment all items are collected — gives the player
//      a visual "now I can finish" cue.
//   4. (Optional) Make a UI TMP_Text for the "collect everything first"
//      hint and drag it into the Hint Text slot.
//   5. Player must be tagged "Player".
public class Reactor : MonoBehaviour
{
    [Header("Visuals (optional)")]
    [Tooltip("Light that switches on when all items have been collected.")]
    public Light armedLight;

    [Tooltip("Particle System that plays when all items have been collected.")]
    public ParticleSystem armedParticles;

    [Header("Player feedback (optional)")]
    [Tooltip("UI text shown briefly if the player touches the reactor too early.")]
    public TMP_Text hintText;

    [Tooltip("How long the hint stays on screen, in seconds.")]
    public float hintDuration = 2f;

    bool armed = false;
    float hintHideTime = 0f;

    void Start()
    {
        // Reset visuals to "locked" state at startup.
        if (armedLight != null) armedLight.enabled = false;
        if (armedParticles != null) armedParticles.Stop();
        if (hintText != null) hintText.gameObject.SetActive(false);

        // Ask the LevelManager to tell us when the gate opens.
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.OnAllCollected += Arm;
        }
    }

    void OnDestroy()
    {
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.OnAllCollected -= Arm;
        }
    }

    void Update()
    {
        // Auto-hide the hint after its duration.
        if (hintText != null && hintText.gameObject.activeSelf
            && Time.unscaledTime >= hintHideTime)
        {
            hintText.gameObject.SetActive(false);
        }
    }

    void Arm()
    {
        armed = true;
        if (armedLight != null) armedLight.enabled = true;
        if (armedParticles != null) armedParticles.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (LevelManager.Instance == null) return;

        if (LevelManager.Instance.AllCollected)
        {
            LevelManager.Instance.TriggerEndZone();
        }
        else
        {
            int got = LevelManager.Instance.Collected;
            int total = LevelManager.Instance.totalItems;
            ShowHint($"Collect all items first ({got}/{total})");
        }
    }

    void ShowHint(string message)
    {
        Debug.Log("[Reactor] " + message);
        if (hintText == null) return;
        hintText.text = message;
        hintText.gameObject.SetActive(true);
        hintHideTime = Time.unscaledTime + hintDuration;
    }
}
