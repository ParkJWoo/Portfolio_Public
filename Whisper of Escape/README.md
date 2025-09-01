# 👻 Whisper of Escape

## 🔎 README 개요
- 장르/기간: 3D 호러 퍼즐 (1주, 2025.06)
- 역할: **Whisper 퍼즐 시스템, Slenderman AI(FSM/스폰/추격), 카메라 연출**
- 기술: FSM, NavMesh, Coroutine, Cinemachine, Vignette(Post Processing)
- 성과: **퍼즐 ↔ 추격 루프 설계**로 공포감 강화 / 카메라 연출을 통한 긴장감 극대화
- ▶️ [시연 영상](https://www.youtube.com/watch?v=ufxCjNZIOT4)

---

## 🙋‍♂️ 작업 내역
| 영역 | 내가 맡은 부분 |
|---|---|
| Whisper Puzzle | WhisperTrigger 리팩토링 (Trigger / EffectController / Spawner 분리) |
| Slenderman AI | NavMesh 기반 소환, FSM 상태(Idle→Chase→Vanish) |
| 퍼즐-추격 연계 | SuppressWhisper() → Slenderman 비활성화 후 일정 시간 뒤 재등장 루프 |
| 카메라 연출 | 비네트 효과(Post Processing), Cinemachine을 활용한 추격 시점 전환 |
| 사운드 연출 | Whisper 사운드 반복/중단, Slenderman 출현 SFX 처리 |

---

## 🧩 트러블 슈팅 & 해결 방식 (상세 내용: [해당 링크 참고](https://velog.io/@character453/%EB%B3%B8%EC%BA%A0%ED%94%84-9%EC%A3%BC%EC%B0%A8-%EC%8A%AC%EB%9E%9C%EB%8D%94%EB%A7%A8-%EC%86%8C%ED%99%98%EC%B6%94%EA%B2%A9-%EB%B9%84%EC%A0%95%EC%83%81-%EB%8F%99%EC%9E%91-%EC%9D%B4%EC%8A%88))

- **문제**: Slenderman 소환 시 NavMesh 위에서 부자연스러운 위치에 소환되는 이슈 발생  
  **해결**: `NavMesh.SamplePosition` 활용, 일정 반경 내에서 자연스러운 위치 보정  

---

# 1. 게임 개요 및 개발 기간

- **게임명** : `Whisper of Escape`
- **설명** : 속삭임 퍼즐을 해결하며 Slenderman의 추격을 피해 탈출하는 3D 호러 퍼즐 게임
- **타겟 플랫폼** : Microsoft Windows
- **개발 기간** : `2025.06.01 ~ 2025.06.15`
- **엔진/환경** : Unity 2022.3.17f1, C#

---

# 2. 구현 기능

### Whisper Puzzle
- 퍼즐 방에서 일정 시간 내 정답 사진 상호작용 필요
- 실패 시 Slenderman 재등장 및 추격 시작
- SuppressWhisper() → Whisper 사운드/Slenderman 비활성화 후 일정 시간 뒤 재개

### Slenderman AI
- 일정 거리 내에서 NavMesh 기반 자연스러운 위치로 소환
- 소환 즉시 추격 상태 진입
- 추격 중 퍼즐 해결 시 Slenderman 사라짐
- FSM 기반 Idle/Chase/Disappear 상태 전환

### Camera & Visual Effect
- **Cinemachine Virtual Camera**를 활용해 퍼즐 ↔ 추격 상황에 따라 시점 전환
- **비네트(Post Processing)** 효과로 추격 상황 시 긴장감 강화
- 카메라 Shake 및 시야 전환 효과로 플레이어 불안감 유도  

---

# 3. 기능 명세서 (코드 하이라이트)

| 스크립트 | 설명 |
|---|---|
| [WhisperTrigger.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Whisper%20of%20Escape/CodeSamples/1.%20Enemy/Whisper/WhisperTrigger.cs) | 퍼즐 방 입장 시 트리거 발생, WhisperEffectController/Spawner 연동 |
| [WhisperEffectController.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Whisper%20of%20Escape/CodeSamples/1.%20Enemy/Whisper/WhisperEffectController.cs) | Whisper 사운드 관리, Suppress → 재개 루프 |
| [SlendermanSpawner.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Whisper%20of%20Escape/CodeSamples/1.%20Enemy/Whisper/SlendermanSpawner.cs) | NavMesh.SamplePosition 기반 소환 위치 계산 |
| [Enemy.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Whisper%20of%20Escape/CodeSamples/1.%20Enemy/Enemy.cs) | OnEnable 시 FSM 진입, Slenderman 추격 로직 |

---

# 4. 사용 에셋
- [Vintage Living Room 3D Game Pack (배경 프랍)](https://assetstore.unity.com/packages/3d/environments/vintage-living-room-3d-game-pack-314464)
- [Scary Man Free (Slenderman 캐릭터)](https://assetstore.unity.com/packages/3d/characters/scary-man-free-173376)
- [Jewelry Shop (아이템 오브젝트)](https://assetstore.unity.com/packages/3d/environments/jewelry-shop-261543)
- [Free Horror SFX Pack](https://assetstore.unity.com/packages/audio/music/free-horror-starter-pack-211340)
