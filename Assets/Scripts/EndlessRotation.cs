using UnityEngine;

/// <summary>
/// Скрипт бесконечного вращения объекта вокруг собственного центра
/// </summary>
public class EndlessRotation : MonoBehaviour {
    /// <summary>
    /// Угол, на который выполняется поворот объекта
    /// </summary>
    [SerializeField] private float angle = 3f;

    /// <summary>
    /// Поворот объекта на угол angle по оси Z
    /// </summary>
    private void Update() {
        transform.Rotate(new Vector3(0, 0, angle));
    }
}