using UnityEngine;

public interface IMoveable
{
    void Move(Vector2 delta);
    void Stop();
}