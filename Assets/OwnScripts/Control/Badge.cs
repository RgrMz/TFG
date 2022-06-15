using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Badge
{
    public int BadgeId { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
    public string Title { get; set; }

    public Badge (int badgeId, string desc, string imagePath, string title)
    {
        BadgeId = badgeId;
        Description = desc;
        ImagePath = imagePath;
        Title = title;
    }
}
