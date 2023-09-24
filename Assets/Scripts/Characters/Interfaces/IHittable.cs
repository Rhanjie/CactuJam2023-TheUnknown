using UnityEngine;

interface IHittable
{
    public Transform Handler { get; }

    public void Hit(float damage);
}