using UnityEngine;

public class BackgroundAnimation : MonoBehaviour {
	/*
	 * Скрипт для создания анимации на фоне
	 */

	// префаб объекта для фона (обычный прямоугольник)
	public GameObject prefab;

	// массив этих объектов
	GameObject[] sampleSquare = new GameObject[512];

	// коэфициент увеличения объекта
	public float maxScale;

	void Start () {

		// создаем 512 прямоугольников по кругу
		for (int i = 0; i < 512; i++) {

			// создаем объект
			GameObject bgSquare = (GameObject)Instantiate(prefab);

			// задаем родителя - объект на который прикреплен данный скрипт
			bgSquare.transform.SetParent(this.transform);

			// задаем имя объекта
			bgSquare.name = "bgSquare" + i;

			// задаем позицию - координаты X, Y высчитываем как
			// берем косинус (для икса) и синус (для игрика) от (2 * pi / 512 * i)
			// и умножаем на радиус = 80
			bgSquare.transform.position = new Vector2(Mathf.Cos(0.01227185f * i) * 80f, Mathf.Sin(0.01227185f * i) * 80f);

			// задаем куда направлен вверх объекта (делаем поворот объекта)
			bgSquare.transform.up = -bgSquare.transform.position;

			// сохраняем в массиве
			sampleSquare[i] = bgSquare;
		}
	}
	
	void FixedUpdate () {

		// каждый прямоугольник увеличиваем/уменьшаем по высоте (т.е. по координате Y)
		for (int i = 0; i < 512; i++)
			if (sampleSquare != null)
				sampleSquare[i].transform.localScale = new Vector3(1, (AudioEngine.samples[i] * maxScale) + 2, 1);
	}
}
