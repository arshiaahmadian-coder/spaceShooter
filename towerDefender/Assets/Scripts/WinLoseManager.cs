using System.Collections.Generic;
using UnityEngine;

public class WinLoseManager : MonoBehaviour
{
    // Singleton
    public static WinLoseManager instance;
    void Awake() { instance = this; }

    private bool canWin = true;

    [SerializeField] List<GameObject> enemiesList;
    [SerializeField] int winLoseInterwal;

    [SerializeField] GameObject controllerUI;
    [SerializeField] GameObject loseDialogUI;
    [SerializeField] GameObject winDialogUI;

    private void Win()
    {
        if (!canWin) return;
        GameManager.instance.IncreaseLevelIndex();
        controllerUI.SetActive(false);
        winDialogUI.SetActive(true);
    }

    public void CallLose()
    {
        canWin = false;
        Invoke(nameof(Lose), winLoseInterwal);
    }

    private void Lose()
    {
        controllerUI.SetActive(false);
        loseDialogUI.SetActive(true);
    }

    public void DeleteEnemyInList(GameObject gameObject)
    {
        enemiesList.Remove(gameObject);

        // check enemies list | there is no enemies left = win
        if (enemiesList.Count == 0) Invoke(nameof(Win), winLoseInterwal);
    }
}
