using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Models;
using SFB;

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

    public void ClickButtonStart() {
        StartGame(audioClips[dropDown.value]);
    }

    private void StartGame(AudioClip clip) {
        // Сохранение выбранного аудио в статическом сторэдже
        StaticStorage.AudioClip = clip;
        
        GameManager.Instance.Reset();

        // Открытие сцены с игровым процессом
        SceneManager.LoadScene("VisualizationScene", LoadSceneMode.Single);
    }

    public async void ClickButtonTest() {
        var path = StandaloneFileBrowser.OpenFilePanel("Select mp3 song", "", "mp3", false)[0];

        if (path == "") {
            Debug.Log("Path not selected");

            return;
        }

        var clip = await LoadClip($"file://{path}");

        StartGame(clip);
    }

    async Task<AudioClip> LoadClip(string path) {
        AudioClip clip = null;

        using (UnityWebRequest uwr = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.MPEG)) {
            uwr.SendWebRequest();

            try {
                while (!uwr.isDone) {
                    await Task.Delay(5);
                }

                if (uwr.result == UnityWebRequest.Result.ProtocolError) {
                    Debug.Log($"{uwr.error}");
                }
                else {
                    clip = DownloadHandlerAudioClip.GetContent(uwr);
                }
            }
            catch (Exception err) {
                Debug.Log($"{err.Message}, {err.StackTrace}");
            }
        }

        return clip;
    }

    /// <summary>
    /// Закрытие приложения
    /// </summary>
    public void ClickButtonExit() {
        Application.Quit();
    }
}