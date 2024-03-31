using UnityEngine;

public class MoveMyHero : MonoBehaviour {
	/*
	 * Скрипт для передвижения Героя за курсором
	 */

	Rigidbody2D rb;
	float moveSpeed = 500f;

	void Start () {

		// скрываем курсор
		Cursor.visible = false;

		rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {

		// получаем координаты мыши (курсора) и перемещаем Героя в текущую точку курсора
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector2 direction = (mousePosition - transform.position).normalized;
		rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
	}
}
