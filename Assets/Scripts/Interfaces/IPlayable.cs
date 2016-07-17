using UnityEngine;
using System.Collections;

public interface IPlayable {

    void IncrementFrag();

    void DecrementFrag();

    void IncrementDeaths();

    void Respawn();

    void ActionUpdate();

}
