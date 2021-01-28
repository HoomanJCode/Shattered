﻿using System;
using UnityEngine;

// ReSharper disable Unity.InefficientPropertyAccess

namespace Movements
{
    public abstract class Movement : MonoBehaviour
    {
        [SerializeField] private float movementSpeed = 1;
        [SerializeField] private float rotationSpeed = 1;
        [NonSerialized] public Vector2 TargetPosition;
        [NonSerialized] public Quaternion TargetRotation;

        protected float MovementSpeed => movementSpeed;

        protected float RotationSpeed => rotationSpeed;

        private void Awake()
        {
            TargetPosition = transform.position;
            TargetRotation = transform.rotation;
        }

        private void FixedUpdate()
        {
            transform.position = Vector2.Lerp(transform.position, TargetPosition, Time.deltaTime * movementSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, TargetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PhysicalMovement : Movement
    {
        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            TargetPosition = transform.position;
            TargetRotation = transform.rotation;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody2D.MovePosition(Vector2.Lerp(_rigidbody2D.position, TargetPosition,
                Time.deltaTime * RotationSpeed));
            _rigidbody2D.MoveRotation(Quaternion.Lerp(transform.rotation, TargetRotation,
                Time.deltaTime * RotationSpeed));
        }
    }
}