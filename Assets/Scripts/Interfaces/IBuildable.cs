using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildable 
{
    /// <summary>
    /// Проверяет, выбран ли объект
    /// </summary>
    public bool IsSelected { get; }
    /// <summary>
    /// Выбрать объект для постройки
    /// </summary>
    /// <param name="objectToSeelct">Объект, который будет построен</param>
    public void Select(object objectToSeelct);

    /// <summary>
    /// Закончить строительство для какой-либо из тем 
    /// </summary>
    public void StopBuild();

    /// <summary>
    /// Устанвоить имеющийся спрайт (на RileMap или просто на сцену)
    /// </summary>
    public void Build();
}
