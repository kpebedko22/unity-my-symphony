using UnityEngine;

public class CircleMovement : MonoBehaviour {
	/*
	 * Скрипт для движения объектов по кругу вокруг центра
	 * с заданным радиусом и скоростью
	 */

	public Transform center;

	[SerializeField]
	float radius = 2f, angularSpeed = 2f;

	public float angle = 0f;

	float positionX, positionY;

	void Update () {
		// вычисляем новые координаты X, Y для объекта
		positionX = center.position.x + Mathf.Cos(angle) * radius;
		positionY = center.position.y + Mathf.Sin(angle) * radius;

		// задаем позицию объекта по вычисленным координатам
		transform.position = new Vector2(positionX, positionY);

		// увеличиваем текущий угол
		angle += Time.deltaTime * angularSpeed;

		// если угол больше 360 - сбрасываем его до 0
		if (angle >= 360f) angle = 0f;
	}
}
