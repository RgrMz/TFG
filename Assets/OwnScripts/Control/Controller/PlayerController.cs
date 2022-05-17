using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private PlayerDAO playerDao;

    public PlayerController()
    {
        playerDao = new PlayerDAO();
    }

    public int savePlayer(Player p)
    {
        return playerDao.savePlayer(p);
    }

    public List<string> getUsernames()
    {
        List<Player> players = playerDao.getAllPlayers();
        List<string> usernames = new List<string>();
        foreach(Player p in players)
        {
            usernames.Add(p.UserName);
        }

        return usernames;
    }
}
