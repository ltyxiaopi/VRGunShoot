# VRGunShoot - 任务看板

> **下一个**：001。带 🎨 的任务派 Codex 前 Claude 先走「美术先行」搜素材给用户挑。

## 待实现 (TODO)

### 阶段 1 · 核心枪械系统（三模块共用，MVP）
- [002 - 步枪 + 双手持枪抓取](tasks/002-rifle-grab.md) 🎨 — XRI 抓取、持枪姿态
- [003 - 机械瞄具 + 三点一线](tasks/003-iron-sights.md) — 觇孔/准星几何、柱顶压靶心判定、射线开火
- [004 - 弹道/命中系统](tasks/004-ballistics.md) — 射线→命中点→弹着点，预留可注入 `zeroOffset`
- [005 - 后座 + 栓动循环](tasks/005-recoil-bolt.md) — 开火后枪偏移、物理重新举枪、单发上膛

### 阶段 2 · 胸环靶 + 计分
- [006 - 胸环靶 + 环数判定](tasks/006-target-board.md) 🎨 — 1.7m 靶、环区→环数、100m/200m 配置
- [007 - 结算 UI](tasks/007-result-ui.md) — 靶图 + 弹孔渲染 + 评分(30-35/36-44/45+)
- [008 - 规则模板分析文本](tasks/008-analysis-text.md) — 弹着点偏移→中文建议

### 阶段 3 · 训练模式
- [009 - 训练模式串联](tasks/009-training-mode.md) — 选距离→5发(后座重瞄)→结算

### 阶段 4 · 校枪
- [010 - 校枪偏移模型](tasks/010-zeroing-model.md) — zeroOffset + 准星(度/竖直)/觇孔(格/水平)换算
- [011 - 校枪 UI 全流程](tasks/011-zeroing-ui.md) — 弹着图→填空→校验→修正→高亮新瞄点→距靶心→5cm内合格

### 阶段 5 · 城市攻防战
- [012 - 敌人 + 三种出现方式](tasks/012-enemy-targets.md) 🎨 — 趴/直线/斜向、受击判定
- [013 - 掩体场景 + 探身射击](tasks/013-cover-scene.md) 🎨 — 城市掩体、探身/起蹲
- [014 - 攻防战流程](tasks/014-combat-mode.md) — 随机刷敌 + 半自动 + 5秒全歼合格 + 结算

### 阶段 6 · 导航
- [015 - 主菜单 + 模式选择](tasks/015-main-menu.md) — 三模式入口 (VR World-Space UI)

## 进行中 (In Progress)
暂无

## 审查中 (In Review)
- [001 - VR 基础跑通](tasks/001-vr-bootstrap.md) — 首轮审查 0 编译错误、结构达标，**返工 R1**（双 Simulator，见规格书「审查返工」）→ 待 Codex 修后复审

## 已完成 (Done)
- 000 - 项目脚手架（Claude 直接铺：CLAUDE.md + docs 结构 + 模板 + 设计/架构文档）

<!--
状态流转：TODO → In Progress（已派给 Codex）→ In Review（已 push 待审）→ Done（PR 合并）
Done 条目建议记录：PR 号 + 合并 commit + 一句话要点 + 交付记录链接，例如：
- [001 - VR 基础跑通](tasks/001-vr-bootstrap.md) — 通过（PR #1, main abc1234），[交付记录](../codex-reports/001-vr-bootstrap.md)
-->
