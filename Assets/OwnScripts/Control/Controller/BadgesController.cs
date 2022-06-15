using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadgesController
{
    private BadgeDAO badgeDao;
    private GameDAO gameDao;

    public BadgesController()
    {
        badgeDao = new BadgeDAO();
        gameDao = new GameDAO();
    }

    public void saveBadge(Player player, int badgeId)
    {
        badgeDao.saveBadge(player, badgeId);
    }

    public bool userWonBadges(Game playedGame)
    {
        Player player = playedGame.GamePlayer;
        int numberOfGames = gameDao.numberOfGamesPlayed(playedGame.GamePlayer);
        bool result = false;
        if (numberOfGames == 1)
        {
            if (playedGame.Role.Equals("Dev")) {
                if (!badgeDao.hasBadge(player, 1))
                {
                    saveBadge(player, 1);
                    result = true;
                }
            }
            else
            {
                if (!badgeDao.hasBadge(player, 2))
                {
                    saveBadge(player, 2);
                    result = true;
                }
                
            }

        }
        else if (numberOfGames >= 5)
        {
            if (playedGame.Role.Equals("Dev"))
            {
                if (!badgeDao.hasBadge(player, 3))
                {
                    saveBadge(player, 3);
                    result = true;
                }
                
            }
            else
            {
                if (!badgeDao.hasBadge(player, 4))
                {
                    saveBadge(player, 4);
                    result = true;
                }
                    
            }
            saveBadge(player, 5);
        }

        if (!playedGame.Calification.Equals(""))
        {
            switch(playedGame.Calification)
            {
                case "D":
                    if (!badgeDao.hasBadge(player, 6))
                    {
                        saveBadge(player, 6);
                        result = true;
                    }
                    break;
                case "C":
                    if (!badgeDao.hasBadge(player, 7))
                    {
                        saveBadge(player, 7);
                        result = true;
                    }   
                    break;
                case "B":
                    if (!badgeDao.hasBadge(player, 8))
                    {
                        saveBadge(player, 8);
                        result = true;
                    }
                        
                    break;
                case "A":
                    if (!badgeDao.hasBadge(player, 9))
                    {
                        saveBadge(player, 9);
                        result = true;
                    }
                        
                    break;
                case "S":
                    if (!badgeDao.hasBadge(player, 10))
                    {
                        saveBadge(player, 10);
                        result = true;
                    }
                    break;
            }
        }
        return result;
    }

}
