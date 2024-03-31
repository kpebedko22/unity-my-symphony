using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
	/*
	 * Скрипт для главного меню
	 * для работы с UI - выбор аудио из списка
	 * запуск сцены с игрой
	 * или выхода из игры
	 */

	// массив аудио (заполняется через Unity)
	public AudioClip[] audioClips;

	// объект Выпадающий список опций (задается через Unity)
	public Dropdown dropDown;

	void Start () {
		Cursor.visible = true;

		// очищаем выпадающий список с опциями
		dropDown.ClearOptions();

		// создаем список опций
		List<Dropdown.OptionData> audioItems = new List<Dropdown.OptionData>();

		// для каждого объекта аудио из массива
		// создаем опцию для выпадающего меню
		foreach(var audio in audioClips) {
			var audioOption = new Dropdown.OptionData(audio.name);
			audioItems.Add(audioOption);
		}

		// добавляем опции в выпадающий список
		dropDown.AddOptions(audioItems);
	}
	
	public void ClickButtonStart() {
		// сохраняем в статическом классе выбранное аудио
		StaticStorage.audioClip = audioClips[dropDown.value];

		// открыть сцену с игровым процессом
		SceneManager.LoadScene("VisualizationScene", LoadSceneMode.Single);
	}

	public void ClickButtonExit() {
		// закрыть приложение
		Application.Quit();
	}
}
