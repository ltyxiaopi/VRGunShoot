# 任务 001 - VR 基础跑通 + 工程骨架

## 目标
把 VR Template 整理成可持续开发的雏形：建立脚本/美术目录骨架、一个干净的主训练场景（XR Origin + 地面 + 光 + 占位靶位），并确认在编辑器内用 **XR Device Simulator** 可操作（移动/转头/手柄射线），为后续枪械任务打底。本任务**不写玩法逻辑、不需要新美术**。

## 美术资源前置检查
- **是否需要新素材**: 否。

## 范围
### 1. 目录骨架
建立并各放一个 `.gitkeep` 占位：
- `Assets/Scripts/Weapon/`、`Assets/Scripts/Targets/`、`Assets/Scripts/Modes/`、`Assets/Scripts/UI/`
- `Assets/Art/Models/`、`Assets/Art/Textures/`、`Assets/Art/UI/`、`Assets/Art/Effects/`

### 2. 程序集定义
- `Assets/Scripts/VRGunShoot.asmdef` — 程序集名 `VRGunShoot`，引用：`Unity.XR.Interaction.Toolkit`、`Unity.InputSystem`、`Unity.XR.CoreUtils`。后续脚本都归这个 asmdef。

### 3. 主场景 `Assets/Scenes/Range.unity`
- 复用 VR Template 的 XR Origin (XR Rig) 体系（含 XR Interaction Manager、左右手 Controller、Device Simulator）。
- 一块水平地面（Plane/Cube，足够延伸到 200m），方向光，简单天空/环境光即可。
- 一个固定**射击工位**空物体 `FiringPoint`（玩家起始位，朝向 +Z 看向靶位方向）。
- 两个占位靶位锚点空物体：`TargetAnchor_100m`（距 FiringPoint 100m）、`TargetAnchor_200m`（200m），仅作 Transform 标记，先各放一个临时 Cube 可视化，后续 006 替换为真胸环靶。
- 确认 **XR Device Simulator** 在场景内启用（Template 自带的 simulator prefab，或 XRI Samples 里的）。

### 4. 构建设置
- 把 `Range` 加入 Build Settings 场景列表第 0 位。

## 依赖
- 无（基于现有 VR Template）。

## 文件清单
- `Assets/Scripts/VRGunShoot.asmdef`
- `Assets/Scripts/{Weapon,Targets,Modes,UI}/.gitkeep`
- `Assets/Art/{Models,Textures,UI,Effects}/.gitkeep`
- `Assets/Scenes/Range.unity`

## 验收标准
- [ ] 0 编译错误（MCP `Unity_GetConsoleLogs` 确认）。
- [ ] 进入 Play，用 XR Device Simulator 能移动、转头、看到左右手柄与射线交互。
- [ ] 场景含 `FiringPoint` / `TargetAnchor_100m` / `TargetAnchor_200m`，距离正确（100m / 200m）。
- [ ] 目录骨架与 asmdef 就位，新脚本归属 `VRGunShoot` 程序集。
- [ ] `Range` 在 Build Settings 第 0 位。

## 注意事项
- 不删 Template 里 XR/OpenXR 的配置资产，只在其基础上整理出 Range 场景。
- 距离用真实尺度（1 unit = 1m），100m/200m 用真实坐标摆放，别缩放糊弄——后续弹道按真实距离算。
- Device Simulator 只在 Editor 生效，不影响真机；保留 Template 的 XR 运行时配置。

## 交付记录（Codex 必填）
完成并自测后，push 分支前在 `docs/codex-reports/001-vr-bootstrap.md` 写交付记录（参考 `docs/codex-reports/README.md`）。创建 `feature/001-vr-bootstrap` 分支并 push，**不要创建 PR**，等 Claude 审查。

## 审查返工 (Rework · Claude 2026-06-03)
首轮审查：结构/坐标/距离/Build Settings/asmdef/Device Simulator vendor 全部达标，MCP 编译验证 0 编译错误。**1 处需修后再审：**

### R1 — Simulator 双重实例化（必修）
- **现象**：`Assets/XRI/Settings/Resources/XRDeviceSimulatorSettings.asset` 里 `m_AutomaticallyInstantiateSimulatorPrefab: 1`（编辑器 Play 时自动生成 Simulator 实例），**同时** `Range.unity` 里又手动摆了一个 `XR Device Simulator` 实例（prefab guid `18ddb545287c546e19cc77dc9fbb2189`）。进 Play 后会同时存在两个 Simulator 抢输入，键鼠模拟手柄/头显会异常或叠加。
- **修法（二选一，**采用方案 A**）**：
  - **方案 A（采用）**：保留项目设置的自动实例化（editor-only、跨场景通用，后续每个新场景都不必再摆），**删掉 `Range.unity` 里手动摆放的那个 `XR Device Simulator` GameObject 实例**。`XRDeviceSimulatorSettings.asset` 维持 `m_AutomaticallyInstantiateSimulatorPrefab: 1` / `m_AutomaticallyInstantiateInEditorOnly: 1` 不变。
  - 方案 B（不采用）：反过来——保留场景实例，把设置的自动实例化关掉。因后续会有多个场景，A 更省事，故定 A。
- **自测**：改完进 Play，确认场景里**只有一个** `XRDeviceSimulator` 组件实例（可 `FindObjectsByType<XRDeviceSimulator>` 计数 == 1），键鼠能移动/转头、左右手柄射线正常。
- **交付**：在 `codex-reports/001-vr-bootstrap.md` 追加「Rework R1」小节说明改动与自测结果，push 到原分支 `feature/001-vr-bootstrap`，等 Claude 复审 + Play 实测。
