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
    /// Начальный scale
    /// </summary>
    [SerializeField] private float startScale;

    /// <summary>
    /// Множитель scale
    /// </summary>
    [SerializeField] private float scaleMultiplier;

    /// <summary>
    /// Время спавна
    /// </summary>
    private float _nextSpawnTime;

    /// <summary>
    /// Объект, в котором создаются Противники
    /// </summary>
    public GameObject enemyContainer;

    /// <summary>
    /// Флаг спавна
    /// </summary>
    private bool _isAbleToShoot;

    private void Start() {
        // инициализируем время спавна как время запуска игры + 1,5секунды
        _nextSpawnTime = Time.time + 1.5f;

        // флаг спавна ставим в true
        _isAbleToShoot = true;
    }

    private void Update() {
        // Вычисляем значение scale для координат X, Y
        var x = AudioEngine.bandBuffer[band] * scaleMultiplier + startScale;

        // если был скачок в музыке
        // то создаем Противника и ждем некоторое время, чтобы создавать еще
        // также используем флаг, необходимо чтобы значение сначала опустилось ниже амплитуды
        if (x > AudioEngine.amplitude * scaleMultiplier + startScale) {
            if (_isAbleToShoot && Time.time > _nextSpawnTime) {
                // вызываем создание Противника передавая текущий объект, чтобы знать начальную позицию
                enemyContainer.GetComponent<EnemyInstantiation>().InstantiateEnemy(band, transform);

                // флаг спавна в false
                // увеличиваем время спавна
                _isAbleToShoot = false;
                _nextSpawnTime += 1.5f;
            }
        }
        // иначе флаг спавна = true
        else {
            _isAbleToShoot = true;
        }

        // Меняем размер текущего объекта
        transform.localScale = new Vector3(x, x, transform.localScale.z);
    }
}