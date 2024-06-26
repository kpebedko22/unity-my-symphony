﻿using UnityEngine;

/// <summary>
/// Скрипт для передвижения Героя за курсором
/// </summary>
public class MoveMyHero : MonoBehaviour {
    private Rigidbody2D _rb;

    private const float MoveSpeed = 500f;

    private Camera _camera;

    private void Start() {
        Cursor.visible = false;

        _rb = GetComponent<Rigidbody2D>();

        _camera = Camera.main;
    }

    private void Update() {
        // получаем координаты мыши (курсора) и перемещаем Героя в текущую точку курсора
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;
        _rb.velocity = new Vector2(direction.x * MoveSpeed, direction.y * MoveSpeed);
    }
}