using Mono.Data.Sqlite;
using System.Data;
using System.Collections.Generic;
using Assets.Scripts.Persistance;
using System;
using UnityEngine;

public class EffectDAO
{
    private Singleton db;
    private string sqlQuery;

    public EffectDAO()
    {
        db = Singleton.GetInstance();
        sqlQuery = null;
    }

}
