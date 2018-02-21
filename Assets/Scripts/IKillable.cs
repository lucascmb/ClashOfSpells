using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKillable {

    void Death();
    void TakeDamage(float damage);
    void Recoil(Vector2 force);

}
