using System.Collections;
using System.Collections.Generic;

public class Player
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public int Age { get; set; }
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
