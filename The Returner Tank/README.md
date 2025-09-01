# 🚩 탱크로 다시 태어난 나는 미궁을 방랑한다

## 🔎 README 개요
- 장르/기간: 탑다운 슈팅 + 던전 크롤러 (1주, 2025.05)
- 역할: **웨이브 & 던전 매니저, 몬스터 오브젝트 풀링, 스포너, 스테이지/레벨 관리**
- 기술: FSM, Object Pooling, Wave System, Dungeon Manager
- 성과: **오브젝트 풀링 기반 웨이브 시스템 구현 → 성능 안정화**,  
  **던전 레벨/웨이브 구조 설계 → 확장성과 난이도 조절 용이**
- ▶️ [시연 영상](https://www.youtube.com/watch?v=Z0R4iaVQl8I)

---

## 🎮 Preview
| ![Movie_023](https://github.com/user-attachments/assets/58750829-5e68-43b0-9459-84a73bbfcdc4) | ![Movie_024](https://github.com/user-attachments/assets/dc49e93b-5000-4b90-843b-af1719bb0ce3) |
|---------------------------------------------------------------------------------------------- | --------------------------------------------------------------------------------------------- |
| 게임시작 화면                                                                                  | 맵 선택지                                                                                      | 
| ![Movie_025](https://github.com/user-attachments/assets/4459db3f-641b-4810-a174-b2de3bf9b78b) | ![Movie_026](https://github.com/user-attachments/assets/02d66ecd-33d9-4516-a039-daa1051af3aa) |
| 공격 화면       

---

## 🙋‍♂️ 작업 내역
| 영역 | 내가 맡은 부분 |
|---|---|
| 몬스터 풀링 | `Dungeon.cs` 내부 풀 매니저 기능 구현 → 오브젝트 배열 기반 풀링 |
| 스포너 & 웨이브 | `Spawner.cs`, `Dungeon.cs` → 스폰 포인트 관리 및 웨이브 진행 |
| 웨이브/던전 매니저 | `DungeonManager.cs` → 웨이브 시작/종료, 던전 클리어 처리 |
| 스테이지/던전 정보 | `GameManager.cs` → 스테이지 입장 시 초기화, 던전 클리어 시 레벨 증가 |

---

## 🧩 트러블 슈팅 & 해결 방식 (상세 내용: [해당 링크 참고](https://velog.io/@character453/%EB%B3%B8%EC%BA%A0%ED%94%84-5%EC%A3%BC%EC%B0%A8-%ED%83%B1%ED%81%AC%EB%A1%9C-%ED%83%9C%EC%96%B4%EB%82%9C-%EB%82%98%EB%8A%94-%EB%AF%B8%EA%B6%81%EC%9D%84-%EB%B0%A9%EB%9E%91%ED%95%9C%EB%8B%A4-67))
- **문제**: 오브젝트 풀링 방식을 활용한 몬스터 재스폰 과정에서 생긴 이슈
  **해결**: 재스폰 시 몬스터 값들을 초기화하여 해결

---

# 1. 게임 개요 및 개발 기간

- **게임명** : `탱크로 다시 태어난 나는 미궁을 방랑한다`
- **설명** : 던전 속에서 탱크가 되어 미궁을 탐험하고 적들을 처치하는 탑다운 슈팅 게임
- **타겟 플랫폼** : Microsoft Windows
- **개발 기간** : `2025.05.01 ~ 2025.05.15`
- **엔진/환경** : Unity 2022.3.17f1, C#

---

# 2. 구현 기능

### 몬스터 생성 & 풀링 (Dungeon.cs)
- **Enemies 배열**: 몬스터 프리팹 등록
- **pools 리스트 배열**: 프리팹별 풀 관리
- `CreateEnemies(index)`: 비활성 객체 재사용 → 없으면 Instantiate  
- `ResetEnemy()`: 몬스터 상태 초기화 후 재사용
- ⇒ **Dungeon.cs가 PoolManager 역할** 수행

### 스포너 & 웨이브 시스템
- **Spawner.cs**: SpawnPoint 배열에 따라 웨이브별 몬스터 생성
- 웨이브는 던전당 최대 3개, 마지막 웨이브 종료 시 던전 클리어
- 던전 레벨 상승 시 몬스터 수/종류 변화 적용

### 던전 매니저
- **DungeonManager.cs**  
  - `StartWave`: 웨이브 시작 시 적 수 설정  
  - `OnEnemyDeath`: 몬스터 사망 시 남은 수 추적, 웨이브 종료 조건 확인  
  - `ClearDungeon`: 던전 클리어 처리  

### 스테이지 & 던전 정보
- **GameManager.cs**  
  - `SetStageInfo`: 스테이지 입장 시 정보 초기화  
  - `IncreaseDungeonLevel`: 던전 클리어 시 레벨 증가  

---

# 3. 기능 명세서 (코드 하이라이트)

| 스크립트 | 메서드 | 설명 |
|---|---|---|
| [DungeonManager.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/The%20Returner%20Tank/CodeSamples/0.%20Managers/DungeonManager.cs) | StartWave, OnEnemyDeath, ClearDungeon | 웨이브 시작/종료 및 던전 클리어 처리 |
| [Spawner.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/The%20Returner%20Tank/CodeSamples/1.%20Entity/1.%20Dungeons/Spawner.cs) | (전체) | 웨이브별 몬스터 스폰 담당 |
| [Dungeon.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/The%20Returner%20Tank/CodeSamples/1.%20Entity/1.%20Dungeons/Dungeon.cs) | CreateEnemies | 오브젝트 풀링 기반 몬스터 생성/재사용 |
| [GameManager.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/The%20Returner%20Tank/CodeSamples/0.%20Managers/GameManager.cs) | SetStageInfo, IncreaseDungeonLevel | 스테이지 입장 초기화 & 던전 클리어 시 레벨 증가 |
---

# 4. 사용 에셋
- [Free Tank Model](생성형 AI로 제작)
- [Top-down Dungeon Tileset](https://0x72.itch.io/dungeontileset-ii)
- [Free SFX Pack (Gun/Explosion)](생성형 AI로 제작)
