using UnityEngine;

/// <summary>
/// Статический класс для передачи данных между сценами
/// </summary>
public static class StaticStorage {
    /// <summary>
    /// Статическое поле для хранения аудио
    /// </summary>
    public static AudioClip AudioClip { get; set; }
}