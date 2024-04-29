using System;
using UnityEngine;

namespace TheLegendsOfNart.Utils
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField] private float _value;

        private float _timesUp;

        public void Reset()
        {
            _timesUp = Time.time + _value;
        }

        public bool IsReady => _timesUp <= Time.time;
    }
}
