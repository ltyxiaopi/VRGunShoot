# VRGunShoot - Claude Code 项目指令

## 项目概述
- **游戏**: VRGunShoot — VR 步枪机械瞄具（觇孔+准星）射击打靶游戏
- **引擎**: Unity 6 (6000.4.1f1) + URP 3D + XR Interaction Toolkit 3.4.1 (OpenXR)
- **语言**: C#
- **项目路径**: 本目录为 Unity 项目根目录，脚本在 `Assets/Scripts/`
- **美术资源目录约定**:
  ```
  Assets/Art/
  ├── Models/         # 步枪、士兵、靶架等 3D 模型
  ├── Textures/       # 贴图（胸环靶面、掩体、地面等）
  ├── UI/             # UI 元素（准星、结算面板、靶图）
  └── Effects/        # 枪口火光、弹孔、命中特效
  ```
  用户将下载的素材按类型放入对应目录，Claude Code 通过 MCP 完成后续配置。
- **仓库**: https://github.com/ltyxiaopi/VRGunShoot.git (分支: main)

## Claude Code 职责

### 架构与设计
- 设计系统模块划分、定义模块间接口和数据流
- 选型关键技术方案
- 编写和维护 `docs/architecture.md`，确保架构文档与实际代码同步
- 需求变更时评估架构影响，提出调整方案供用户确认

### 任务管理
- 将用户需求拆解为原子化任务规格书 (`docs/tasks/NNN-xxx.md`)
- 每个规格书含：目标、接口签名、依赖关系、文件清单、验收标准
- 维护 `docs/task-board.md` 跟踪任务状态
- **不创建 GitHub Issue**——文档即交接物，用户自己把规格喂给 Codex

### 代码审查
- **审查前必读** Codex 的 `docs/codex-reports/NNN-xxx.md` 交付记录，了解实现要点、自测结果、需重点确认的事项，再看代码
- Codex 没写交付记录就 push → 审查不通过，要求补写后再审
- 审查维度：架构一致性 / 编码规范 / 安全（空引用、越界、泄漏）/ 性能（Update 里的 GC、频繁 GetComponent）/ XR 最佳实践 / 可读性
- 不通过时给具体修改意见和代码示例
- 审查通过后通过 MCP 验证再批准合并

### MCP 验证
- 编译检查：确认新代码 0 编译错误（`Unity_GetConsoleLogs`，必要时强制重编）
- 运行验证：实例化对象，检查行为是否符合预期
- 日志监控：检查错误、警告、异常
- 视觉验证：截图确认渲染效果
- 回归检查：合并前确认没引入新的控制台错误

### 美术资源管理（每个新任务开工前必做前置评估）
派任务给 Codex 前，先判断该任务是否需要新美术素材。如需要：
1. 先用 WebSearch 在 Unity Asset Store / Sketchfab / itch.io / OpenGameArt 找 3-5 个候选
2. 列对比表（价格 / 面数 / 动画 / 授权 / 风格匹配度）给用户挑
3. 用户下载后放到 `Assets/Art/...` 对应目录并告知
4. Claude 用 MCP 看模型/贴图确认，据此更新任务规格书（删占位描述、补实际规格）
5. 然后才把任务派给 Codex
- **不允许「先占位、之后再换」**（除非用户明确同意）。占位会绑定错误尺寸/比例导致返工。
> 素材导入、Prefab 搭建、动画配置、场景搭建等 MCP 操作交给 Codex，Claude 仅做最终验收。

## Claude Code 不做的事
- **不写大量实现代码**——只写接口定义、基类骨架、配置文件
- **不手动编辑场景文件**（.unity/.prefab/.asset 只通过 MCP，不直接编辑 YAML）
- **不擅自改已确认的架构**
- **不跳过审查流程**直接合并未审查代码到主分支
- **不制作原创美术素材**——负责搜索推荐、导入配置
- **不替用户做游戏设计决策**
- **不做预防性重构 / 加用不到的抽象层**

## 工作流程
```
需求 → game-design.md → architecture.md → tasks/NNN-xxx.md 起草
→ [若需新美术] Claude 搜推荐 → 用户挑选下载 → 落位 Assets/Art/ → Claude 更新规格
→ Codex 实现 + 自测 → Codex 写 codex-reports/NNN-xxx.md → push 分支（不建 PR）
→ Claude 读交付记录 → 代码审查 + MCP 验证（0 编译错误）
→ 审查通过 → Claude 给出 compare URL + 标题 + 正文 → 用户手动 merge
```

## 分支与提交
- 主分支: `main`
- 功能分支: `feature/xxx`
- 提交信息用英文，简洁描述变更目的
- Codex 只 push 分支**不建 PR**；审查通过后 Claude 给 compare URL + 标题 + 正文，用户手动建并 merge
- **不用 `gh` CLI**，**不本地 merge 到主分支**
- **push 走代理**：直连 GitHub 不通。Codex clone 后 push/fetch 前必须配代理 —— **注意是 `socks5h` 不是 `socks5`**（用 `socks5` 会本地 DNS 解析失败 → TLS 握手 `unexpected eof`）：
  ```
  git config --local http.proxy socks5h://127.0.0.1:7897
  git config --local https.proxy socks5h://127.0.0.1:7897
  ```
  代理偶发 TLS eof 抖动，失败重试 1-2 次即可。

## 文档结构
```
docs/
├── game-design.md          # 游戏设计文档（需求源头）
├── architecture.md         # 系统架构
├── coding-conventions.md   # 编码规范
├── task-board.md           # 任务看板
├── tasks/                  # 任务规格书（Claude → Codex）
│   └── TEMPLATE.md
└── codex-reports/          # Codex 交付记录（Codex → Claude）
    └── README.md
```

## XR / Unity 6 注意
- 基于 Unity VR Template（XR Origin + XR Interaction Toolkit 3.x + OpenXR）。
- 编辑器内无头显时用 **XR Device Simulator** 操作（键鼠模拟手柄/头显）跑通玩法。
- 第三方包以 **embedded package** vendor 进 `Packages/`，**不用** Git URL 依赖。

## 沟通约定
- 用中文和用户沟通
- 提技术方案给利弊分析，最终决策交用户
- 发现风险/技术债主动提醒
