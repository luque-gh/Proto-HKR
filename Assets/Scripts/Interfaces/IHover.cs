using UnityEngine;
using System.Collections;

public interface IHover {

    void Move(float turn, float thrust, float strafe);

    void ShotWeapon1();

    void ShotWeapon2();

    void Damage(float value, Vector3 contactPos, IPlayable owner);

    float GetHealth();

    bool IsAlive();
}
