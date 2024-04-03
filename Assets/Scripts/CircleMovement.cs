using UnityEngine;

/// <summary>
/// Скрипт для движения объектов по кругу вокруг центра с заданным радиусом и скоростью
/// </summary>
public class CircleMovement : MonoBehaviour {
    public Transform center;

    [SerializeField] private float radius = 2f, angularSpeed = 2f;

    public float angle = 0f;

    private void Update() {
        var centerPos = center.position;

        // Вычисляем новые координаты X, Y для объекта
        // Задаем позицию объекта по вычисленным координатам
        transform.position = new Vector2(
            centerPos.x + Mathf.Cos(angle) * radius,
            centerPos.y + Mathf.Sin(angle) * radius
        );

        // Увеличиваем текущий угол
        angle += Time.deltaTime * angularSpeed;

        // Если угол больше 360 - сбрасываем его до 0
        if (angle >= 360f) {
            angle = 0f;
        }
    }
}