# 🐇 Project Rabbit

## 🔎 README 개요

- 장르/기간: 2D 액션 (8주, 2025.06–08)
- 역할: **보스 FSM/패턴, 정예 몹 FSM 3종, 적 풀링/웨이브 리팩토링**
- 기술: FSM, Delegate 이벤트, Object Pooling, URP Shader
- 성과: 패턴 확장성↑, 풀링으로 **GC 스파이크 제거/프레임 안정화**
- ▶️ [시연 영상](https://youtu.be/8UbreDtBYWE) · ⬇️ [Windows 빌드](https://drive.google.com/drive/folders/1RzH7ExUxHPEqNFo1FC_CEj43eHt-KDKe)

## 🎮 Preview
![TitleScene](https://github.com/RanKa110/Rabbit/blob/main/Assets/99.%20Externals/image.png)
![TutorialScene](https://github.com/RanKa110/Rabbit/blob/main/Assets/99.%20Externals/image%20(1).png)
![vsBossGIF](https://github.com/RanKa110/Rabbit/blob/main/Assets/99.%20Externals/3.gif)
![vsEnemy](https://github.com/RanKa110/Rabbit/blob/main/Assets/99.%20Externals/image%20(2).png)

## 🙋‍♂️ 작업 내역

| 영역 | 내가 한 일 | 근거 |
|---|---|---|
| Boss FSM | Idle/Chase/Attack/Phase2, 전이 규칙/핸들러 조립 | [BossController.cs](https://github.com/RanKa110/Rabbit/blob/main/Assets/02.%20Scripts/Controller/BossController.cs) · [BossStates.cs](https://github.com/RanKa110/Rabbit/blob/main/Assets/02.%20Scripts/State/Boss/BossStates.cs) |
| Attack Patterns | Charge / Shelling / Shoot / Summon / TNT | [BossPatterns/](https://github.com/RanKa110/Rabbit/tree/main/Assets/02.%20Scripts/Boss/Attack/BossPatterns) |
| Enemy 리팩토링 | 정예 3종 FSM, Normal 라인 정리 | [Elite/](https://github.com/RanKa110/Rabbit/tree/main/Assets/02.%20Scripts/Controller/Enemies/Elite) |
| 풀링/웨이브 | ProjectilePool, EliteEnemyPool, WaveManager | [Pooling/](https://github.com/RanKa110/Rabbit/tree/main/Assets/02.%20Scripts/Enemy/EliteEnemy/Pooling) |

## 🧩 Key Challenges & Solutions
- 돌진 패턴에서 벽 끼임/수직 튕김 → **넉백 벡터 클램프 + 충돌 각도 보정** → 재현 불가
- WebGL 빌드 실행 불가 → **압축 포맷/로더 설정 교정** → 브라우저 정상 실행
- 적 스폰/반납 시 GC 스파이크 → **풀링 Reset/Return 규약** → 프레임 안정화

---

# 1. 게임 개요 및 개발 기간

- **게임명** : `Project Rabbit`
- **설명** : 2D 픽셀 횡스크롤 액션 어드벤쳐 게임
- **개요** : 캐릭터를 조작해 적들을 물리치고 나아가 스테이지를 클리어하자.
- **게임 방법**
    - [AD] or [⬅️➡️] : 이동
    - [SPACE] : 점프 / 2단 점프
    - [Left Mouse] : 공격
    - [Right Mouse] : 방어
    - [E] : 상호작용
    - [Esc] : 옵션
- **타겟 플랫폼** : Microsoft Windows
- **개발 기간** : `2025.06.20 ~ 2025.08.12`
- **엔진/환경** : Unity 6000.1.8f1, Visual Studio, GitHub

---

# 2. 구현 기능

## **시작 화면**
- 게임 시작 시 실행되는 씬
- 새 게임, 불러오기, 설정, 게임 종료 선택지가 있다.

## **새 게임 선택 화면**
- 튜토리얼 여부 선택
- "아니오" → 튜토리얼 후 게임 시작 / "예" → 튜토리얼 스킵 후 바로 게임 시작

## **불러오기 선택 화면**
- 저장한 시점을 불러오는 씬 (10개 슬롯 제공)

## **게임 화면**
- 최종 목표: 보스를 처치하고 스테이지 클리어
- 구역별 적 처치 및 상황별 대처가 게임의 재미를 좌우

---

# 3. 기능 명세서

<details>
<summary>UML & 다이어그램</summary>

#### 보스 구조
![보스 구조](https://github.com/RanKa110/Rabbit/blob/main/Assets/99.%20Externals/%EC%B5%9C%EC%A2%85%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8_%EB%B3%B4%EC%8A%A4%ED%81%B4%EB%9E%98%EC%8A%A4%EB%8B%A4%EC%9D%B4%EC%96%B4%EA%B7%B8%EB%9E%A8.png)

#### 적 소환
![적 소환](https://github.com/RanKa110/Rabbit/blob/main/Assets/99.%20Externals/%EC%B5%9C%EC%A2%85%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8_%EC%A0%81%EC%86%8C%ED%99%98%ED%81%B4%EB%9E%98%EC%8A%A4.png)

#### 투사체
![투사체 구조](https://github.com/RanKa110/Rabbit/blob/main/Assets/99.%20Externals/%EC%B5%9C%EC%A2%85%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8_%ED%88%AC%EC%82%AC%EC%B2%B4%ED%81%B4%EB%9E%98%EC%8A%A4%EA%B5%AC%EC%A1%B0%EB%8F%84.png)

#### 적 (Elite / Normal)
![적 구조](https://github.com/RanKa110/Rabbit/blob/main/Assets/99.%20Externals/%EC%B5%9C%EC%A2%85%ED%94%84%EB%A1%9C%EC%A0%9D%ED%8A%B8_%EC%A0%81%ED%81%B4%EB%9E%98%EC%8A%A4%EB%8B%A4%EC%9D%B4%EC%96%B4%EA%B7%B8%EB%9E%A8.png)

</details>

---

## **보스**

### 1) Attack Patterns
| 스크립트 | 내용 | 기여자 |
|---|---|---|
| [HitData.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/3.%20Attack/HitData.cs) | 공격 시 데미지, 넉백, 공격 타입 등의 데이터를 담는 구조체 | 박진우 |
| [BaseAttackPattern.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/3.%20Attack/BossPatterns/BaseAttackPattern.cs) | 모든 보스 공격 패턴의 기본 동작과 인터페이스 정의 | 박진우 |
| [PatternContext.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/3.%20Attack/BossPatterns/PatternContext.cs) | 패턴 실행에 필요한 컨텍스트 데이터 제공 | 박진우 |
| [ChargePattern.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/3.%20Attack/BossPatterns/ChargePattern.cs) | 돌진 패턴 (윈드업 후 충돌 시 데미지) | 박진우 |
| [ShellingPattern.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/3.%20Attack/BossPatterns/ShellingPattern.cs) | 공중 포격 패턴 | 박진우 |
| [ShootPattern.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/3.%20Attack/BossPatterns/ShootPattern.cs) | 연사 패턴 | 박진우 |
| [SummonPattern.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/3.%20Attack/BossPatterns/SummonPattern.cs) | 소환 패턴 | 박진우 |
| [TNTPattern.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/3.%20Attack/BossPatterns/TNTPattern.cs) | TNT 투척 패턴 | 박진우 |

### 2) Projectiles
| 스크립트 | 내용 | 기여자 |
|---|---|---|
| [BossProjectile.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/4.%20Projectiles/BossProjectile.cs) | 보스 전용 탄환 (속도, 방향, 충돌 처리) | 박진우 |
| [FirebombProjectile.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/4.%20Projectiles/FirebombProjectile.cs) | 폭탄 투사체 (타이머 기반 폭발) | 박진우 |
| [ShellingProjectile.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/4.%20Projectiles/ShellingProjectile.cs) | 포격 투사체 | 박진우 |
| [ProjectilePool.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/4.%20Projectiles/ProjectilePool.cs) | 투사체 풀링 시스템 | 박진우 |

### 3) Effects
| 스크립트 | 내용 | 기여자 |
|---|---|---|
| [ExplosionEffect.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/6.%20Effects/ExplosionEffect.cs) | 폭발 비주얼/사운드 | 박진우 |
| [Phase2TransitionEffects.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/6.%20Effects/Phase2TransitionEffects.cs) | 페이즈 전환 이펙트 | 박진우 |

### 4) Handlers
| 스크립트 | 내용 | 기여자 |
|---|---|---|
| [AnimationHandler.cs]([https://github.com/RanKa110/Rabbit/blob/main/Assets/02.%20Scripts/Boss/Handlers/AnimationHandler.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/2.%20Handlers/AnimationHandler.cs)) | 보스 애니메이션 상태 제어 | 박진우 |
| [AttackHandler.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/2.%20Handlers/AttackHandler.cs) | 공격 데이터 적용, 히트 이벤트 처리 | 박진우 |
| [BossAudioHandler.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/2.%20Handlers/BossAudioHandler.cs) | 보스 SFX 관리 | 박진우 |
| [BossEffectHandler.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/2.%20Handlers/BossEffectHandler.cs) | 잔상, 폭발, 먼지 등 이펙트 관리 | 박진우 |
| [BossPhaseTransitionHandler.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/2.%20Handlers/BossPhaseTransitionHandler.cs) | 페이즈 전환 연출 관리 | 박진우 |
| [BossStatHandler.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/2.%20Handlers/BossPhaseTransitionHandler.cs) | 보스 체력/게이지 관리 | 박진우 |
| [BossVisualHandler.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/2.%20Handlers/BossVisualHandler.cs) | 색상 변화/강조 효과 관리 | 박진우 |
| [ColliderFlipHandler.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/2.%20Handlers/ColliderFlipHandler.cs) | 방향 전환 시 콜라이더 보정 | 박진우 |
| [FacingHandler.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/2.%20Handlers/FacingHandler.cs) | 타겟 방향에 따른 스프라이트 방향 전환 관리 | 박진우 |
| [TargetingHandler.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/2.%20Handlers/TargetingHandler.cs) | 타겟 탐색 관리 | 박진우 |
| [MovementHandler.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/2.%20Handlers/MovementHandler.cs) | 이동 처리 | 박진우 |
| [SummonHandler.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/2.%20Handlers/SummonHandler.cs) | 소환 로직 관리 | 박진우 |

### 5) Roots
| 스크립트 | 내용 | 기여자 |
|---|---|---|
| [BossController.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/1.%20Controller/BossController.cs) | 보스 FSM의 메인 컨트롤러 | 박진우 |
| [BossStates.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/3.%20States(Boss%2CEnemy)/Boss/BossState.cs) | 보스 FSM 상태 정의/전이 | 박진우 |
| [AnimationEventRelay.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/AnimationEventRelay.cs) | 애니메이션 이벤트 전달 | 박진우 |
| [DamageReceiver.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/1.%20Boss/DamageReceiver.cs) | 보스 피격/체력 처리 | 박진우 |

---

## **적**

### 1) Roots
| 스크립트 | 설명 | 기여자 |
|---|---|---|
| [BaseEnemyController.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/2.%20Enemy/Controller/BaseEnemyController.cs) | 적 기본 동작 베이스 클래스 | 박진우 |
| [BaseEnemyState.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/3.%20States(Boss%2CEnemy)/Enemy/BaseEnemy/BaseEnemyState.cs) | 적 개별 상태 정의 | 박진우 |
| [BaseEnemyStates.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/3.%20States(Boss%2CEnemy)/Enemy/BaseEnemy/BaseEnemyStates.cs) | 적 FSM 묶음 | 박진우 |

### 2) Elite Enemy
| 스크립트 | 설명 | 기여자 |
|---|---|---|
| [EliteMeleeEnemyController.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/2.%20Enemy/Controller/Elite/EliteMeleeEnemyController.cs) | 정예 근접 적 AI 제어 | 박진우 |
| [EliteRangedEnemyController.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/2.%20Enemy/Controller/Elite/EliteRangedEnemyController.cs) | 정예 원거리 적 AI 제어 | 박진우 |
| [EliteShieldEnemyController.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/2.%20Enemy/Controller/Elite/EliteShieldEnemyController.cs) | 방패를 가진 정예 적 | 박진우 |

### 3) Normal Enemy
| 스크립트 | 설명 | 기여자 |
|---|---|---|
| [NormalMeleeController.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/2.%20Enemy/Controller/Normal/NormalMeleeController.cs) | 일반 근접 적 AI 제어 | 박진우 |
| [NormalRangedController.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/2.%20Enemy/Controller/Normal/NormalRangedController.cs) | 일반 원거리 적 AI 제어 | 박진우 |

### 4) Enemy Pool & Wave
| 스크립트 | 설명 | 기여자 |
|---|---|---|
| [EliteEnemyPool.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/2.%20Enemy/EliteEnemy/Pooling/EliteEnemyPool.cs) | 적 전용 오브젝트 풀 | 박진우 |
| [WaveManager.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/2.%20Enemy/EliteEnemy/Pooling/WaveManager.cs) | 웨이브 기반 적 스폰 시스템 | 박진우 |

### 5) Attacks
| 스크립트 | 설명 | 기여자 |
|---|---|---|
| [OnAttackEventHandler.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/2.%20Enemy/EliteEnemy/AnimationEvent/OnAttackEventHandler.cs) | 애니메이션 이벤트 기반 데미지 처리 | 박진우 |
| [EliteMeleeHitbox.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/2.%20Enemy/EliteEnemy/Attack/EliteMeleeHitbox.cs) | 정예 근접 적 히트박스 관리 | 박진우 |
| [EliteProjectile.cs](https://github.com/ParkJWoo/Portfolio_Public/blob/main/Project%20Rabbit/CodeSamples/2.%20Enemy/EliteEnemy/Attack/Projectile/EliteProjectile.cs) | 정예 원거리 적 투사체 관리 | 박진우 |

---

# 4. 사용 에셋
- [UI User Interface Pack - Cyber by ToffeeCraft](https://toffeecraft.itch.io/ui-user-interface-pack-cyber)
- [Smoke n Dust 04 by pimen](https://pimen.itch.io/smoke-n-dust-04)
- [Battle VFX: Hit Spark by pimen](https://pimen.itch.io/battle-vfx-hit-spark)
- [Smoke FX by jasontomlee](https://jasontomlee.itch.io/smoke-fx)
- [Residential Area Bosses Pixel Art - CraftPix.net](https://craftpix.net/product/residential-area-bosses-pixel-art/CraftPix.net)
- [Bombs and Explosions Pixel Art Set - CraftPix.net](https://craftpix.net/product/bombs-and-explosions-pixel-art-set/)
- [Residential Area Tileset Pixel Art - CraftPix.net](https://craftpix.net/product/residential-area-tileset-pixel-art/)
