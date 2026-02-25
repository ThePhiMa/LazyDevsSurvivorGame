# CLAUDE.md — Lazy Dev's Survivor Game

## Project Purpose

This is a **Vampire Survivors-style game** built in Unity as a teaching aid for game performance optimisation lectures. The codebase is **intentionally "vibe coded"** — functional but unoptimised — so students can identify, profile, and fix real performance problems.

Every deliberate anti-pattern is an opportunity: naive loops, per-frame allocations, missing object pools, redundant `GetComponent` calls, and so on. The goal is a working prototype that is demonstrably slow at scale, not a production-ready game.

---

## Project Structure

```
Assets/
  Scripts/
    BIMM/
      Core/           # Bootstrap, singletons, game loop
      Gameplay/       # Player, enemies, weapons, XP
      UI/             # HUD, menus, level-up screen
      Data/           # ScriptableObjects, configs
      Utilities/      # Shared helpers, extensions
  Scenes/
  Prefabs/
  Textures/
  Settings/
```

All runtime scripts live under the `BIMM` root namespace with the matching subnamespace for their folder (e.g. `BIMM.Gameplay`, `BIMM.Core`).

---

## Code Style Guidelines

### Naming
| Target | Convention | Example |
|---|---|---|
| Classes, Methods, Properties | PascalCase | `EnemySpawner`, `TakeDamage()` |
| Private fields | _camelCase (underscore prefix) | `_currentHealth` |
| Interfaces | `I` prefix + PascalCase | `IDamageable` |
| Constants | SCREAMING_SNAKE_CASE | `MAX_ENEMY_COUNT` |

### Formatting
- **4-space indentation** — no tabs.
- **Braces on their own line** (Allman style).
```csharp
public void TakeDamage(float amount)
{
    _currentHealth -= amount;
}
```
- One blank line between members; two blank lines between top-level declarations.

### Types
- Use **explicit types** — avoid `var`.
- Enable **nullable annotations** (`?`) where a null is a valid state.
- Prefer `float` over `double` (Unity default).

### Architecture
- **Component-based design**: small, single-responsibility MonoBehaviours.
- **Composition over inheritance**: share behaviour through components or injected helpers, not deep class hierarchies.
- **Data**: use ScriptableObjects for tunable config (enemy stats, weapon data, level curves).
- **Interfaces**: define capabilities as interfaces (e.g. `IDamageable`, `IPickupable`).

### Example Skeleton
```csharp
using UnityEngine;

namespace BIMM.Gameplay
{
    public class EnemyHealth : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _maxHealth = 100f;

        private float _currentHealth;

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float amount)
        {
            _currentHealth -= amount;

            if (_currentHealth <= 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            Destroy(gameObject);
        }
    }
}
```

---

## Intentional Anti-Patterns (Teaching Targets)

The following patterns are **deliberately present** in the initial prototype. Do NOT fix them unless the task explicitly says to — they are lecture material.

| Anti-Pattern | Location hint | What to teach |
|---|---|---|
| `GetComponent<T>()` every frame | Enemy & player update loops | Cache in `Awake`/`Start` |
| `FindObjectsOfType` in `Update` | Weapon hit detection | Cache references; use spatial data structures |
| No object pooling | Enemy spawner, projectiles | `UnityEngine.Pool`, custom pools |
| `List<T>` allocations every frame | Collision/overlap checks | Pre-allocate; reuse buffers |
| Per-frame `new Vector3` / LINQ | Movement, targeting | Struct reuse, avoid LINQ at runtime |
| Naive O(n²) enemy-vs-enemy checks | Crowd simulation | Spatial hashing, Unity Physics broadphase |
| Unoptimised sprites / no atlases | All art assets | Sprite atlases, batching |
| No `[SerializeField]` discipline | Various | Proper encapsulation |

---

## Game Design (Prototype Scope)

- **Player**: moves freely with WASD / left stick; attacks automatically.
- **Enemies**: spawn at the map edge, walk toward the player, deal contact damage.
- **Weapons**: start with one projectile weapon; weapons level up on player level-up.
- **XP Gems**: enemies drop gems; player auto-collects nearby gems.
- **Level Up**: on level-up, player is offered three random upgrades (pause game, show UI).
- **Timer**: survive as long as possible; difficulty scales over time.

No win condition in the prototype — survival time is the only score.

---

## Unity Version & Packages

- **Unity**: 6.3
- **Input System**: new Input System package (InputSystem_Actions asset already present).
- **No additional packages** should be added without discussion.

---

## Working With This Project

- Never refactor the intentional anti-patterns unless explicitly asked.
- Keep new code consistent with the style guide above.
- Prefer small, readable commits scoped to a single feature or fix.
- When adding a new system, note in a code comment if it contains a deliberate anti-pattern and why.
