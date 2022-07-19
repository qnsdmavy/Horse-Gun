using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Text;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // AI target 
    public Transform[] target;

    // 메뉴 오브젝트
    [Header("Menu")]
    public GameObject startMenu;
    public GameObject ui;
    public GameObject finishMenu;

    // 텍스트
    [Header("Text")]
    public TextMeshProUGUI countText;
    public TextMeshProUGUI curTimeText;
    public TextMeshProUGUI curTargetText;

    // 시간 변수
    float curTime;
    int hitTarget;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // 시작 버튼
    public void StartButton()
    {
        startMenu.SetActive(false);
        StartCoroutine("StartCount");
    }

    // 종료 버튼
    public void EndButton()
    {
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    // 카운트 시작
    IEnumerator StartCount()
    {
        ui.SetActive(true);

        transform.GetChild(0).gameObject.SetActive(true);
        countText.text = "3";
        countText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        countText.gameObject.SetActive(false);
        countText.text = "2";
        countText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        countText.gameObject.SetActive(false);
        countText.text = "1";
        countText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        countText.gameObject.SetActive(false);
        countText.text = "GO!";
        countText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1);
        countText.gameObject.SetActive(false);

        GameObject.Find("player").GetComponent<PlayerController>().StartControll();
        GameObject.Find("AI1").GetComponent<AI>().StartAI();
        GameObject.Find("AI2").GetComponent<AI>().StartAI();
        StartCoroutine("Timer");
    }

    // 시간 측정
    IEnumerator Timer()
    {
        while (true)
        {
            curTime += Time.deltaTime;

            curTimeText.text = string.Format("{0:00}:{1:00.00}", (int)(curTime / 60 % 60), curTime % 60);
            yield return null;
        }
    }

    // 결승선 도달시
    public void finish()
    {
        StopCoroutine("Timer");
        curTime += hitTarget;
        finishMenu.SetActive(true);
        insert(curTime);

    }

    // DB에 저장
    public void insert(float time)
    {
        curTime = 10000 - curTime;
        string address = "http://127.0.0.1/insert.php";
        WWWForm Form = new WWWForm();
        Form.AddField("Score",(int)curTime);
        WWW wwwURL = new WWW(address, Form);
    }

    // 과녁 hit
    public void HitTarget()
    {
        hitTarget += 100;
        curTargetText.text = string.Format("{0:00}", hitTarget);
    }
}
