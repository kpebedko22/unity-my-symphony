using UnityEngine;

public class Bumping : MonoBehaviour {
	/*
	 * Скрипт для проверки столкновения двух объектов
	 * Столкнувшиеся объекты проходят скозь друг друга
	 */

	// переменная отвечающая за принадлежность скрипта объекту Героя
	public bool isMyHero;

	void OnTriggerEnter2D(Collider2D other) {

		// если объект Герой столкнулся с кем-то, то засчитываем
		if (isMyHero) GameController.enemiesBumped++;
	}
}
