
using System.Collections;
using UnityEngine;

namespace TheLegendsOfNart.Creatures.Patrolling
{
    public abstract class Patrol : MonoBehaviour
    {
        public abstract IEnumerator DoPatrol();
    }
}
