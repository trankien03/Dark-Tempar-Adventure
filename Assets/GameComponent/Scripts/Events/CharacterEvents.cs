using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;


public class CharacterEvents
{
    //character damage and damage value
    public static UnityAction<GameObject, int> characterDamaged;

    //character health and amount of heal 
    public static UnityAction<GameObject, int> characterHealth;
}
