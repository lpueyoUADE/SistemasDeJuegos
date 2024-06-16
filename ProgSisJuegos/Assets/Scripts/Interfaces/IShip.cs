using UnityEngine;

public interface IShip
{
    void InitializeWeapons();
    void Move(Vector3 direction);
    void Move(Vector3 direction, float speed, ForceMode type = ForceMode.Acceleration);
    void Shield(float duration, Color color);
}
