using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuildable 
{
    /// <summary>
    /// ���������, ������ �� ������
    /// </summary>
    public bool IsSelected { get; }
    /// <summary>
    /// ������� ������ ��� ���������
    /// </summary>
    /// <param name="objectToSeelct">������, ������� ����� ��������</param>
    public void Select(object objectToSeelct);

    /// <summary>
    /// ��������� ������������� ��� �����-���� �� ��� 
    /// </summary>
    public void StopBuild();

    /// <summary>
    /// ���������� ��������� ������ (�� RileMap ��� ������ �� �����)
    /// </summary>
    public void Build();
}
