# VRGunShoot - 系统架构

> 本文档由 Claude Code 维护，新增系统模块时同步更新，确保与实际代码一致。

## 模块总览
```
                ┌─────────────────┐
                │  ModeManager    │  主菜单 / 三模式入口 / 场景加载
                └────────┬────────┘
        ┌────────────────┼────────────────┐
   训练模式流程        校枪流程         攻防战流程
   (TrainingMode)     (ZeroingMode)    (CombatMode)
        └────────────────┼────────────────┘
                         │ 共用
        ┌────────────────┴───────────────────────────┐
   枪械系统 Weapon                              目标/计分
   ├ RifleGrab    持枪抓取(XRI)                ├ TargetBoard 胸环靶+环区
   ├ IronSights   觇孔+准星几何/三点一线        ├ ScoreCalculator 环数
   ├ Ballistics   射线→命中→弹着(zeroOffset)    ├ EnemyTarget 敌兵+出现方式
   └ Recoil/Bolt  后座+栓动/半自动              └ ResultUI 靶图/弹孔/评分/文本
```

## 各模块职责与接口（随实现细化）
### 枪械系统 Weapon
- **职责**：持枪交互、机械瞄准、开火弹道、后座/上膛。
- **关键类型**（草案，规格书里定签名）：`RifleController` / `IronSights` / `Ballistics` / `RecoilController`。
- **依赖**：XR Interaction Toolkit、Input System。
- **数据流**：扣扳机 → Ballistics 射线（叠加 `zeroOffset`）→ 命中点 → 通知 TargetBoard。

### 目标 / 计分
- **职责**：胸环靶环区判定、环数计算、敌兵受击、结算数据。
- **关键类型**：`TargetBoard` / `ScoreCalculator` / `EnemyTarget`。

### 校枪 Zeroing
- **职责**：注入未知 `zeroOffset`、填空换算校验、应用修正、合格判定。
- **换算常量**：准星 360°↔上下 24cm；觇孔 1 格↔左右 2cm（见 game-design）。

### 模式管理 / UI
- **职责**：主菜单、模式切换、结算面板、校枪面板（VR World-Space UI）。

## 关键技术决策（ADR 风格）
### 2026-06-03: 命中计算预留 zeroOffset
- **背景**：校枪模块要求"柱顶压靶心但弹着点有偏移"。
- **选择**：Ballistics 命中点统一叠加一个可配置 `zeroOffset(dx,dy)`，训练/攻防战为 0，校枪注入未知值。
- **理由**：三模块共用一套弹道，校枪只是给同一管线注入偏移，避免分叉实现。

### 2026-06-03: 分析文本走规则模板
- **选择**：根据弹着点散布中心的方向/距离，套预写中文模板，不联网调 AI。
- **理由**：离线、确定、零成本、VR 内即时。

## 数据格式约定
- 距离/靶面/环值等配置用 ScriptableObject（规格书里定）。
- 一局成绩（5 发弹着点 + 环数 + 评分）用普通可序列化结构在内存传递，暂不落盘存档。
