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
