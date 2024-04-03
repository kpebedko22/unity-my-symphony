using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Скрипт для контроля счетчиков и завершения игры
/// </summary>
public class GameController : MonoBehaviour {
    // счетчики кол-ва противников
    public static int totalEnemies;
    public static int enemiesBumped;

    // текстовые поля для вывода счетчиков
    public Text textTotalEnemies;
    public Text textEnemiesBumped;
    public Text textScore;

    // объект завершения игры (меню)
    public GameObject endGameObject;

    // объект в котором хранятся кружочки
    public GameObject audioCirclesCenter;

    // переменная - завершилась ли игра
    public static bool gameIsOver;

    private void Start() {
        // на старте устанавливаем счетчики в ноль и gameIsOver = false
        totalEnemies = 0;
        enemiesBumped = 0;
        gameIsOver = false;

        // отключаем отобрежение меню завершения игры (на всякий случай)
        endGameObject.SetActive(false);
    }

    private void Update() {
        // если игра закончена - скип
        if (gameIsOver) {
            return;
        }
        
        // обновляем текстовые поля счетчиков
        textTotalEnemies.text = totalEnemies.ToString();
        textEnemiesBumped.text = enemiesBumped.ToString();

        // если была нажата клавиша Escape - выход в главное меню
        if (Input.GetKeyDown(KeyCode.Escape)) {
            QuitMainMenu();
        }
    }

    /// <summary>
    /// Функция для завершения игры
    /// </summary>
    public void EndGame() {
        // включаем отображение курсора
        Cursor.visible = true;

        // устанавливаем флаг окончания игры в true
        gameIsOver = true;

        // вычисляем счет
        // предполагается, что нужно задеть как можно меньше Врагов
        // поэтому из общего числа врагов вычитаем кол-во врагов, с которыми столкнулись
        // умножаем на 100 и делим на общее число врагов
        // (да, можно получить отрицательное число, т.к. одного врага можно задеть несколько раз)
        if (totalEnemies != 0) {
            int score = (totalEnemies - enemiesBumped) * 100 / totalEnemies;
            // выводим текст и отображаем меню окончания игры
            textScore.text = "Your score is " + score + "%";
        }
        else {
            textScore.text = "No enemies were spawned.";
        }

        endGameObject.SetActive(true);
    }

    /// <summary>
    /// Выход в главное меню
    /// </summary>
    public void QuitMainMenu() {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}