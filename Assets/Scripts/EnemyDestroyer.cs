using UnityEngine;

public class EnemyDestroyer : MonoBehaviour {
    /// <summary>
    /// Объект, где создаются объекты Взрыв (анимация завершения движения)
    /// </summary>
    [SerializeField] private GameObject enemyBoomContainer;

    private void OnTriggerExit2D(Collider2D other) {
        if (!other.tag.Equals("Enemy")) {
            return;
        }

        // Создаем объект Взрыв
        enemyBoomContainer.GetComponent<EnemyBoomInstantiation>().InstantiateEnemyBoom(other.transform.position);

        // Уничтожаем текущий объект
        Destroy(other.gameObject);
    }
}