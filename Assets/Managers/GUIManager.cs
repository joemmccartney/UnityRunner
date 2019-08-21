using UnityEngine;

public class GUIManager : MonoBehaviour {

    public GUIText boostsText, distanceText, gameOverText, instructionsText, runnerText;

    private static GUIManager instance;

    private void Start ()
    {
        instance = this;
        GameEventsManager.GameStart += GameStart;
        GameEventsManager.GameOver += GameOver;
        gameOverText.enabled = false;
    }

    private void Update ()
    {
        if (Input.GetButtonDown("Jump"))
        {
            GameEventsManager.TriggerGameStart();
        }
    }

    private void GameStart ()
    {
        gameOverText.enabled = false;
        instructionsText.enabled = false;
        runnerText.enabled = false;
        enabled = false;
    }

    private void GameOver ()
    {
        gameOverText.enabled = true;
        instructionsText.enabled = true;
        enabled = true;
    }

    public static void SetBoosts(int boosts)
    {
        instance.boostsText.text = boosts.ToString();
    }

    public static void SetDistance(float distance)
    {
        instance.distanceText.text = distance.ToString("f0");
    }
}
