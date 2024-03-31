using UnityEngine;

public static class StaticStorage {
	/* 
	 * Статический класс для передачи данных между сценами
	 */

	// статическое поле для хранения аудио
	public static AudioClip audioClip { set; get; }
}
