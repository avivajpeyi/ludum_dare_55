using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Summonable : MonoBehaviour
{
    public float maxSize;
    protected float _size;

    abstract public void Summon();

    abstract public void Grow();
}