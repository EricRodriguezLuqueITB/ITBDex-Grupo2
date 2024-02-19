using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fakemon
{
    public int id;
    public string fakename;
    public string season;
    public string type;
    public string info;
    public Fakemon(int id, string fakename, string season, string type, string info)
    {
        this.id = id;
        this.fakename = fakename;
        this.season = season;
        this.type = type;
        this.info = info;
    }
}
