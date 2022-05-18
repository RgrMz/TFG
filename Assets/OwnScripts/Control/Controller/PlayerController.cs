using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private PlayerDAO playerDao;
    public Player PlayerOfTheGame {get; set;}

    public PlayerController()
    {
        playerDao = new PlayerDAO();
    }

    public int savePlayer(Player p)
    {
        PlayerOfTheGame = p;
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
    public void loadPlayer(string username)
    {
        PlayerOfTheGame = playerDao.getPlayer(username);
    }

    public List<Badge> getBadgesOfPlayer()
    {
        return PlayerOfTheGame.WonBadges;
    }

}
