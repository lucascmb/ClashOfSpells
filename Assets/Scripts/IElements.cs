using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IElements {

    string GetName();
    void Off();
    void On();
    void DisableCollisions();
    void PrepareForBattle();
    Transform GetTransform();
}
