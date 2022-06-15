using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player
{
    [SerializeField]
    public int _userId;
    public int UserId { get { return _userId; } set { _userId = value; } }

    [SerializeField]
    public string _userName;
    public string UserName { get { return _userName; } set { _userName = value; } }

    [SerializeField]
    public int _age;
    public int Age { get { return _age; } set { _age = value; } }
    public List<Badge> WonBadges { get; set; }

    public Player(int userId, string userName, int age)
    {
        UserId = userId;
        UserName = userName;
        Age = age;
        WonBadges = new List<Badge>();
    }
    public Player(string userName, int age)
    {
        UserName = userName;
        Age = age;
        WonBadges = new List<Badge>();
    }


}
