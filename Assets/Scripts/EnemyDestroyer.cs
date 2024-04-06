using UnityEngine;

public class EnemyDestroyer : MonoBehaviour {
    /// <summary>
    /// Объект, где создаются объекты Взрыв (анимация завершения движения)
    /// </summary>
    [SerializeField] private GameObject enemyBoomContainer;

    private EnemyBoomInstantiation _enemyBoomInstantiation;

    private void Start() {
        _enemyBoomInstantiation = enemyBoomContainer.GetComponent<EnemyBoomInstantiation>();
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (!other.tag.Equals("Enemy")) {
            return;
        }

        // Создаем объект Взрыв
        _enemyBoomInstantiation.InstantiateEnemyBoom(other.transform.position);

        // Уничтожаем текущий объект
        Destroy(other.gameObject);
    }
}