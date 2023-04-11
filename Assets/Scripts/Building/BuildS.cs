using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuildS : MonoBehaviour, IBuildable
{
    public abstract bool IsSelected { get; }

    public abstract void Build();
    public abstract void Select(object objectToSeelct);
    public abstract void StopBuild();
}
