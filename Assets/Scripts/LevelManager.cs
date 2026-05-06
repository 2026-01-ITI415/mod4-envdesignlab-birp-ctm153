using UnityEngine;
using TMPro; // If you don't have TextMeshPro, swap TMP_Text for UnityEngine.UI.Text

// Tracks score + time, ends the level on full collection, end-zone trigger, or timeout.
// Place one in the scene as an empty GameObject called "LevelManager".
public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [Header("Run Settings")]
    public int totalItems = 2;
    public float timeLimit = 300f; // seconds

    [Header("UI Refs")]
    public GameObject endCanvas;   // disabled at start, shown at end
    public TMP_Text statsText;     // assign the Text on the end canvas
    public TMP_Text counterText;   // top-right HUD counter, e.g. "Materials Collected: 1/2"

    int collected = 0;
    float startTime;
    bool ended = false;

    // Public read-only state so the Reactor (and anything else) can check progress.
    public int Collected => collected;
    public bool AllCollected => collected >= totalItems;

    // Anything (e.g. the Reactor) can subscribe to know the moment the gate opens.
    public System.Action OnAllCollected;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        startTime = Time.time;
        if (endCanvas != null) endCanvas.SetActive(false);
        UpdateCounter();
    }

    void Update()
    {
        if (ended) return;
        if (Time.time - startTime >= timeLimit) EndLevel("Time's up!");
    }

    public void CollectItem()
    {
        collected++;
        UpdateCounter();
        // No longer auto-ends here. The Reactor handles the level-end now.
        if (collected >= totalItems)
        {
            OnAllCollected?.Invoke();
        }
    }

    void UpdateCounter()
    {
        if (counterText != null)
        {
            counterText.text = $"Materials Collected: {collected}/{totalItems}";
        }
    }

    public void TriggerEndZone()
    {
        EndLevel("Reactor activated!");
    }

    void EndLevel(string reason)
    {
        if (ended) return;
        ended = true;

        float elapsed = Time.time - startTime;
        if (statsText != null)
        {
            statsText.text = $"{reason}\nItems: {collected}/{totalItems}\nTime: {elapsed:F1}s";
        }
        if (endCanvas != null) endCanvas.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
}
