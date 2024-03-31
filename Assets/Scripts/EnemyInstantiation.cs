using UnityEngine;

public class EnemyInstantiation : MonoBehaviour {
	/*
	 * Скрипт для создания объекта Противник
	 */

	// префаб объекта Противник
	public GameObject prefab;

	// объект, в котором создаются объекты Взрыв
	public GameObject enemyBoomContainer;

	public void InstantiateEnemy(int band, Transform shooter) {
		
		// инкриментируем количество созданных объектов Противников
		GameController.totalEnemies++;

		// создаем объект Противника на основе префаба
		GameObject instanceEnemy = (GameObject)Instantiate(prefab);

		// задаем начальную позицию объекта Противник, равной позиции Стрелка
		instanceEnemy.transform.position = shooter.position;

		// поворачиваем объект Противник, чтобы было красиво 
		// (в какую сторону будет смотреть острый угол)
		instanceEnemy.transform.up = -shooter.position;

		// задаем родителя - пустой объект содержащий всех Противников
		// и задаем имя объекта
		instanceEnemy.transform.SetParent(this.transform);
		instanceEnemy.name = "enemy" + band;

		// для объекта в скрипт движения устанавливаем контейнер, в котором будем создавать объект Взрыв
		instanceEnemy.GetComponent<LerpMover>().enemyBoomContainer = enemyBoomContainer;
	}
}
