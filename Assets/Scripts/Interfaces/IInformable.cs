using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public interface IInformable
{
    public DataToShow GetInfo();
}

public class DataToShow
{
    public Sprite MainSprite { private set; get; }
    public int Lifes { private set; get; }
    public int Damage { private set; get; }
    public string Name { private set; get; }
    public string Type { private set; get; }
    
    public DataToShow(Sprite sprite, int lifes, int damage, string name, string type)
    {
        MainSprite = sprite;
        Lifes = lifes;
        Name = name;
        Type = type;
        Damage = damage;
    }

}