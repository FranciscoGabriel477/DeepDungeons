using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance{get; private set;}
    public LevelData levelData;
    public PlayerController player;
    int currentLevel;
    bool inPauseByTrapCoroutine=false;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance=this;
            DontDestroyOnLoad(gameObject);
        }
    } 
    public void Play(int choice)
    {
        currentLevel=0;
        switch (choice)
        {
            case 1:
                SceneManager.LoadScene(ScenesNames.PreloadSoldier);
            break;
            case 2:
                SceneManager.LoadScene(ScenesNames.PreloadArcher);
            break;
        }
        PassLevel();
    }
    public void PassLevel()
    {   Debug.Log(levelData.levels.Count);
        if (currentLevel >= levelData.levels.Count)
        {
            PlayAgain();
            return;
        }
        LevelData.LevelInfo levelSelected=levelData.levels[currentLevel].levelList[UnityEngine.Random.Range(0,levelData.levels[currentLevel].levelList.Count)];
        currentLevel++;
        StartCoroutine(LoadLevelAndMovePlayer(levelSelected));
    }
    IEnumerator LoadLevelAndMovePlayer(LevelData.LevelInfo info)
    {
        AsyncOperation operacao = SceneManager.LoadSceneAsync(info.levelName);
        while (!operacao.isDone)
        {
            yield return null;
        }

        if (player != null)
        {
            player.NewLevel(info.levelStart);
        }
        else
        {
            Debug.LogError("O Player sumiu ou foi destruído durante a transição");
        }
    }
    public void PauseGame()
    {
       Time.timeScale=0; 
    }

    public void DespauseGame()
    {
        Time.timeScale=1;
    }

    public void PlayerHittedByTrap()
    {
        StartCoroutine(PlayerRecoveryByTrapHit());
    }
    protected IEnumerator PlayerRecoveryByTrapHit()
    {
        float timeToFade=0.5f;
        float timeInTotalFade=1f;
            PauseGame();
            inPauseByTrapCoroutine=true;
            float actualTime=0f;
            while (actualTime <= timeToFade)
            {
                actualTime+=Time.unscaledDeltaTime;
                HUDManager.instance.ChangeScreenFade(Mathf.InverseLerp(0,timeToFade,actualTime));
                yield return null;                
            }
            HUDManager.instance.ChangeScreenFade(1f);
            player.HittedByTrap();
            yield return new WaitForSecondsRealtime(timeInTotalFade);
            actualTime=0f;
            while (actualTime <= timeToFade)
            {
                actualTime+=Time.unscaledDeltaTime;
                HUDManager.instance.ChangeScreenFade(1-Mathf.InverseLerp(0,timeToFade,actualTime));
                yield return null;
            }
            HUDManager.instance.ChangeScreenFade(0f);
            yield return new WaitForSecondsRealtime(1f);
            DespauseGame();
            inPauseByTrapCoroutine=false;
    }
    public void DestroyAllnDontDestroy()
    {
        Scene dontDestroyScene = gameObject.scene;
        
        GameObject[] rootObjs = dontDestroyScene.GetRootGameObjects();
        if (rootObjs== null)
        {
            Debug.Log("PORQUUEEE");
        }
        foreach (GameObject obj in rootObjs)
        {
            if (obj == gameObject) continue; 
            Destroy(obj);
        }
        
        Debug.Log("Todos os objetos do DontDestroyOnLoad foram limpos");
    }

    public void GameOver()
    {
        StartCoroutine(GameOverCoroutine());
    }
    private IEnumerator GameOverCoroutine()
    {
        float timeToFade=2f;
        float actualTime=0f;
        while (actualTime <= timeToFade)
        {
            actualTime+=Time.unscaledDeltaTime;
            HUDManager.instance.ChangeScreenGameOverFade(Mathf.InverseLerp(0,2*timeToFade,actualTime));
            yield return null;                
        }
        HUDManager.instance.gameOverButton.SetActive(true);            
    }
    public void PlayAgain()
    {
        DestroyAllnDontDestroy();
        SceneManager.LoadScene(ScenesNames.MainMenu);
    }
    public void Continue()
    {
        if (!inPauseByTrapCoroutine)
        {
            DespauseGame();
        }
    }
}
