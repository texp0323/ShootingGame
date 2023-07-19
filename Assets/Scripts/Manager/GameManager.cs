using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [SerializeField] PlayerInfo playerInfo;
    [SerializeField] UIandAudioManager uiManager;

    [Header("BGM")]
    [SerializeField] AudioSource bgmAds;
    [SerializeField] AudioClip[] bgmClips;

    [Header("UI")]
    [SerializeField] private Text stageNumText;
    [SerializeField] private GameObject stageClearText;
    private Text stageClearScoreText;
    [Header("MAP")]
    [SerializeField] private GameObject[] mapParticle;

    private int stageNum = 1;

    private int endScore;

    private void Start()
    {
        stageClearScoreText = stageClearText.transform.GetChild(0).GetComponent<Text>();

        stageNumText.text = stageNum.ToString();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F8))
        {
            playerInfo.LevelUp();
        }
    }

    public void ChangeBgm(int bgmNum)
    {
        StartCoroutine(ChangeEnd(bgmNum));
    }
    IEnumerator ChangeEnd(int bgmNum)
    {
        yield return new WaitForSeconds(3);
        bgmAds.clip = bgmClips[bgmNum];
        bgmAds.Stop();
        bgmAds.Play();
    }

    public void NextStage()
    {
        stageNum++;
        if(stageNum < 3)
        {
            stageNumText.text = stageNum.ToString();
            mapParticle[0].SetActive(false);
            mapParticle[1].SetActive(true);
        }
        stageClearScoreText.text = ScoreManager.score.ToString();
        stageClearText.SetActive(true);
        playerInfo.ResetPlayer();
        playerInfo.UseInvincibility(10);
        Invoke(nameof(StageClearTextEnd), 5);
    }

    public void BeforeStage()
    {
        stageNum--;
        stageNumText.text = stageNum.ToString();
        mapParticle[1].SetActive(false);
        mapParticle[0].SetActive(true);
        playerInfo.ResetPlayer();
    }

    private void StageClearTextEnd()
    {
        stageClearText.SetActive(false);
        if (stageNum > 2)
        {
            GameEnd(false);
        }
    }
    public void GameEnd(bool death)
    {
        uiManager.GameClear(death);
    }
}
