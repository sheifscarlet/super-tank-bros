using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakeDamage(int damageAmount);
    void ResetHealth();
    
}