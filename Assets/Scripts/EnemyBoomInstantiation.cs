using UnityEngine;

public class EnemyBoomInstantiation : MonoBehaviour {
	/*
	 * Скрипт для создания объектов Взрыв
	 */

	// префаб объекта Взрыв
	public GameObject prefab;

	public void InstantiateEnemyBoom(Vector3 positionBoom) {

		// создаем объект Взрыва на основе префаба
		GameObject instanceEnemyBoom = (GameObject)Instantiate(prefab);

		// задаем позицию объекта Взрыв
		// позицию получаем как параметр - конечная позиция Противника при движении
		instanceEnemyBoom.transform.position = positionBoom;

		// задаем родителя - пустой объект содержащий все Взрывы
		// и задаем имя объекта
		instanceEnemyBoom.transform.SetParent(this.transform);
		instanceEnemyBoom.name = "enemyBoom";
	}
}
