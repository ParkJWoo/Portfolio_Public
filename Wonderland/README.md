# 🐇 Wonderland

## 🔎 README 개요
- 장르/기간: 3D 퍼즐 플랫폼 (1주, 2025.06)
- 역할: **플레이어 조작, 인벤토리 시스템, 조명 연출, 위치 저장 & 로드 기능, 가이드 문구 출력 기능**
- 기술: Unity Input System, ScriptableObject, Save/Load System, Lighting(URP/2D Light), Coroutine
- 성과: **플레이어 이동/점프 안정화**, **슬롯 기반 인벤토리 및 아이템 사용 시스템 완성**,  
  **위치 저장/로드 기능으로 진행 데이터 유지**, **조명 연출로 공간 분위기 강화**
- ▶️ [시연 영상](https://drive.google.com/file/d/13P6lxGPXSjomlvTmESlo4HdzRF8pPvOT/view?usp=drive_link)

---

## 🎮 Preview
<img width="808" height="449" alt="image" src="https://github.com/user-attachments/assets/63b14f4b-65a3-4f1f-88dc-c0c35f6a2e49" />


[맵 회전](https://www.youtube.com/watch?v=m8gV3JLoujg&t=1s)

[장갑 아이템 사용](https://www.youtube.com/watch?v=_2X0z7_6pKg)

[케이크 아이템 사용](https://www.youtube.com/watch?v=tuTkrrDmda8)

[모래시계 아이템 사용](https://www.youtube.com/watch?v=byBp0D_e7UQ&t=1s)

---

## 🙋‍♂️ 작업 내역
| 영역 | 내가 맡은 부분 |
|---|---|
| 플레이어 | Unity Input System 기반 이동/점프, 물리 제어 |
| 인벤토리 | 슬롯 기반 1~3번 키 선택, 마우스 좌/우 클릭으로 아이템 사용 |
| 조명 시스템 | URP 기반 조명 적용, 분위기 조정용 Light 제어 |
| 저장 & 로드 | 플레이어 위치 저장, 씬 재진입 시 위치 복원 로직 구현 |

---

## 🧩 트러블 슈팅 & 해결 방식 (상세 내용: [해당 링크 참고](https://velog.io/@character453/%EB%B3%B8%EC%BA%A0%ED%94%84-7%EC%A3%BC%EC%B0%A8-%EC%9D%B8%EB%B2%A4%ED%86%A0%EB%A6%AC-%EC%95%84%EC%9D%B4%ED%85%9C-%EC%82%AC%EC%9A%A9-%EC%8B%9C-%EB%8C%80%EC%83%81-%EB%AF%B8%EC%97%B0%EA%B2%B0%EB%90%98%EB%8A%94-%EC%9D%B4%EC%8A%88))
- **문제**: 인벤토리 아이템을 선택해 사용했을 때 기능이 정상적으로 동작하지 않던 이슈
- **해결**: 실행 주체를 플레이어로 명확히 설정 후, 아이템 별 호출 경로 보강 및 인벤토리와 실행 로직 연결을 통해 해결
  
---

# 1. 게임 개요 및 개발 기간

- **게임명** : '괴상한 나라의 앨리스(Wonderland)'
- **설명** : 이상한 나라의 앨리스 콘셉트의 3D 퍼즐 플랫폼 게임. 플레이어는 이동/점프, 인벤토리 아이템 사용, 위치 저장/로드를 통해 퍼즐을 공략.
- **타겟 플랫폼** : Microsoft Windows
- **개발 기간** : `2025.06.01 ~ 2025.06.21`
- **엔진/환경** : Unity 2022.3.17f1, C#

---

# 2. 구현 기능

### 플레이어
- Unity Input System 기반 이동/점프
- Rigidbody 기반 물리 이동 (MovePosition)

### 인벤토리
- 슬롯 3개 (1~3번 키로 선택)
- 마우스 좌/우 클릭 → 선택된 아이템의 `IUsableItem` 실행
- 소비형 아이템(크기 조절 등) 지원

### 조명 시스템
- URP Light 사용, 맵 분위기 연출
- 맵 전체에 촛불 형식의 깜빡깜빡이는 조명 기능 구현

### 위치 저장 & 로드
- 플레이어 좌표를 저장 파일에 기록
- 씬 재시작/이동 시 해당 좌표에서 복원

---

# 3. 기능 명세서 (코드 하이라이트)

| 스크립트 | 메서드 | 설명 |
|---|---|---|
| [PlayerController.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Wonderland/CodeSamples/1.%20Player/PlayerController.cs) | Move, Jump | Input System 기반 이동/점프 처리 |
| [PlayerInteraction.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Wonderland/CodeSamples/1.%20Player/PlayerInteraction.cs) | OnInteractInput, OnMouseLeftClick, OnMouseRightClick, OnRespawnInput | 게임 내 플레이어와 오브젝트 간 상호작용 기능 로직 |
| [Inventory.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Wonderland/CodeSamples/2.%20UIs/Inventory/Inventory.cs) | UseItem, SelectSlot | 슬롯 선택 & 아이템 사용 로직 |
| [ItemSlot.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Wonderland/CodeSamples/2.%20UIs/ItemSlot/ItemSlot.cs) | Select, Deselect, Clear | 인벤토리 내 슬롯 관리 로직 |
| [LightFricker.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Wonderland/CodeSamples/5.%20Lighting/LightFricker.cs) |  | URP Light 연출 제어 |
| [SavePoint.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Wonderland/CodeSamples/3.%20SaveLoad/SavePoint.cs) |  | 플레이어 위치 저장 |
| [ManualGuide.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Wonderland/CodeSamples/4.%20Guide/ManualGuide.cs) | GuideStart, ShowControlsRoutine | 튜토리얼 목적의 가이드 문구 순차적 출력 로직 |


---

# 4. 사용 에셋
- [Alchemist House](https://assetstore.unity.com/packages/3d/environments/alchemist-house-112442?srsltid=AfmBOoqyE0zWxdF3Ju8wHoGMm0KliWHVeekSHbuZd3MQfvzx1vCZy58X)
