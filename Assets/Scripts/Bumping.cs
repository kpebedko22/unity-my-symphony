using UnityEngine;

/// <summary>
/// Скрипт для проверки столкновения двух объектов
/// Столкнувшиеся объекты проходят скозь друг друга
/// </summary>
public class Bumping : MonoBehaviour {
    /// <summary>
    /// Принадлежность скрипта объекту Герой
    /// </summary>
    public bool isMyHero;

    private void OnTriggerEnter2D(Collider2D other) {
        // Если объект Герой столкнулся с кем-то, то засчитываем
        if (isMyHero) {
            GameController.enemiesBumped++;
        }
    }
}