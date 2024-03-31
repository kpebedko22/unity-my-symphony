using UnityEngine;

public class ParamCircle : MonoBehaviour {
	/*
	 * Скрипт для изменения размера кружочков
	 * которые стреляют противниками
	 */

	// номер диапазона
	public int band;

	// начальный scale, и множитель scale
	public float startScale, scaleMultiplier;

	// время спавна
	private float nextSpawnTime;

	// объект, в котором создаются Противники
	public GameObject enemyContainer;

	// флаг спавна
	private bool ableToShoot;

	void Start() {
		// инициализируем время спавна как время запуска игры + 1,5секунды
		nextSpawnTime = Time.time + 1.5f;

		// флаг спавна ставим в true
		ableToShoot = true;
	}

	void Update () {

		// вычисляем значение scale для координат X, Y
		float x = (AudioEngine.bandBuffer[band] * scaleMultiplier) + startScale;

		// если был скачок в музыке
		// то создаем Противника и ждем некоторое время, чтобы создавать еще
		// также используем флаг, необходимо чтобы значение сначала опустилось ниже амплитуды
		if (x > (startScale + (AudioEngine.amplitude * scaleMultiplier))) {

			if (ableToShoot && Time.time > nextSpawnTime) {
				// вызываем создание Противника передавая текущий объект, чтобы знать начальную позицию
				enemyContainer.GetComponent<EnemyInstantiation>().InstantiateEnemy(band, this.transform);

				// флаг спавна в false
				// увеличиваем время спавна
				ableToShoot = false;
				nextSpawnTime += 1.5f;
			}
		}

		// иначе флаг спавна = true
		else {
			ableToShoot = true;
		}

		// меняем размер текущего объекта
		transform.localScale = new Vector3(x, x, transform.localScale.z);
	}
}
