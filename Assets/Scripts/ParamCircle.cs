using UnityEngine;

/// <summary>
/// Скрипт для изменения размера кружочков, которые стреляют противниками
/// </summary>
public class ParamCircle : MonoBehaviour {
    /// <summary>
    /// Номер диапазона
    /// </summary>
    public int band;

    /// <summary>
    /// Начальный scale, и множитель scale
    /// </summary>
    public float startScale, scaleMultiplier;

    /// <summary>
    /// Время спавна
    /// </summary>
    private float nextSpawnTime;

    /// <summary>
    /// Объект, в котором создаются Противники
    /// </summary>
    public GameObject enemyContainer;

    /// <summary>
    /// Флаг спавна
    /// </summary>
    private bool ableToShoot;

    private void Start() {
        // инициализируем время спавна как время запуска игры + 1,5секунды
        nextSpawnTime = Time.time + 1.5f;

        // флаг спавна ставим в true
        ableToShoot = true;
    }

    private void Update() {
        // вычисляем значение scale для координат X, Y
        var x = AudioEngine.bandBuffer[band] * scaleMultiplier + startScale;

        // если был скачок в музыке
        // то создаем Противника и ждем некоторое время, чтобы создавать еще
        // также используем флаг, необходимо чтобы значение сначала опустилось ниже амплитуды
        if (x > AudioEngine.amplitude * scaleMultiplier + startScale) {
            if (ableToShoot && Time.time > nextSpawnTime) {
                // вызываем создание Противника передавая текущий объект, чтобы знать начальную позицию
                enemyContainer.GetComponent<EnemyInstantiation>().InstantiateEnemy(band, transform);

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