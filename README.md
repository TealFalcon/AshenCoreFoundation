````markdown
# ⚒️ AshenCore Foundation

> **A modular architecture framework for Unity.**

AshenCore Foundation provides the core architecture required to build scalable, maintainable and modular Unity projects.

Designed around **Dependency Injection**, **Service Architecture**, and **Modular Systems**, it allows you to grow your project without turning it into a monolithic codebase.

---

# ✨ Included Systems

Foundation ships with the following core systems:

| System | Description |
|---------|-------------|
| 🐞 **Debug System** | In-game console, logging and debugging tools. |
| 📡 **Events System** | Global event communication between systems. |
| 🎬 **Scene Manager** | Scene loading and management utilities. |

Additional modules are available separately.

---

# 📦 Installation

## 1. Clone the repository

Repository:


If your project already uses Git, add AshenCore Foundation as a **Git Submodule**.

Otherwise, simply clone the repository inside your Unity project.



📥 Clone Repository

git clone https://github.com/TealFalcon/AshenCoreFoundation.git


📦 Add as Git Submodule (Recommended)

➤ Add submodule

git submodule add https://github.com/TealFalcon/AshenCoreFoundation.git Assets/AshenCoreFoundation

➤ Initialize / update submodule

git submodule update --init --recursive

> This guide assumes basic Git knowledge.

---

## 2. Install the required packages

Open:

```
Packages/manifest.json
```

and add the following dependencies:

```json
"jp.hadashikick.vcontainer": "https://github.com/hadashiA/VContainer.git?path=VContainer/Assets/VContainer#1.18.0",
"com.cysharp.unitask": "https://github.com/Cysharp/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask",
"com.unity.nuget.newtonsoft-json": "3.2.2",
"com.unity.addressables": "2.9.1"
```

Unity will automatically install them the next time the project is opened.

---

## 3. Import the Scenes

Once Unity has finished importing the project, import the scenes corresponding to the distribution you downloaded.

Available distributions:

* 🟢 Foundation
* ⭐ Forged *(GitHub Sponsors only)*
* ⭐ AshenCore Original *(GitHub Sponsors only)*

---

## 4. First Launch

The first time you open the project Unity may ask you to import **TextMeshPro Essentials**.

Simply click:

```
Import
```

This is a standard Unity requirement.

---

# 📚 Documentation & Tutorials

Learn how each system works by watching the official tutorial series:

▶ **YouTube Playlist**

https://www.youtube.com/playlist?list=PLrWo5JPa3uNEkQO38odKenF2XYqUQMirb

---

# 💬 Community

Need help?

Join the official Discord server.

💬 **Discord**

https://discord.gg/wRqJkNfQ4w

---

# 🏗️ Architecture Philosophy

AshenCore follows a modular architecture based on:

* Dependency Injection (VContainer)
* Interface-driven services
* Modular systems
* Low coupling
* High scalability
* Runtime service registration

The goal is to let developers focus on **building games** instead of repeatedly creating infrastructure.

---

# ❤️ Support the Project

If you enjoy AshenCore and want to support its development, consider becoming a **GitHub Sponsor** to gain access to exclusive modules and complete project distributions.

---

⚠️ Common Issues / Errors Found

🧩 Test scenes not working

If you are using the Test scenes, you must mark them as Addressables before running the project.

If they are not marked correctly, the Scene Manager will not be able to load them properly at runtime.

✔ Fix
Select the Test scene in Unity.
In the Inspector, enable Addressable.
Ensure the scene is included in the Addressables Groups.
Rebuild Addressables (if required).

Without this step, scene loading through AshenCore SceneManager may fail or return null references



```text
AshenCore Framework License (ACFL) v1.0

Copyright (c) 2026 TealFalcon

Permission is hereby granted to use AshenCore in personal and commercial projects, subject to the conditions below.

You are allowed to:
- Use AshenCore Foundation in commercial and non-commercial projects.
- Modify the source code for internal use within your projects.
- Distribute applications, games, or products that include AshenCore as part of a compiled build.

You are NOT allowed to:
- Redistribute AshenCore Foundation as a standalone framework, package, or library.
- Repackage, resell, or re-upload AshenCore Foundation in any form outside of a compiled application.
- Publish AshenCore Foundation on other repositories, package managers, or asset stores.

Attribution:
While not required, attribution is appreciated. If you choose to credit, please use:

"AshenCore"
https://github.com/TealFalcon/AshenCoreFoundation

Disclaimer:
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED.
IN NO EVENT SHALL THE AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY ARISING FROM ITS USE.
```
