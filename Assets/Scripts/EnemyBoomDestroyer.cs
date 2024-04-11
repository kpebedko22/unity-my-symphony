using UnityEngine;

/// <summary>
/// Скрипт для уничтожения объекта Взрыв
/// </summary>
public class EnemyBoomDestroyer : MonoBehaviour {
    /// <summary>
    /// Для анимации Взрыва проверяем
    /// </summary>
    public void DestroyMe() {
        Destroy(transform.gameObject);
    }
}