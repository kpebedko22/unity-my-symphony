using UnityEngine;

/// <summary>
/// Скрипт для движения от startPosition до endPosition используется для объектов Противник
/// </summary>
public class LerpMover : MonoBehaviour {
    /// <summary>
    /// Начальная позиции движения
    /// </summary>
    private Vector2 startPosition;

    /// <summary>
    /// Конечная позиции движения
    /// </summary>
    private Vector2 endPosition;

    /// <summary>
    /// Прогресс
    /// </summary>
    private float progress;

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
        startPosition = transform.position;
        endPosition = -startPosition;
    }

    private void FixedUpdate() {
        // вычисляем новое положение объекта
        transform.position = Vector2.Lerp(startPosition, endPosition, progress);
        progress += step;

        //if (transform.position.x == endPosition.x && transform.position.y == endPosition.y) {
        // Если объект не дошел до конечной позиции, выход 
        if (!transform.position.Equals(endPosition)) {
            return;
        }

        // Создаем объект Взрыв
        enemyBoomContainer.GetComponent<EnemyBoomInstantiation>().InstantiateEnemyBoom(transform.position);

        // Уничтожаем текущий объект
        Destroy(transform.gameObject);
    }
}