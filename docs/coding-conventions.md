# 编码规范（Unity 6 + XR Interaction Toolkit / C#）

> Codex 实现时遵守，Claude 审查时据此把关。随项目演进迭代更新。

## 命名
- 类型 / 方法 / 公共成员：`PascalCase`
- 局部变量 / 参数：`camelCase`
- 私有字段：`_camelCase`
- 常量：`PascalCase` 或 `UPPER_SNAKE`（项目内统一）

## MonoBehaviour
- 生命周期方法顺序：`Awake` → `OnEnable` → `Start` → `Update` → `FixedUpdate` → `OnDisable`
- `[SerializeField] private` 暴露给 Inspector，不用 public 字段
- 缓存 `GetComponent` 结果，**不在 `Update` 里反复 GetComponent / 分配 GC**
- 物理相关逻辑放 `FixedUpdate`

## XR / Interaction Toolkit
- 优先用 XRI 现成组件（`XRGrabInteractable`、`XRRayInteractor`、`ActionBasedController`）而非自造
- 输入统一走 Input System 的 `InputActionReference`，不直接读设备
- 涉及头显/手柄位姿的逻辑放 XR Origin 体系下，世界坐标换算注意 `XROrigin`
- 编辑器内用 XR Device Simulator 验证，避免硬编码依赖真机

## 安全与健壮性
- 引用使用前判空；数组/列表访问注意越界
- 订阅事件（含 XRI interactable 事件）要在 `OnDisable`/`OnDestroy` 取消订阅，避免泄漏
- 协程注意停止时机

## 复杂度
- 不做预防性重构、不加用不到的抽象层
- 函数单一职责，过长则拆分

## 注释
- 注释解释「为什么」，不复述「做了什么」
- 公共 API 用 XML doc 注释
