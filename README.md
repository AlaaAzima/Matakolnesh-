# Matakolnesh - Complete Game Architecture & UML Documentation

This repository contains the source code for the **Matakolnesh!!** 2D physics archery game project in Unity. The project is designed following **SOLID principles**, modular architecture, and industry-standard **Software Design Patterns** to ensure clean separation of concerns, scalability, and 60 FPS performance.

---

## 1. High-Level Subsystem Overview

```mermaid
graph TD
    subgraph Core ["Core Architecture & State"]
        GM[GameManagerJE]
        GE[GameEvents]
        GS[IGameState]
        SS[SaveSystemJE]
    end

    subgraph Combat ["Player & Combat System"]
        Bow[Bow Controller]
        Arrow[Arrow Physics & Pool]
        Aim[Archer Aim & Trajectory]
    end

    subgraph Enemies ["Enemy Lifecycle"]
        EH[EnemyHealth & IDeath]
        EC[EnemyController]
        EE[EnemyEffects]
    end

    subgraph LevelEval ["Level Conditions & Progression"]
        LC[LevelCondition Hierarchy]
        WL[WinAndLoseJE]
        LU[LevelUnlockJE]
    end

    subgraph Interactive ["Interactive Mechanics"]
        BTN[ButtonJZ & IInteractable]
        Walls[Appear/Disappear Walls]
        TNT[TNT Explosion Hazard]
    end

    subgraph AudioVFX ["Audio & VFX Engine"]
        SM[SoundManager]
        VFX[VFXManager]
        Cam[Camera Shake & Fade]
    end

    subgraph UI ["User Interface"]
        HUD[UIHandlerJE]
        WinUI[WLGamePanel & WinPanelAnim]
        Menu[MainMenu & StageMenu]
    end

    GM --> GS
    GM --> WL
    GM --> LU
    GM --> SS
    Bow --> Arrow
    Bow --> Aim
    EH --> GE
    EC --> GE
    WL --> LC
    BTN --> Walls
    GE --> VFX
    GE --> SM
    HUD --> GM
    WinUI --> GM
```

---

## 2. Core Architecture & Game State Machine

Handles the core game loop, state management (`PlayingState`, `PausedState`, `GameOverState`), static event dispatching (`GameEvents`), scene navigation (`Loader`), and data persistence (`SaveSystemJE`).

```mermaid
classDiagram
    namespace CoreSystem {
        class GameManagerJE {
            +int remainingEnemies
            +int currentLevel
            -WinAndLoseJE winCondition
            -StarRatingJE starRatingSystem
            -LevelUnlockJE levelUnlockSystem
            -SaveSystemJE saveSystem
            +GameData gameData
            -IGameState currentState
            +ArrowShot() void
            +RegisterArrow() void
            +UnregisterArrow() void
            +SetState(IGameState state) void
            +LoadGameData() void
        }

        class IGameState {
            <<interface>>
            +bool CanShoot
            +bool CanPause
            +EnterState() void
            +ExitState() void
        }

        class PlayingState {
            +bool CanShoot
            +bool CanPause
            +EnterState() void
            +ExitState() void
        }

        class PausedState {
            +bool CanShoot
            +bool CanPause
            +EnterState() void
            +ExitState() void
        }

        class GameOverState {
            +bool CanShoot
            +bool CanPause
            +EnterState() void
            +ExitState() void
        }

        class GameEvents {
            <<static>>
            +Action OnEnemyKilled$
            +Action OnArrowSpawned$
            +Action OnArrowDestroyed$
            +Action OnPlayerDied$
            +Action~SoundType~ OnPlaySound$
            +Action~VFXType, Vector3~ OnPlayVFX$
            +TriggerEnemyKilled()$ void
            +TriggerArrowSpawned()$ void
            +TriggerArrowDestroyed()$ void
            +TriggerPlayerDied()$ void
            +TriggerPlaySound(SoundType type)$ void
            +TriggerPlayVFX(VFXType type, Vector3 pos)$ void
            +Clear()$ void
        }

        class SaveSystemJE {
            -string savePath
            +Save(GameData data) void
            +Load() GameData
        }

        class GameData {
            +int saveVersion
            +int highestUnlockedLevel
            +int[] levelStars
        }

        class LevelGateConfigSO {
            <<ScriptableObject>>
            +int gateInterval
            +int minStarsPerLevel
            +int requiredLevelsMeetingMin
        }

        class LevelUnlockJE {
            -LevelGateConfigSO config
            +UnlockNextLevel(int currentLevel) void
            +CanUnlockGatedLevel(int currentLevel) bool
        }

        class Loader {
            <<static>>
            +Load(Scene targetScene)$ void
            +LoaderCallBack()$ void
        }

        class LoaderCallBack {
            -bool isFirstupdate
        }

        class ResetSaveJE {
            -GameObject confirmationPanel
            +OnResetButtonPressed() void
            +ConfirmReset() void
        }
    }

    IGameState <|.. PlayingState
    IGameState <|.. PausedState
    IGameState <|.. GameOverState
    GameManagerJE o-- IGameState
    GameManagerJE --> SaveSystemJE
    GameManagerJE --> LevelUnlockJE
    SaveSystemJE ..> GameData
    LevelUnlockJE --> LevelGateConfigSO
    LoaderCallBack ..> Loader
```

---

## 3. Player, Bow & Projectile Physics System

Controls player rotation/aiming, bow tension charging, trajectory parabolic prediction dots, 2D physics movement, and Object Pooling for arrows.

```mermaid
classDiagram
    namespace PlayerAndCombat {
        class Bow {
            -GameObject arrowPrefab
            -ShotPoint shotPoint
            -ArrowPool arrowPool
            -TrajectoryPredictor trajectory
            -BowInput bowInput
            -StarRatingJE starRatingSystem
            -float maxForce
            -float maxDragDistance
            -GameObject currentArrow
            +Shoot() void
            -ChargeBow() void
            -ResetBow() void
        }

        class BowInput {
            -Vector2 dragStartPos
            -Vector2 currentDragPos
            -bool isDragging
            +GetDragVector() Vector2
            +IsDragging() bool
        }

        class ArcherAim {
            -Transform bowTransform
            -PlayerVisuals playerVisuals
            -Camera mainCamera
            -AimAtTarget(Vector3 target) void
        }

        class ShotPoint {
            -Transform playerTransform
            -Vector3 rightOffset
            -Vector3 leftOffset
            -Camera mainCamera
            -bool isMouseOnLeft
            +UpdatePosition() void
        }

        class TrajectoryPredictor {
            -GameObject pointPrefab
            -int numberOfPoints
            -float spaceBetweenPoints
            -GameObject[] points
            +ShowTrajectory(Transform shotPoint, float arrowSpeed) void
            +HideTrajectory() void
            -PointPosition(Transform shotPoint, float arrowSpeed, float t) Vector2
        }

        class ArrowPool {
            -Arrow arrowPrefab
            -int poolSize
            -Queue~Arrow~ pool
            +GetArrow() Arrow
            +ReturnArrow(Arrow arrow) void
        }

        class Arrow {
            -float speed
            -Rigidbody2D rb
            -bool isStuck
            +Launch(Vector2 direction, float force) void
            -OnCollisionEnter2D(Collision2D collision) void
        }

        class ArrowFadeInIntro {
            -float fadeDuration
            -Vector3 targetScale
        }

        class PlayerVisuals {
            -GameObject playerRight
            -GameObject playerLeft
            -Camera mainCamera
            -bool isFacingLeft
            +HandleFacingDirection() void
            +ApplyVisualState() void
        }

        class PlayerDeathJZ {
            -Animator animator
            -bool isDead
            +Die() void
        }

        class WallGrab2D {
            -LayerMask wallLayer
            -float highGravity
            -Transform checkPoint
            -float checkRadius
            -Rigidbody2D rb
            +StickToWall() void
            +IsTouchingWall() bool
        }
    }

    Bow --> BowInput
    Bow --> ShotPoint
    Bow --> ArrowPool
    Bow --> TrajectoryPredictor
    ArcherAim --> PlayerVisuals
    ArrowPool o-- Arrow
    PlayerDeathJZ ..|> IDeath
```

---

## 4. Enemy System & Damage Lifecycle

Defines damage contracts (`IDeath`), hit detection, death sequencing, and particle/sound event triggers on death.

```mermaid
classDiagram
    namespace EnemySystem {
        class IDeath {
            <<interface>>
            +Die() void
        }

        class EnemyHealth {
            -bool isDead
            +event Action OnDeath
            +Die() void
            -OnTriggerEnter2D(Collider2D collision) void
        }

        class EnemyController {
            -float destroyDelay
            -Collider2D enemyCollider
            -Rigidbody2D rb
            -EnemyHealth health
            +HandleDeath() void
        }

        class EnemyEffects {
            -Animator animator
            -EnemyHealth health
            +PlayDeathEffects() void
        }

        class EnemyLogicJZ {
            -float destroyDelay
            -Animator animator
            -Collider2D enemyCollider
            -Rigidbody2D rb
            -bool isDead
            +Die() void
            -OnTriggerEnter2D(Collider2D collision) void
        }
    }

    IDeath <|.. EnemyHealth
    IDeath <|.. EnemyLogicJZ
    EnemyController --> EnemyHealth
    EnemyEffects --> EnemyHealth
```

---

## 5. Interactive Environment & Hazard Mechanics

Manages pressure buttons (`IInteractable`), wall toggle triggers, explosive TNT hazards, rolling balls, rotating bar obstacles, and platform translation scripts.

```mermaid
classDiagram
    namespace Environment {
        class IInteractable {
            <<interface>>
            +Interact() void
        }

        class ButtonJZ {
            -Animator animator
            -string clickTriggerName
            +event Action OnButtonPressed
            +Interact() void
        }

        class AppearWall {
            -ButtonJZ button
            +Appear() void
        }

        class DisappearWall {
            -ButtonJZ button
            +Disappear() void
        }

        class PushedBall {
            -bool isStuck
            -OnCollisionEnter2D(Collision2D collision) void
        }

        class TNTJZ {
            -GameObject[] targetEnemy
            -float explosionRadius
            -float explosionForce
            -LayerMask affectedLayers
            -GameObject explosionEffect
            -bool hasExploded
            +Explode() void
        }

        class TNTLogic {
            -GameObject[] targetEnemy
            -GameObject[] targetWalls
            -string arrowTag
            -GameObject explosionEffectPrefab
            -float destroyDelay
            -bool isExploded
            +Explode() void
        }

        class BoundJZ {
            -OnTriggerEnter2D(Collider2D collision) void
        }

        class RotatingBar {
            -float speed
        }

        class Translate {
            -Axis moveAxis
            -float speed
            -float minLimit
            -float maxLimit
            -float dir
        }
    }

    IInteractable <|.. ButtonJZ
    AppearWall --> ButtonJZ
    DisappearWall --> ButtonJZ
```

---

## 6. Level Evaluation & Conditions Framework

Implements condition checking strategy for win/loss criteria (`WinAndLoseJE`), abstract level condition classes, and 3-star rating evaluation (`StarRatingJE`).

```mermaid
classDiagram
    namespace LevelConditions {
        class LevelCondition {
            <<abstract>>
            +bool isSatisfied
            +CheckCondition()* bool
        }

        class EnemiesDeadCondition {
            +CheckCondition() bool
        }

        class NoActiveArrowsCondition {
            +CheckCondition() bool
        }

        class KillAllEnemiesCondition {
            +CheckCondition() bool
        }

        class WinAndLoseJE {
            -LevelCondition[] winConditions
            +CheckWin() bool
            +CheckLose() bool
        }

        class StarRatingJE {
            -UIHandlerJE uiHandler
            +int arrowsUsed
            +ArrowShot() void
            +CalculateStars() int
            +ResetCounter() void
        }
    }

    LevelCondition <|-- EnemiesDeadCondition
    LevelCondition <|-- NoActiveArrowsCondition
    LevelCondition <|-- KillAllEnemiesCondition
    WinAndLoseJE o-- LevelCondition
```

---

## 7. Audio & Visual Effects Engine

Listens to event channels (`GameEvents`) to play sound effects, manage particle pools, and trigger camera shake/fade effects.

```mermaid
classDiagram
    namespace AudioAndVFX {
        class SoundType {
            <<enum>>
            ArrowRelease
            ArrowHit
            EnemyDeath
            ButtonClick
            WinSound
            LoseSound
        }

        class SoundManager {
            -AudioClip[] soundList
            -AudioClip[] musicList
            -AudioSource sfxSource
            -AudioSource musicSource
            +HandlePlaySound(SoundType type) void
            +PlayCurrentTrack() void
            +PlayNextTrack() void
        }

        class VFXType {
            <<enum>>
            Explosion
            ArrowHitSpark
            EnemyBlood
        }

        class VFXMapping {
            <<struct>>
            +VFXType type
            +ParticleSystem prefab
        }

        class VFXManager {
            -VFXMapping[] vfxMappings
            -Dictionary~VFXType, Queue~ParticleSystem~~ vfxPool
            +HandlePlayVFX(VFXType type, Vector3 position) void
        }

        class MenuMusicController {
            -AudioSource musicSource
        }

        class ButtonSound {
            +PlayButtonSound() void
        }

        class CameraShakeJE {
            -float shakeDuration
            -float shakeMagnitude
            -float dampingSpeed
            -Vector3 originalPosition
            +Shake() void
            +Shake(float duration, float magnitude) void
        }

        class CameraFadeJZ {
            +float speedScale
            +Color fadeColor
            +AnimationCurve curve
            +TriggerDeathSequence() void
            +OnGUI() void
        }
    }

    VFXManager ..> VFXMapping
    VFXMapping ..> VFXType
    SoundManager ..> SoundType
```

---

## 8. User Interface & Stage Navigation

Controls in-game HUD displays, Win/Lose popups (`WLGamePanelJZ`), star animations (`WinPanelAnimJE`), main menus, and swipeable level selection pages (`StageMenuUIJE`, `SwipeConrolllerJE`).

```mermaid
classDiagram
    namespace UserInterface {
        class UIHandlerJE {
            -TextMeshProUGUI arrowCounter
            +ArrowCounterUpdateJE(int arrowCount) void
        }

        class WLGamePanelJZ {
            -WinPanelAnimJE winPanelAnimation
            -StarUIJZ starUI
            -float winDelay
            -int[] starsRequiredPerTier
            +ShowWin(int earnedStars) void
            +Restart() void
            +StageMenu() void
            +MainMenu() void
            +NextLevelJE() void
        }

        class WinPanelAnimJE {
            -RectTransform[] stars
            -float emptyStarScale
            -float earnedStarPopScale
            -RectTransform winPanel
            +PlayAnimation(int earnedStars) void
        }

        class StarUIJZ {
            -Image[] stars
            -Sprite fullStar
            -Sprite emptyStar
            +DisplayStars(int earnedStars) void
        }

        class PauseSystemJZ {
            -GameObject pauseMenuUI
            +PauseGame() void
            +ResumeGame() void
        }

        class StartInstructionUI {
            -GameObject instructionPanel
            -float time
            -HideInstructions() IEnumerator
        }

        class MainMenuUI {
            +PlayGame() void
            +QuitGame() void
        }

        class MenuUIController {
            +OpenSettings() void
            +CloseSettings() void
        }

        class StageMenuUIJE {
            -GameObject levelButtons
            -LevelsUIJE levelUI
            -Button[] buttons
            +RefreshUI() void
            +OpenLevel(int levelId) void
        }

        class LevelsUIJE {
            +UpdateButtons(Button[] buttons, int unlockedLevel, int[] levelStars) void
        }

        class SwipeConrolllerJE {
            -LTDescr tween
            +NextPage() void
            +PreviousPage() void
            +SetPage(int page) void
            +OnEndDrag(PointerEventData eventData) void
        }

        class ButtonAnimationJE {
            -Vector3 originalScale
        }

        class ButtonAnimaJE {
            -Vector3 originalScale
            -Button button
            +OnPointerEnter(PointerEventData eventData) void
            +OnPointerExit(PointerEventData eventData) void
            +OnPointerDown(PointerEventData eventData) void
            +OnPointerUp(PointerEventData eventData) void
        }

        class GameTestJE {
            -WinAndLoseJE winAndLose
            -StarRatingJE starRating
            -LevelUnlockJE levelUnlock
            -SaveSystemJE saveSystem
            -GameManagerJE gameManager
            +TestWin() void
            +TestLose() void
            +GiveMaxStars() void
        }
    }

    WLGamePanelJZ --> WinPanelAnimJE
    WLGamePanelJZ --> StarUIJZ
    StageMenuUIJE --> LevelsUIJE
    GameTestJE --> GameManagerJE
```

---

## 9. Applied Software Design Patterns

1. **State Pattern:** Encapsulates core game states (`PlayingState`, `PausedState`, `GameOverState`) behind the `IGameState` interface. This eliminates monolithic switch/case blocks and ensures safe state transitions.
2. **Strategy Pattern:** Implemented for win condition checking. `WinAndLoseJE` checks an array of injected `LevelCondition` strategies (e.g. `EnemiesDeadCondition`, `NoActiveArrowsCondition`), strictly respecting the Open/Closed Principle (OCP).
3. **Observer Pattern (Event Bus):** Handled via `GameEvents.cs`. Entities dispatch C# `Action` delegates (`OnEnemyKilled`, `OnPlayVFX`, `OnPlaySound`) to keep systems decoupled.
4. **Object Pool Pattern:** Managed by `ArrowPool` and `VFXManager` to recycle arrow instances and particle effects, reducing garbage collection overhead.
5. **Singleton Pattern:** Used for global coordinators (`GameManagerJE`, `SoundManager`), decoupled from gameplay objects via the Event Bus.
6. **Command / Interactable Pattern:** Managed through `IInteractable` allowing buttons and switches (`ButtonJZ`) to trigger environment components (`AppearWall`, `DisappearWall`, `TNTJZ`).

---

## 10. Complete Script Directory

- **Core & State Machine:**
  - [`Assets/Scripts/GameManagerJE.cs`](./Assets/Scripts/GameManagerJE.cs)
  - [`Assets/Scripts/Core/GameEvents.cs`](./Assets/Scripts/Core/GameEvents.cs)
  - [`Assets/Scripts/States/IGameState.cs`](./Assets/Scripts/States/IGameState.cs)
  - [`Assets/Scripts/States/PlayingState.cs`](./Assets/Scripts/States/PlayingState.cs)
  - [`Assets/Scripts/States/PausedState.cs`](./Assets/Scripts/States/PausedState.cs)
  - [`Assets/Scripts/States/GameOverState.cs`](./Assets/Scripts/States/GameOverState.cs)
  - [`Assets/Scripts/SaveSystemJE.cs`](./Assets/Scripts/SaveSystemJE.cs)
  - [`Assets/Scripts/GameDataJE.cs`](./Assets/Scripts/GameDataJE.cs)
  - [`Assets/Scripts/ResetSaveJE.cs`](./Assets/Scripts/ResetSaveJE.cs)
  - [`Assets/Scripts/Loader.cs`](./Assets/Scripts/Loader.cs)
  - [`Assets/Scripts/LoaderCallBack.cs`](./Assets/Scripts/LoaderCallBack.cs)
  - [`Assets/Scripts/LevelGateConfigSO.cs`](./Assets/Scripts/LevelGateConfigSO.cs)
  - [`Assets/Scripts/LevelUnlockJE.cs`](./Assets/Scripts/LevelUnlockJE.cs)
  - [`Assets/Scripts/UnlockFiveLevels.cs`](./Assets/Scripts/UnlockFiveLevels.cs)

- **Combat & Player Mechanics:**
  - [`Assets/Scripts/Bow.cs`](./Assets/Scripts/Bow.cs)
  - [`Assets/Scripts/BowInput.cs`](./Assets/Scripts/BowInput.cs)
  - [`Assets/Scripts/NewPlayer/ArcherAim.cs`](./Assets/Scripts/NewPlayer/ArcherAim.cs)
  - [`Assets/Scripts/ShotPoint.cs`](./Assets/Scripts/ShotPoint.cs)
  - [`Assets/Scripts/Trajectory.cs`](./Assets/Scripts/Trajectory.cs)
  - [`Assets/Scripts/Arrow.cs`](./Assets/Scripts/Arrow.cs)
  - [`Assets/Scripts/ArrowPool.cs`](./Assets/Scripts/ArrowPool.cs)
  - [`Assets/Scripts/ArrowTween.cs`](./Assets/Scripts/ArrowTween.cs)
  - [`Assets/Scripts/PlayerVisuals.cs`](./Assets/Scripts/PlayerVisuals.cs)
  - [`Assets/Scripts/PlayerDeathJZ.cs`](./Assets/Scripts/PlayerDeathJZ.cs)
  - [`Assets/Scripts/WallGrab2D.cs`](./Assets/Scripts/WallGrab2D.cs)

- **Enemy Lifecycle:**
  - [`Assets/Scripts/IDeathJZ.cs`](./Assets/Scripts/IDeathJZ.cs)
  - [`Assets/Scripts/EnemyHealth.cs`](./Assets/Scripts/EnemyHealth.cs)
  - [`Assets/Scripts/EnemyController.cs`](./Assets/Scripts/EnemyController.cs)
  - [`Assets/Scripts/EnemyEffects.cs`](./Assets/Scripts/EnemyEffects.cs)
  - [`Assets/Scripts/EnemyLogicJZ.cs`](./Assets/Scripts/EnemyLogicJZ.cs)

- **Interactive Environment:**
  - [`Assets/Scripts/ButtonWire/IInteractable.cs`](./Assets/Scripts/ButtonWire/IInteractable.cs)
  - [`Assets/Scripts/ButtonWire/ButtonJZ.cs`](./Assets/Scripts/ButtonWire/ButtonJZ.cs)
  - [`Assets/Scripts/ButtonWire/AppearWall.cs`](./Assets/Scripts/ButtonWire/AppearWall.cs)
  - [`Assets/Scripts/ButtonWire/DisappearWall.cs`](./Assets/Scripts/ButtonWire/DisappearWall.cs)
  - [`Assets/Scripts/ButtonWire/PushedBall.cs`](./Assets/Scripts/ButtonWire/PushedBall.cs)
  - [`Assets/Scripts/ButtonWire/TNTJZ.cs`](./Assets/Scripts/ButtonWire/TNTJZ.cs)
  - [`Assets/Scripts/TNT.cs`](./Assets/Scripts/TNT.cs)
  - [`Assets/Scripts/BoundJZ.cs`](./Assets/Scripts/BoundJZ.cs)
  - [`Assets/Scripts/RotatingBar/RotatingBar.cs`](./Assets/Scripts/RotatingBar/RotatingBar.cs)
  - [`Assets/Scripts/Translate/Translate.cs`](./Assets/Scripts/Translate/Translate.cs)

- **Level Conditions & Rules:**
  - [`Assets/Scripts/Conditions/LevelCondition.cs`](./Assets/Scripts/Conditions/LevelCondition.cs)
  - [`Assets/Scripts/Conditions/EnemiesDeadCondition.cs`](./Assets/Scripts/Conditions/EnemiesDeadCondition.cs)
  - [`Assets/Scripts/Conditions/NoActiveArrowsCondition.cs`](./Assets/Scripts/Conditions/NoActiveArrowsCondition.cs)
  - [`Assets/Scripts/Conditions/KillAllEnemiesCondition.cs`](./Assets/Scripts/Conditions/KillAllEnemiesCondition.cs)
  - [`Assets/Scripts/WinAndLoseJE.cs`](./Assets/Scripts/WinAndLoseJE.cs)
  - [`Assets/Scripts/StarRatingJE.cs`](./Assets/Scripts/StarRatingJE.cs)

- **Audio & Visual Effects:**
  - [`Assets/Scripts/Core/VFXManager.cs`](./Assets/Scripts/Core/VFXManager.cs)
  - [`Assets/Scripts/SoundManager.cs`](./Assets/Scripts/SoundManager.cs)
  - [`Assets/Scripts/MenuMusicController.cs`](./Assets/Scripts/MenuMusicController.cs)
  - [`Assets/Scripts/ButtonSound.cs`](./Assets/Scripts/ButtonSound.cs)
  - [`Assets/Scripts/CameraShakeJE.cs`](./Assets/Scripts/CameraShakeJE.cs)
  - [`Assets/Scripts/CameraFadeJZ.cs`](./Assets/Scripts/CameraFadeJZ.cs)

- **User Interface & Stage Navigation:**
  - [`Assets/Scripts/UIHandlerJE.cs`](./Assets/Scripts/UIHandlerJE.cs)
  - [`Assets/Scripts/WLGamePanelJZ.cs`](./Assets/Scripts/WLGamePanelJZ.cs)
  - [`Assets/Scripts/WinPanelAnimJE.cs`](./Assets/Scripts/WinPanelAnimJE.cs)
  - [`Assets/Scripts/StarUIJZ.cs`](./Assets/Scripts/StarUIJZ.cs)
  - [`Assets/Scripts/PauseSystemJZ.cs`](./Assets/Scripts/PauseSystemJZ.cs)
  - [`Assets/Scripts/StartInstructionUI.cs`](./Assets/Scripts/StartInstructionUI.cs)
  - [`Assets/Scripts/MainMenuUi.cs`](./Assets/Scripts/MainMenuUi.cs)
  - [`Assets/Scripts/MainmenuController.cs`](./Assets/Scripts/MainmenuController.cs)
  - [`Assets/Scripts/StageMenuScripts/StageMenuUIJE.cs`](./Assets/Scripts/StageMenuScripts/StageMenuUIJE.cs)
  - [`Assets/Scripts/StageMenuScripts/LevelsUIJE.cs`](./Assets/Scripts/StageMenuScripts/LevelsUIJE.cs)
  - [`Assets/Scripts/StageMenuScripts/SwipeConrolllerJE.cs`](./Assets/Scripts/StageMenuScripts/SwipeConrolllerJE.cs)
  - [`Assets/Scripts/StageMenuScripts/ButtonAnimationJE.cs`](./Assets/Scripts/StageMenuScripts/ButtonAnimationJE.cs)
  - [`Assets/Scripts/ButtonAnimJE.cs`](./Assets/Scripts/ButtonAnimJE.cs)
  - [`Assets/Scripts/GameTestJE.cs`](./Assets/Scripts/GameTestJE.cs)
