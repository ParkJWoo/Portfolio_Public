using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UIImg = UnityEngine.UI.Image;

using UnityEngineInternal;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Card firstCard;
    public Card secondCard;
    public Card etcCard;
    public Board board;


    AudioSource audioSource;
    public AudioClip clip;
    public AudioClip wrongClip;

    public GameObject clearPanel; // 최종 스테이지 클리어 시 나오는 팀원 정보 판넬
    public GameObject nextPanel;  // 최종 스테이지가 아닌 스테이지 클리어 시 나오는 다음 스테이지 이동 판넬
    public GameObject failPanel;  // 제한 시간이 지날 시 나오는 실패 판넬
    public GameObject hiddenPanel; // 히든 스테이지 클리어 시 나오는 판넬
    public GameObject infinityPanel;
    public GameObject optionPanel;
    public GameObject optionButton; //우측 상단 옵션버튼

    public GameObject hiddenBtn;
    public GameObject hardBtn;
    public UIImg st2Btnimg;
    public UIImg st3Btnimg;

    public Text timeTxt;
    public float time = 60.0f;

    public Text nowScoreTxt;
    public Text bestScoreTxt;
    public Text cbestScoreTxt;

    public bool isGamePlaying = false; //게임 시작 여부 판단

    public int cardCount = 16;

    // 스테이지 클리어 여부 판단.
    public int Clear = 0;

    public bool isGhost = false;

    public int stage = 0;
    public int mode = 0;
    public int score = 0;
    public int bestScore = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (PlayerPrefs.HasKey("Clear"))
        {
            Clear = PlayerPrefs.GetInt("Clear");
        }

        if (PlayerPrefs.HasKey("bestScore"))
        {
            bestScore = PlayerPrefs.GetInt("bestScore");
        }

        board.gameObject.SetActive(false);
        timeTxt.gameObject.SetActive(false);
        optionButton.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        audioSource = GetComponent<AudioSource>();
        stage = 0;
        mode = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Clear > 2)                                   //기본모드 모두 클리어후 무한모드 최고점수보임
        {
            cbestScoreTxt.text = "무한모드 최고점수 : " + bestScore.ToString();
        }
        

        Text hardbtnText = hardBtn.GetComponentInChildren<Text>();
        if (Clear >= 3)
        {
            hardbtnText.text = "무한 모드";
        }
        else
        {
            hardbtnText.text = "무한 모드(잠김)";
        }

        if (mode == 1)
        {
            if (Clear < 3)                                      //쉬움모드를 모두 클리어하지않았을때
            {
                st2Btnimg.enabled = true;                       //잠금표시 보이게 초기화
                st3Btnimg.enabled = true;
            }
            else
            {
                st2Btnimg.enabled = false;                       //모두 클리어했을때 열쇠이미지 없음
                st3Btnimg.enabled = false;
            }
                switch (Clear)
                {
                    case 0:
                        break;
                    case 1:
                        st2Btnimg.enabled = false;
                        break;
                    case 2:
                        st2Btnimg.enabled = false;
                        st3Btnimg.enabled = false;
                        break;
                    case 3:
                        st2Btnimg.enabled = false;
                        st3Btnimg.enabled = false;
                        break;

                }
        }
        /*else if (level == 2)
        {
            if (Clear < 6)                                      //어려움모드를 모두 클리어하지않았을때
            {
                st2Btnimg.enabled = true;                       //잠금표시 초기화
                st3Btnimg.enabled = true;
            }
            switch (Clear)
            {
                case 4:
                    st2Btnimg.enabled = false;
                    break;
                case 5:
                    st2Btnimg.enabled = false;
                    st3Btnimg.enabled = false;
                    break;

            }
        }*/


        if (bestScore >= 20f)                        //히든버튼 무한모드 점수10이상시 보임
        {
            hiddenBtn.SetActive(true);
        }

        else
        {
            hiddenBtn.SetActive(false);
        }

        //카드게임 시작시
        if (!isGamePlaying)
        {
            return;
        }

        //// BGM관련 순서 조정
        //if (stage == 4 && !AudioManager.instance.IsHellMode())
        //{
        //    AudioManager.instance.SwitchMusic(true);
        //    AudioManager.instance.StopTickSfx();
        //}

        ////무한 모드 전용 BGM 재생
        //if (mode == 2 && AudioManager.instance.IsHellMode())
        //{
        //    AudioManager.instance.SwitchMusic(false, true);
        //    AudioManager.instance.StopTickSfx();
        //}

        // 모드별 BGM 전환
        if (stage == 4 && AudioManager.instance.audioSource.clip != AudioManager.instance.hellClip)
        {
            AudioManager.instance.SwitchMusic(true);
        }

        else if (mode == 2 && AudioManager.instance.audioSource.clip != AudioManager.instance.infinityClip)
        {
            AudioManager.instance.SwitchMusic(false, true);
        }

        time -= Time.deltaTime;
        timeTxt.text = time.ToString("N2");

        if (AudioManager.instance.audioSource.pitch == 1.0f && AudioManager.instance.SFXSource.isPlaying)
        {
            AudioManager.instance.StopTickSfx();
        }

        if (time <= 20.0f)
        {
            if (AudioManager.instance.audioSource.pitch != 1.5f)
            {
                AudioManager.instance.SetSpeed(1.5f);
            }

            if (!AudioManager.instance.SFXSource.isPlaying || AudioManager.instance.SFXSource.clip != AudioManager.instance.tickSfx)
            {
                AudioManager.instance.PlayTickSfx();
            }

            //if ((mode == 2 || stage == 4))
            //{
            //    if (!AudioManager.instance.SFXSource.isPlaying || AudioManager.instance.SFXSource.clip != AudioManager.instance.tickSfx)
            //    {
            //        AudioManager.instance.PlayTickSfx();
            //    }
            //}
        }

        if (mode == 1 && time <= 0.0f && cardCount != 0)    //  실패 UI 생성 조건
        {
            failPanel.SetActive(true);
            Time.timeScale = 1.0f;
            AudioManager.instance.ResetSpeed();
            AudioManager.instance.StopTickSfx();
            isGamePlaying = false;
            board.gameObject.SetActive(false);

            time = 0.0f;
            timeTxt.text = time.ToString("N2");
        }

        if (cardCount <= 0 && mode == 1)
        {//게임 끝난 조건 확인
            isGamePlaying = false;
            AudioManager.instance.ResetSpeed();
            AudioManager.instance.StopTickSfx();

            board.gameObject.SetActive(false);
        }

        if ((cardCount == 0 && mode == 1) || (time <= 0.0f && mode == 2))
        {
            timeTxt.gameObject.SetActive(false);

            GameClear(stage,score);

            time = 0.0f;
            timeTxt.text = time.ToString("N2");

            Delete();
        }

        if (AudioManager.instance.audioSource.pitch == 1.0f && AudioManager.instance.SFXSource.isPlaying)
        {
            AudioManager.instance.StopTickSfx();
        }

        Option();

        if (board.gameObject.activeSelf == true)
        {
            optionButton.gameObject.SetActive(true);
        }
        else
        {
            optionButton.gameObject.SetActive(false);
        }

    }

    public void Matched()
    {
        if (firstCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(clip);

            if (secondCard.idx == 8) // 악령 카드가 매치되었을 때
            {
                isGhost = true; //isGhost true -> 카드 뒤집기 불가능
                firstCard.DestroyCard(0f);

                secondCard.shouldTurnOffGhost = true; // 앞에 firstcard가 파괴되었을 때 isGhost가 false가 되지 않도록 방지하는 변수
                secondCard.front.GetComponent<Animator>().SetBool("isOpen", true); // 악령 확대 애니 실행
                secondCard.transform.position = new Vector2(0, 0); //악령 중앙으로 이동시킴
                secondCard.DestroyCard(3f); //3초 뒤 카드 파괴
            }
            else
            {
                firstCard.DestroyCard(1f);
                secondCard.DestroyCard(1f);
            }

            cardCount -= 2;

            
            if (mode == 2)
            {
                score++;
                if (cardCount == 0)
                {
                    Invoke("ReBoard", 1f);
                }
            }
        }
        else
        {
            audioSource.PlayOneShot(wrongClip);
            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        firstCard = null;
        secondCard = null;
    }

    public void GameClear(int _stage,int _score) // 게임이 끝났을 경우
    {
        //nextPanel.SetActive(true); //  카드를 모두 맞췄을 시, 클리어 판넬 생성
        AudioManager.instance.ResetSpeed();
        AudioManager.instance.StopTickSfx();

        int pastBestScore = PlayerPrefs.GetInt("bestScore", bestScore); // 무한모드 최고점수
        int pastStage = PlayerPrefs.GetInt("Clear", Clear); // 이전 최고 스테이지
        Time.timeScale = 0.0f;

        //if (stage >= 0 && stage <= 2)
        //{
        //    nextPanel.SetActive(true);
        //}

        //else if (stage == 3)
        //{
        //    clearPanel.SetActive(true);
        //}

        if (stage == 4)
        {
            hiddenPanel.SetActive(true);
        }

        if (mode == 1)
        {
            Clear = _stage;

            //if (stage == 4)
            //{
            //    clearPanel.SetActive(true);
            //}

            if (stage >= 0 && stage <= 2)
            {
                nextPanel.SetActive(true);
            }

            else if (stage == 3)
            {
                clearPanel.SetActive(true);
            }

        }
        else if (mode == 2)
        {
            Clear = _stage + 3;
            board.OnDisable();

            infinityPanel.SetActive(true);

            if (score >= pastBestScore)
            {
                PlayerPrefs.SetInt("bestScore", score);  
                PlayerPrefs.Save(); 

                bestScoreTxt.text = score.ToString();
                nowScoreTxt.text = score.ToString();
            }

            else
            {
                bestScoreTxt.text = bestScore.ToString();
                nowScoreTxt.text = score.ToString();
            }

            //score = 0;
        }

        if (pastStage < _stage)
        {
            PlayerPrefs.SetInt("Clear", Clear);
        }
    }

    public void Delete()                                                //  스테이지 종료 후, 기존의 스테이지 데이터 초기화 → 다음 스테이지의 데이터를 새로 받아오기 위한 정리 함수
    {
       
        cardCount = 0;
        Time.timeScale = 1f;
        time = 0.0f;
        timeTxt.text = time.ToString("N2");
        if (mode == 2)
        {
            //score = 0;  // 무한모드 점수 초기화
        }
    }

    private void ReBoard()
    {
        board.Start();
    }

    private void Option()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && board.gameObject.activeSelf == true)
        {
            if (optionPanel.activeSelf == false)
            {
                optionPanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else if (optionPanel.activeSelf == true)
            {
                optionPanel.SetActive(false);
                Time.timeScale = 1f;
            }

        }
    }
}
