using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Скрипт для главного меню:
/// - выбор аудио из списка;
/// - запуск сцены с игрой
/// - выход из игры
/// </summary>
public class MainMenuController : MonoBehaviour {
    /// <summary>
    /// Массив аудио (заполняется через Unity)
    /// </summary>
    public AudioClip[] audioClips;

    /// <summary>
    /// Выпадающий список опций (задается через Unity)
    /// </summary>
    public Dropdown dropDown;

    private void Start() {
        Cursor.visible = true;

        // Добавляем опции в выпадающий список
        dropDown.options = new List<Dropdown.OptionData>(
            audioClips.Select(x => new Dropdown.OptionData(x.name))
        );
    }

    /// <summary>
    /// Старт игрового процесса
    /// </summary>
    public void ClickButtonStart() {
        // Сохранение выбранного аудио в статическом сторэдже
        StaticStorage.AudioClip = audioClips[dropDown.value];

        // Открытие сцены с игровым процессом
        SceneManager.LoadScene("VisualizationScene", LoadSceneMode.Single);
    }

    /// <summary>
    /// Закрытие приложения
    /// </summary>
    public void ClickButtonExit() {
        Application.Quit();
    }
}