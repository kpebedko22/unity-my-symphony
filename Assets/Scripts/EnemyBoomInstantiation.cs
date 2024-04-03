using UnityEngine;

/// <summary>
/// Скрипт для создания объектов Взрыв
/// </summary>
public class EnemyBoomInstantiation : MonoBehaviour {
    /// <summary>
    /// Префаб объекта Взрыв
    /// </summary>
    public GameObject prefab;

    public void InstantiateEnemyBoom(Vector3 positionBoom) {
        // Родитель - пустой объект содержащий все Взрывы
        // Позиция - конечная позиция Противника
        GameObject instanceEnemyBoom = Instantiate(
            prefab,
            positionBoom,
            Quaternion.identity,
            transform
        );

        // Задаем имя объекта
        instanceEnemyBoom.name = "enemyBoom";
    }
}