﻿using UnityEngine;

namespace TheLegendsOfNart.Components.ColliderBased
{
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField] protected LayerMask _layer;
        [SerializeField] protected bool _isTouchingLayer;
        public bool IsTouchingLayer => _isTouchingLayer;
    }
}
