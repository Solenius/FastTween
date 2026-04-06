# FastTween

A lightweight tweening library for **Unity** focused on **simplicity** and **performance**.
FastTween gives you a small fluent API for animating common values

## ✨ Features

- Simple fluent API
- Built-in tweens for:
  - `float`
  - `Transform.position`
  - `Transform.rotation`
  - `Transform.localScale`
- 30+ easing functions
- Looping and ping-pong support
- `Update`, `LateUpdate`, and `FixedUpdate` execution modes
- Optional unscaled time (`realTime: true`)
- Coroutine-friendly and observable wrappers

---

## 📦 Installation

### Option 1: Install via UPM Git URL

Add this to your `manifest.json`:

```json
{
  "dependencies": {
    "com.solenius.fasttween": "https://github.com/Solenius/FastTween.git?path=/Assets/Scripts/FastTween"
  }
}
```

### Option 2: Copy into your project

Copy the `Assets/Scripts/FastTween` folder into your Unity project.

---

## 🚀 Quick Start

```csharp
using FastTween;
using UnityEngine;

public class TweenExample : MonoBehaviour
{
    [SerializeField] private Transform target;

    private void Start()
    {
        Tween.CreatePositionTween(
                target: target,
                from: target.position,
                to: target.position + new Vector3(0f, 2f, 0f),
                duration: 0.5f)
            .SetEasing(EasingType.EaseOutCubic)
            .SetOnComplete(() => Debug.Log("Move complete"))
            .CreateAndRun();
    }
}
```

> The first tween automatically creates the internal controller object, so no additional setup is required.

---

## 🧩 Example Usages

### 1. Move a transform

```csharp
Tween.CreatePositionTween(
        target: transform,
        from: transform.position,
        to: new Vector3(5f, 1f, 0f),
        duration: 0.4f)
    .SetEasing(EasingType.EaseInOutSine)
    .CreateAndRun();
```

### 2. Scale punch with ping-pong

```csharp
Tween.CreateScalingTween(
        target: transform,
        from: Vector3.one,
        to: Vector3.one * 1.2f,
        duration: 0.12f)
    .SetLoop(LoopType.PingPong)
    .SetEasing(EasingType.EaseOutBack)
    .CreateAndRun();
```

### 3. Rotate an object

```csharp
Tween.CreateRotationTween(
        target: transform,
        from: transform.rotation,
        to: Quaternion.Euler(0f, 180f, 0f),
        duration: 0.35f)
    .SetEasing(EasingType.EaseOutQuad)
    .CreateAndRun();
```

### 4. Fade UI with a value tween

```csharp
[SerializeField] private CanvasGroup canvasGroup;

private void FadeIn()
{
    Tween.CreateValueTween(from: 0f, to: 1f, duration: 0.25f)
        .SetEasing(EasingType.EaseOutQuad)
        .SetOnUpdate(value => canvasGroup.alpha = value)
        .CreateAndRun();
}
```

### 5. Run using unscaled time

```csharp
Tween.CreateValueTween(from: 0f, to: 1f, duration: 0.2f)
    .SetOnUpdate(value => canvasGroup.alpha = value)
    .CreateAndRun(update: UpdateType.Update, realTime: true);
```

### 6. Use inside a coroutine

```csharp
private IEnumerator MoveRoutine()
{
    yield return Tween.CreatePositionTween(
            target: transform,
            from: transform.position,
            to: transform.position + Vector3.up * 3f,
            duration: 0.5f)
        .SetEasing(EasingType.EaseInOutQuad)
        .ToYieldInstruction();

    Debug.Log("Tween finished");
}
```

### 7. Start manually and cancel later

```csharp
using FastTween;

private ITween activeTween;

private void StartLoop()
{
    activeTween = Tween.CreateScalingTween(
            target: transform,
            from: Vector3.one,
            to: Vector3.one * 1.1f,
            duration: 0.2f)
        .SetLoop(LoopType.PingPongLoop)
        .Create();

    Tween.Start(tween: activeTween);
}

private void StopLoop()
{
    if (activeTween != null)
        Tween.Cancel(tween: activeTween);
}
```

---

## 🔧 API Overview

### Tween factory methods

| Method | Description |
|---|---|
| `Tween.CreateValueTween(float from, float to, float duration)` | Tween a float value via `SetOnUpdate(...)` |
| `Tween.CreatePositionTween(Transform target, Vector3 from, Vector3 to, float duration)` | Tween world position |
| `Tween.CreateRotationTween(Transform target, Quaternion from, Quaternion to, float duration)` | Tween world rotation |
| `Tween.CreateScalingTween(Transform target, Vector3 from, Vector3 to, float duration)` | Tween local scale |

### Builder methods

| Method | Purpose |
|---|---|
| `SetEasing(EasingType easing)` | Set the easing curve |
| `SetLoop(LoopType loop)` | Configure looping behavior |
| `SetOnStart(Action)` | Called when the tween starts |
| `SetOnComplete(Action)` | Called when the tween completes |
| `SetOnCancel(Action)` | Called when the tween is cancelled |
| `SetOnUpdate(Action<float>)` | Value tween update callback |
| `Create()` | Build the tween without starting it |
| `CreateAndRun(...)` | Build and start immediately |
| `ToYieldInstruction(...)` | Await the tween in a coroutine |
| `ToObservable(...)` | Expose the tween as an `IObservable` |

### Enums

- `LoopType`: `None`, `Loop`, `PingPong`, `PingPongLoop`
- `UpdateType`: `Update`, `LateUpdate`, `FixedUpdate`
- `EasingType`: linear, sine, quad, cubic, quart, quint, expo, circ, back, elastic, and bounce variants

---

## 📄 License

MIT License. See [`LICENSE`](LICENSE).
