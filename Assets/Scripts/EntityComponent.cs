﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public abstract class EntityComponent: MonoBehaviour
{
    [Serializable]
    public class Json
    {
        public Json(EntityComponent ec)
        {
            name = ec.ComponentName;
            specifics = ec.SpecificJson;
        }

        public string name;
        public string specifics;
    }

    public abstract string SpecificJson { get; }
    public abstract void ApplySpecificJson(string jsonString);

    protected Entity entity;
    public virtual void Start()
    {
        entity = GetComponent<Entity>();
    }

    public abstract string ComponentName { get; }


    public struct Ts
    {
        public Ts(string symbol, string setup)
        {
            this.symbol = symbol;
            this.setup = setup;
        }

        public string symbol;
        public string setup;
    }

    public abstract Ts GetTypeScript();
    //{
    //    return new Ts( $"{entityName}setup", $"const {entityName}setup = new BoxShape()\n");
    //}
}
