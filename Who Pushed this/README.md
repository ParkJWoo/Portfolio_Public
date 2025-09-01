# 🃏 Who Pushed it?

## 🔎 README 개요
- 장르/기간: 카드 매칭 퍼즐 (1주, 2025.04)
- 역할: **클리어 성공/실패 UI, 스테이지 전환 UI/로직 담당**
- 기술: TextMeshPro, SceneManagement
- 성과: **클리어 시 직관적인 UI 피드백 제공**, **스테이지 클리어 후 다음 스테이지로 자연스럽게 전환**

---

## 🎮 Preview

![Image](https://github.com/user-attachments/assets/4de79d9f-be20-4038-a937-ccbaeb1cb51c)

---

## 🙋‍♂️ My Ownership
| 영역 | 내가 맡은 부분 |
|---|---|
| 클리어 UI | 카드 매칭 성공 시 "Stage Clear" 알림 UI 표시 |
| 실패 UI | 매칭 실패 시 "Retry / Game Over" 알림 UI 구현 |
| 스테이지 전환 | Stage Clear 후 다음 스테이지로 이동하는 UI/로직 구현 |

---

## 🧩 Key Challenges & Solutions
- **문제**: 클리어/실패 여부를 UI로 명확히 전달하기 어려움  
  **해결**: TextMeshPro 기반 알림 메시지와 애니메이션 효과로 직관적인 피드백 제공  

- **문제**: 스테이지 전환 시 장면이 갑자기 바뀌어 몰입감 저해  
  **해결**: UI 페이드 인/아웃 효과 + SceneManager 연계로 부드러운 전환 구현  

- **문제**: 재시작/다음 스테이지 선택 시 사용자 혼란 발생  
  **해결**: 명확한 버튼 UI 및 알림 메시지 배치로 플레이어 경험 개선  

---

# 1. 게임 개요 및 개발 기간

- **게임명** : `Who Pushed it?`
- **설명** : 팀원들의 사진이 담긴 카드를 뒤집어 같은 그림을 매칭하는 퍼즐 게임. 모든 카드를 맞추면 다음 스테이지로 이동. **중간에 초대받지 않은 "유령"이 있으니 주의하시길!**
- **타겟 플랫폼** : Microsoft Windows
- **개발 기간** : `2025.07.01 ~ 2025.07.14`
- **엔진/환경** : Unity 2022.3.17f1, C#

---

# 2. 구현 기능

### 클리어/실패 UI
- 매칭 성공 → "Stage Clear" 메시지 표시  
- 매칭 실패 → "Retry / Game Over" 메시지 표시  

### 스테이지 전환
- Stage Clear 시 "Next Stage" 버튼 활성화  
- 버튼 클릭 시 SceneManager를 통해 다음 스테이지 로드  

### UX 개선
- TextMeshPro 기반 알림 UI  
- 버튼/텍스트 애니메이션으로 몰입감 강화  
- 실패 → Retry, 성공 → Next Stage 분기 처리  

---

# 3. 기능 명세서 (코드 하이라이트)

| 스크립트 | 메서드 | 설명 |
|---|---|---|
| [UIManager.cs](링크) | ShowClearUI | 스테이지 클리어 시 메시지/버튼 표시 |
| [UIManager.cs](링크) | ShowFailUI | 스테이지 실패 시 메시지/버튼 표시 |
| [StageUIController.cs](링크) | OnNextStage | "Next Stage" 버튼 → SceneManager 전환 |
| [StageUIController.cs](링크) | OnRetry | "Retry" 버튼 → 현재 씬 재시작 |

---
