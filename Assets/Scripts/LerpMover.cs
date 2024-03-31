using UnityEngine;

public class LerpMover : MonoBehaviour {
	/*
	 * Скрипт для движения от startPosition до endPosition
	 * используется для объектов Противник
	 */

	// начальная и конечная позиции движения
	private Vector2 startPosition;
	private Vector2 endPosition;

	// прогресс и шаг
	private float progress;
	public float step;

	// объект, где создаются объекты Взрыв (анимация завершения движения)
	public GameObject enemyBoomContainer;

	void Start () {

		// при инициализации объекта Противник
		// задаем начальную и конечную позиции движения
		startPosition = transform.position;
		endPosition = -startPosition;
	}
	
	void FixedUpdate () {

		// вычисляем новое положение объекта
		transform.position = Vector2.Lerp(startPosition, endPosition, progress);
		progress += step;

		// если объект дошел до конечной позиции 
		if (transform.position.x == endPosition.x && transform.position.y == endPosition.y) {
			// создаем объект Взрыв - анимацию
			enemyBoomContainer.GetComponent<EnemyBoomInstantiation>().InstantiateEnemyBoom(transform.position);

			// уничтожаем объект
			Destroy(transform.gameObject);
		}
	}
}
