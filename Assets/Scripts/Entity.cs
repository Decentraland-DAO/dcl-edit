using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using static System.Char;

public class Entity : MonoBehaviour
{
    [Serializable]
    public struct Json
    {
        public Json(Entity e)
        {
            name = e.name;
            components = e.Components.Select(c => new EntityComponent.Json(c)).ToList();
        }
         
        public string name;
        public List<EntityComponent.Json> components;
    }
    /*
    const darkCobblestoneTile21 = new Entity('darkCobblestoneTile21')
    engine.addEntity(darkCobblestoneTile21)
    darkCobblestoneTile21.setParent(_scene)
    darkCobblestoneTile21.addComponentOrReplace(gltfShape3)
    const transform29 = new Transform({
        position: new Vector3(16, 0, 10),
        rotation: Quaternion.Euler(0,90,0),
        scale: new Vector3(1, 1, 1)
    })
    darkCobblestoneTile21.addComponentOrReplace(transform29)
    */
    public string name;

    [Space]
    public GameObject gizmos;

    public GameObject componentsParent;


    public EntityComponent[] Components => GetComponents<EntityComponent>();

    /// <summary>
    /// will be true, when the game object is to be destroyed
    /// </summary>
    [NonSerialized]
    public bool doomed = false;

    void Start()
    {

    }

    private static int nameFillCount = 0;
    public string GetTypeScript(ref List<ScriptGenerator.ExposedVars> exposed)
    {
        if (name == "")
        {
            name = "Entity " + nameFillCount++;
        }

        var script = $"const {name.ToCamelCase()} = new Entity(\"{name}\")\n";
        script += $"engine.addEntity({name.ToCamelCase()})\n";

        var exposedVar = new ScriptGenerator.ExposedVars
        {
            exposedAs = "entity",
            symbol = name.ToCamelCase()
        };
        exposed.Add(exposedVar);

        foreach (var component in Components)
        {
            var componentTs = component.GetTypeScript();
            script += componentTs.setup;
            script += $"{name.ToCamelCase()}.addComponentOrReplace({componentTs.symbol})\n";

            var exposedComponentVar = new ScriptGenerator.ExposedVars
            {
                exposedAs = component.ComponentName,
                symbol = componentTs.symbol
            };
            exposed.Add(exposedComponentVar);

        }


        return script;
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(new Json(this));
    }
}

public static class CamelCase
{

    public static string ToCamelCase(this string s)
    {
        var retVal = "";
        bool lastWasSpace = false;

        foreach (var c in s)
        {
            if (IsLetterOrDigit(c))
            {
                retVal += lastWasSpace? ToUpper(c) :ToLower(c);
                lastWasSpace = false;
            }
            else if (IsWhiteSpace(c))
            {
                lastWasSpace = true;
            }
        }

        return retVal;
    }
}
