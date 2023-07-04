using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject LevelCompletedPanel;
    private GameObject hatStack;
    private Transform hatStackTransform;
    public static int currentLevelIndex;
    public int childCount;
    
    void Awake()
    {
        currentLevelIndex = PlayerPrefs.GetInt("currentLevelIndex", 1);
    }
    void Start()
    {
        Time.timeScale = 1;
        hatStack = GameObject.FindGameObjectWithTag("Hatstack");
        hatStackTransform = hatStack.GetComponent<Transform>();
    }

    void Update()
    {
        childCount = hatStackTransform.childCount;

        if (LevelCompletedPanel.activeSelf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlayerPrefs.SetInt("currentLevelIndex", currentLevelIndex + 1);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    public void EndGame()
    {
        StartCoroutine(DelayedEndGame());
    }

    IEnumerator DelayedEndGame()
    {
        yield return new WaitForSeconds(1f);

        Time.timeScale = 0;
        LevelCompletedPanel.SetActive(true);
    }
}
