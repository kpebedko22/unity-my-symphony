using Models;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Скрипт для контроля счетчиков и завершения игры
/// </summary>
public class GameController : MonoBehaviour {
    // текстовые поля для вывода счетчиков
    [SerializeField] private Text textTotalEnemies;
    [SerializeField] private Text textEnemiesBumped;
    [SerializeField] private Text textScore;
    [SerializeField] private Text textPoints;

    // объект завершения игры (меню)
    public GameObject endGameObject;

    private void Start() {
        // отключаем отобрежение меню завершения игры (на всякий случай)
        endGameObject.SetActive(false);

        GameManager.Instance.TotalEnemiesUpdate += delegate(int value) { textTotalEnemies.text = value.ToString(); };
        GameManager.Instance.EnemiesBumpedUpdate += delegate(int value) { textEnemiesBumped.text = value.ToString(); };
        GameManager.Instance.GameOverUpdate += delegate(int? score, int points)
        {
            // включаем отображение курсора
            Cursor.visible = true;

            textScore.text = score == null
                ? "No enemies were spawned."
                : "Your score is " + score + "%";

            textPoints.text = $"You got {points.ToString()} extra points.";

            endGameObject.SetActive(true);
        };
    }

    private void Update() {
        // если была нажата клавиша Escape - выход в главное меню
        if (Input.GetKeyDown(KeyCode.Escape)) {
            QuitMainMenu();
        }
    }

    /// <summary>
    /// Выход в главное меню
    /// </summary>
    private void QuitMainMenu() {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}