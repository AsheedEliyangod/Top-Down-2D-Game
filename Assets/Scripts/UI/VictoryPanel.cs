using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public static VictoryManager Instance;

    [SerializeField]
    private GameObject victoryPanel;

    private void Awake()
    {
        Instance = this;

        victoryPanel.SetActive(false);
    }

    public void ShowVictory()
    {
        victoryPanel.SetActive(true);

        Time.timeScale = 0f;
    }
}