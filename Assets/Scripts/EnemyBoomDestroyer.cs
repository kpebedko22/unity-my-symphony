using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Скрипт для уничтожения объекта Взрыв
/// </summary>
public class EnemyBoomDestroyer : MonoBehaviour {
    /// <summary>
    /// Для анимации Взрыва проверяем:
    /// если альфа-канал равен нулю, то объект прозрачный и его можно удалить со сцены
    /// </summary>
    private void Update() {
        if (transform.GetComponent<Image>().color.a == 0) {
            Destroy(transform.gameObject);
        }
    }
}