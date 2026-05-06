# Module 4 — Environment Design Lab Walkthrough

A start-to-finish plan for the Mod 4 BIRP project. Work top to bottom — each step builds on the last.

---

## Part 1 — The Assignment, Broken Down

### What the level has to demonstrate (skills checklist)

- [ ] Outdoor space using Terrain or ProBuilder (hills, gullies, trees, textures, detail meshes, bump maps)
- [ ] Interior space (SNAPS or ProBuilder + lighting)
- [ ] Player attention direction (points of interest at immediate, mid, and long range)
- [ ] Merged ideas from earlier prototypes (collectibles)
- [ ] Multiple lighting techniques
- [ ] Multiple audio zones
- [ ] Animation adding life to the scene
- [ ] Particle systems for dynamism

### Hard deliverable targets

- [ ] Terrain between 300×300 m and 1000×1000 m
- [ ] Player cannot fall off the terrain (invisible colliders around the perimeter)
- [ ] Hard terrain edges are hidden — pick ONE: keep player away with geometry, place player on an island with stretched water, OR use fog
- [ ] First-person walker placed in a starting area
- [ ] FP walker collects items by running into them
- [ ] Texture/geometry/lighting visibly directs the player to the next objective
- [ ] Player enters an interior, collects more items, and **exits through a different door** back outside
- [ ] Final collection area has a different audio environment from the rest of the level
- [ ] Level ends when (a) all items collected, (b) end trigger entered, OR (c) timer runs out
- [ ] End screen Canvas shows run stats (items collected, time, etc.)

### Submission deliverables

- [ ] Save the scene
- [ ] Commit to repo
- [ ] Build as HTML5 (WebGL) — **no compression**
- [ ] Add the build folder to the repository
- [ ] Commit the build
- [ ] Push to GitHub
- [ ] Submit GitHub Pages link (this is what Crosbie sees)

---

## Part 2 — The Game in One Sentence

> A short first-person scavenger hunt across an outdoor environment that funnels the player into an interior building, then out a back door into a moodier final zone where the last collectibles sit. End screen shows their stats.

That single concept hits every requirement. Don't scope-creep past it.

---

## Part 3 — Build Order (do these in sequence)

### Step 1 — Terrain (outdoor)
1. `GameObject → 3D Object → Terrain`. Open Terrain Settings, set resolution to **500 × 500 m** (safe middle ground).
2. **Sculpt:**
   - Raise Terrain brush → push up a ridge along the perimeter (hides the hard edges and stops the player walking off).
   - Smooth brush to soften ridges.
   - Carve a gully or path leading from the spawn area toward where the building will go — this is your attention-direction tool.
3. **Paint textures (3+ layers):** grass for flat areas, dirt for the path/gully, rock for steep slopes. Each layer should have a normal/bump map assigned in the Layer settings.
4. **Trees:** use the tree painter with the `Tree.prefab` you already have. Cluster them densely around the perimeter, sparse along the path so the path reads as "go this way."
5. **Detail meshes:** grass tufts and small rocks scattered with the Details brush.
6. **Edge safety:** drop empty GameObjects with `BoxCollider`s along the outer ridge as a backstop. Tag them invisible (no Mesh Renderer).

### Step 2 — Hide the seams
Pick **one** edge-hiding strategy and commit:
- **Fog** is easiest — `Window → Rendering → Lighting → Environment → Fog`. Linear fog, start ~80 m, end ~250 m.
- Or stretch a water plane out to the horizon if you went island-style.

### Step 3 — Player
1. Drag in Unity's **First Person Controller** (Standard Assets has one — I see them in your project) into the spawn area.
2. Place spawn on flat ground inside a clearing — this is the player's "where am I, where do I go" moment.
3. Make sure the camera's clipping plane far value matches your fog distance.

### Step 4 — Attention direction (the most-graded part)
Set up three layers of objective:
- **Immediate (within 5–10 m):** a glowing first collectible right in front of the spawn. Add a Point Light + a Particle System (sparkles) on it.
- **Mid-range (~30–60 m):** a lit lantern, a brightly-colored banner, or a path of stones leading toward the building.
- **Long-range (silhouette):** the building itself, lit from inside so the windows glow at dusk. This should be visible from spawn.

### Step 5 — Collectibles (merged prototype idea)
1. Make a simple `Pickup` prefab — small mesh + Sphere Collider set to **Is Trigger**.
2. Add the `Collectible.cs` script (see Part 5 below).
3. Distribute ~5 outside, ~3 inside the building, ~2 in the final audio zone.

### Step 6 — Interior (ProBuilder or SNAPS)
1. Build a small structure with ProBuilder: 4 walls, floor, roof, **two doorways on different walls** (entry and exit are different — this is required).
2. Lighting inside: at least one **baked** light (warm interior glow) + one **realtime** light (a flickering candle or lamp) — that's "different lighting techniques."
3. Drop 3 collectibles inside.

### Step 7 — Audio zones
You need contrast. Use Audio Sources with **3D spatial blend = 1** and tuned min/max distances:
- **Outdoor zone:** ambient wind + birds, low volume.
- **Interior:** muffled tone, maybe a fireplace crackle.
- **Final zone (after the back door):** something distinct — eerie drone, water, anything different. This is where the last collectibles go.

### Step 8 — Animation + Particles
- **Animation:** a flag waving (Animator + simple clip), a door swinging open as you approach (trigger), or rotating collectibles.
- **Particles:** sparkles on each collectible, smoke from a chimney, dust motes inside the building, mist in the final zone.

### Step 9 — End of level
1. Add a `LevelManager` GameObject with the `LevelManager.cs` script (Part 5).
2. End triggers: full collection, an end-zone trigger volume, OR a 5-minute timer.
3. Build a Canvas with: "Level Complete," items collected / total, time elapsed, a Restart button. Disable it on Awake; the LevelManager turns it on.

### Step 10 — Polish pass
Walk the level start to finish. Ask: at every point can I see where to go next? If not, add a light, a banner, or a particle effect there.

---

## Part 4 — Build & Submit (HTML5 + GitHub Pages)

1. **Save the scene** (`Ctrl/Cmd + S`).
2. `File → Build Settings → WebGL → Switch Platform` (this can take a while the first time).
3. **Player Settings → Publishing Settings → Compression Format = Disabled** (the assignment specifies no compression).
4. Add SampleScene to "Scenes In Build."
5. **Build** into a folder named `Build` (or similar) **inside the repo root**. You already have `mod4 build/` and `mod4 build terrain/` — pick one or replace with a fresh build.
6. From the repo root in Terminal:
   ```bash
   git add .
   git commit -m "Mod 4 final build"
   git push
   ```
7. **GitHub Pages setup:** repo → Settings → Pages → Source: `main` branch, folder: `/ (root)` (or `/docs` if you put the build there). Save.
8. Your URL will be `https://<your-username>.github.io/mod4-envdesignlab-birp-ctm153/<build-folder>/index.html`. Open it in a browser to confirm it loads, then submit that link.

> Tip: GitHub Pages is case-sensitive in the URL. Folders with spaces (`mod4 build/`) become `mod4%20build/` — easier to rename to `mod4-build` before pushing.

---

## Part 5 — Two scripts you'll need

Drop these into `Assets/Scripts/`. They're short and do everything the assignment asks for.

### `Collectible.cs`
```csharp
using UnityEngine;

public class Collectible : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.Instance.CollectItem();
            Destroy(gameObject);
        }
    }
}
```

### `LevelManager.cs`
```csharp
using UnityEngine;
using UnityEngine.UI;
using TMPro; // remove this line and switch to Text if you don't use TMP

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int totalItems = 10;
    public float timeLimit = 300f; // 5 minutes
    public GameObject endCanvas;
    public TMP_Text statsText;

    int collected = 0;
    float startTime;
    bool ended = false;

    void Awake()  { Instance = this; }
    void Start()  { startTime = Time.time; endCanvas.SetActive(false); }

    void Update()
    {
        if (ended) return;
        if (Time.time - startTime >= timeLimit) EndLevel("Time's up!");
    }

    public void CollectItem()
    {
        collected++;
        if (collected >= totalItems) EndLevel("All items collected!");
    }

    public void TriggerEndZone() { EndLevel("Reached the end!"); }

    void EndLevel(string reason)
    {
        ended = true;
        float elapsed = Time.time - startTime;
        statsText.text = $"{reason}\nItems: {collected}/{totalItems}\nTime: {elapsed:F1}s";
        endCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f;
    }
}
```

Add a third tiny script `EndZone.cs` if you want a trigger volume to end the level:
```csharp
using UnityEngine;
public class EndZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) LevelManager.Instance.TriggerEndZone();
    }
}
```

Tag your First Person Controller as `Player` for any of this to fire.

---

## Part 6 — Suggested time budget

| Phase | Hours |
|---|---|
| Terrain sculpt + texture | 1.5 |
| Trees, details, fog | 0.5 |
| Player + collectibles + scripts | 1 |
| Interior building | 1.5 |
| Lighting pass | 1 |
| Audio zones | 0.5 |
| Animation + particles | 1 |
| End screen + UI | 0.5 |
| WebGL build + push | 1 |
| Polish | 0.5 |
| **Total** | **~9 hours** |

---

## Quick sanity check before submitting

Open the final WebGL build in a browser and confirm:
1. It loads without errors in the JS console.
2. You can move, look, and collect.
3. The end canvas appears when you finish.
4. The GitHub Pages link works in an incognito window (rules out cache).
