using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBg : MonoBehaviour
{
    public float scrollSpeed = 0.4f;

    public Vector3 resetPos = new Vector3(2.34f, 2.34f);   // 반복되는 경계 위치
    public Vector3 startPos = new Vector3(-3f, -3f); // 시작 위치

    public float fadeSpeed = 2.0f; // 알파 변화 속도
    public float cycleSpeed = 4.0f;

    private SpriteRenderer sr;
    private float alphaTime = 0f;

    public bool fadeActive = false;
    void Start()
    {
        transform.position = startPos;
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        // 오른쪽 위로 이동 (X, Y 둘 다 양수)
        transform.Translate(new Vector3(1, 1, 0).normalized * scrollSpeed * Time.deltaTime);

        // 리셋 위치보다 크면 되돌리기
        if (transform.position.x >= resetPos.x && transform.position.y >= resetPos.y)
        {
            transform.position = new Vector3(startPos.x, startPos.y, transform.position.z);
        }
        if (fadeActive)
        {
            alphaTime += Time.deltaTime * cycleSpeed;
            float alpha = 0.75f + Mathf.Sin(alphaTime) * 0.25f * fadeSpeed;        //Mathf.Sin(alphaTime)은 -1~1 그래프
            Color c = sr.color;
            c.a = Mathf.Clamp(alpha, 0f, 1f);
            sr.color = c;
        }
    }


    public void ActivateFade()
    {
        fadeActive = true;
    }
}