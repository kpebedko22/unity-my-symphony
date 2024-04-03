using UnityEngine;

/// <summary>
/// Скрипт для движения от startPosition до endPosition используется для объектов Противник
/// </summary>
public class LerpMover : MonoBehaviour {
    /// <summary>
    /// Начальная позиции движения
    /// </summary>
    private Vector2 _startPosition;

    /// <summary>
    /// Конечная позиции движения
    /// </summary>
    private Vector2 _endPosition;

    /// <summary>
    /// Прогресс
    /// </summary>
    private float _progress;

    /// <summary>
    /// Шаг
    /// </summary>
    public float step;

    /// <summary>
    /// Объект, где создаются объекты Взрыв (анимация завершения движения)
    /// </summary>
    public GameObject enemyBoomContainer;

    /// <summary>
    /// При инициализации объекта Противник задаем начальную и конечную позиции движения
    /// </summary>
    private void Start() {
        _startPosition = transform.position;
        _endPosition = -_startPosition;
    }

    private void FixedUpdate() {
        // вычисляем новое положение объекта
        transform.position = Vector2.Lerp(_startPosition, _endPosition, _progress);
        _progress += step;

        // Если объект дошел до конечной позиции 
        if (transform.position.x != _endPosition.x || transform.position.y != _endPosition.y) {
            return;
        }

        // Создаем объект Взрыв
        enemyBoomContainer.GetComponent<EnemyBoomInstantiation>().InstantiateEnemyBoom(transform.position);

        // Уничтожаем текущий объект
        Destroy(transform.gameObject);
    }
}