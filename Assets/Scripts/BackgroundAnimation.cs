using UnityEngine;

/// <summary>
/// Скрипт для создания анимации на фоне
/// </summary>
public class BackgroundAnimation : MonoBehaviour {
    /// <summary>
    /// Префаб объекта для фона (обычный прямоугольник)
    /// </summary>
    public GameObject prefab;

    /// <summary>
    /// Массив объектов
    /// </summary>
    private readonly GameObject[] _samples = new GameObject[AudioEngine.SamplesCount];

    /// <summary>
    /// Коэфициент увеличения объекта
    /// </summary>
    public float maxScale;

    private void Start() {
        // Создаем 512 прямоугольников по кругу
        for (var i = 0; i < AudioEngine.SamplesCount; i++) {
            // Формула для позиции:
            // берем косинус (для икса) и синус (для игрика) от (2 * pi / 512 * i)
            // и умножаем на радиус = 80
            var sample = Instantiate(
                prefab,
                new Vector2(
                    Mathf.Cos(0.01227185f * i) * 80f,
                    Mathf.Sin(0.01227185f * i) * 80f
                ),
                Quaternion.identity,
                transform
            );

            // Задаем, куда направлен верх объекта (делаем поворот объекта)
            sample.transform.up = -sample.transform.position;

            sample.name = "sample" + i;

            _samples[i] = sample;
        }
    }

    /// <summary>
    /// Каждый прямоугольник увеличиваем/уменьшаем по высоте (т.е. по координате Y)
    /// </summary>
    private void FixedUpdate() {
        for (var i = 0; i < AudioEngine.SamplesCount; i++) {
            
            // TODO: check this 'if'
            if (_samples != null) {
                _samples[i].transform.localScale = new Vector3(
                    1,
                    (AudioEngine.samples[i] * maxScale) + 2,
                    1
                );
            }
        }
    }
}