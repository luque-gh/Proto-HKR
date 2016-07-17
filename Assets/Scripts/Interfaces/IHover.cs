using UnityEngine;
using System.Collections;

// NAO ACEITE ESTE COMENTARIO NO PULL REQUEST!! - Pegadinha do Malandro!
public interface IHover {

    void Move(float turn, float thrust, float strafe);

    void ShotWeapon1();

    void ShotWeapon2();

    void Damage(float value, Vector3 contactPos, IPlayable owner);

    float GetHealth();

    bool IsAlive();
}
