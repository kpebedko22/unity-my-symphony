using UnityEngine;

/// <summary>
/// Скрипт для создания объекта Противник
/// </summary>
public class EnemyInstantiation : MonoBehaviour {
    /// <summary>
    /// Префаб объекта Противник
    /// </summary>
    public GameObject prefab;

    public void InstantiateEnemy(int band, Transform shooter) {
        // Инкриментируем количество созданных объектов Противников
        GameController.totalEnemies++;

        // Создаем объект Противника на основе префаба
        // Родитель - пустой объект содержащий всех Противников
        GameObject instanceEnemy = Instantiate(prefab, transform);

        // Позиция стрелка
        var shooterPos = shooter.position;

        // Задаем начальную позицию объекта Противник, равной позиции Стрелка
        instanceEnemy.transform.position = shooterPos;

        // Поворачиваем объект Противник, чтобы было красиво 
        // (в какую сторону будет смотреть острый угол)
        instanceEnemy.transform.up = -shooterPos;

        // Задаем имя объекта
        instanceEnemy.name = "enemy" + band;
    }
}