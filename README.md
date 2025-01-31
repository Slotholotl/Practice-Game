# 🍩 Unity Third-Person Controller with Donut Collectibles

This Unity project features a **third-person player controller** with movement, sprinting, jumping, and a **collectible system for donuts**. The player can pick up donuts by pressing `E`, and a UI counter keeps track of collected donuts.

---

## 📌 Features

✅ **Third-person movement** (WASD)  
✅ **Sprint (Left Shift)**  
✅ **Jump (Spacebar)**  
✅ **Camera follows the player smoothly**  
✅ **Prevents camera clipping using SphereCast**  
✅ **Collectible donuts with counter UI**  
✅ **Interact with `E` key to collect**  

---

## 🛠️ Setup Instructions

### **1️⃣ Player Setup**
1. **Create a Player Object**
   - Add a **`CharacterController`** component.
   - Attach the `PlayerMovement.cs` script.
   - Set the **tag to `"Player"`**.
   - Assign **CameraFollow** as the `cameraTransform` in `PlayerMovement.cs`.

---

### **2️⃣ Camera Setup**
1. **Attach `ThirdPersonCamera.cs`** to **CameraFollow**.
2. **Set up collision detection:**
   - Add a **`LayerMask`** in the **Inspector**.
   - Set it to `"Default"` or `"Everything"` **(Exclude the Player Layer).**

---

### **3️⃣ Donut Collectibles Setup**
1. **Create a Donut Object**
   - Create a **3D donut model** or use a placeholder (e.g., a sphere).
   - Add a **Collider** (`SphereCollider`) and enable `IsTrigger = true`.
   - Add a **Rigidbody** and set `Use Gravity = false`, `Is Kinematic = true`.
   - Assign the tag `"Donut"`.

2. **Attach `Donut.cs`** to the donut object.

3. **Ensure Donuts Are Collectible**  
   - The player can pick up donuts by pressing `E` when near them.  
   - The donut count updates on the UI.  
   - The donut disappears when collected.

---

### **4️⃣ UI Setup (Donut Counter)**
1. **Create a UI Text element**:
   - Right-click in the Hierarchy > **UI > Text** (or **TextMeshPro**).
   - Set its position and font size in the Canvas.
   - Name it **"DonutCountText"**.

2. **Assign the UI Text to the PlayerMovement script**:
   - Select the **Player object**.
   - Drag the **DonutCountText UI element** into the `Donut Count Text` field in the Inspector.

---

## 🎮 Controls

| Action  | Key |
|---------|----|
| Move    | WASD |
| Sprint  | Left Shift |
| Jump    | Spacebar |
| Rotate Camera | Mouse Movement |
| Collect Donut | E |

---

## 🔧 How Donut Collection Works
- Uses `Physics.OverlapSphere` to detect nearby donuts.
- Press `E` when close to a donut to collect it.
- The UI counter updates instantly.
- Donuts disappear after collection.

---

## 🚀 Future Improvements
- ✅ Add **random donut spawn locations**  
- ✅ Implement **sound effects & animations**  
- ✅ Create **donut respawning system**  

---

## 🛠️ **Made With**
- **Unity** (Version XX.X.X)  
- **C#**  

---

### 📢 **Credits**
Developed by **Remi** 🚀  

---

### 📜 License
This project is **free to use** and modify.
