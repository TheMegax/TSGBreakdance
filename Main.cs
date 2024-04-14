using System;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.GenericBehaviors;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Unity.Display;
using Il2CppSystem.IO;
using MelonLoader;
using TSGBreakdance;
using UnityEngine;
using Main = TSGBreakdance.Main;
using Object = UnityEngine.Object;
using SVector3 = Il2CppAssets.Scripts.Simulation.SMath.Vector3;

// ReSharper disable MemberCanBePrivate.Global

[assembly: MelonInfo(typeof(Main), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace TSGBreakdance;

public class Main : BloonsTD6Mod
{
    private static MelonLogger.Instance? _mllog;

    public override void OnNewGameModel(GameModel result)
    {
        ApplyBreakdance(result);
        for (var i = 1; i <= 2; i++)
        {
            ApplyBreakdance(result, i);
        }
        for (var j = 1; j <= 2; j++)
        {
            ApplyBreakdance(result, 0, j);
        }
    }

    private static void ApplyBreakdance(GameModel result, int i = 0, int j = 0)
    {
        var tsgTowerModel = result.GetTower(TowerType.SuperMonkey, 5, i, j);
        var displayModel = tsgTowerModel.GetAttackModel().GetBehavior<DisplayModel>();
        displayModel.ApplyDisplay<TrueSunGodBreakdance>();
        displayModel.ignoreRotation = true;
        displayModel.positionOffset = new SVector3(0, -5, 100);
    }
    
    public enum MessageType
    {
        Msg,
        Warn,
        Error
    }

    public static void Log(object thingtolog,MessageType type= MessageType.Msg)
    {
        if (_mllog == null) return;
        switch (type) {
            case MessageType.Msg:
                _mllog.Msg(thingtolog);
                break;
            case MessageType.Warn:
                _mllog.Warning(thingtolog);
                break;
            case MessageType.Error:
                _mllog.Error(thingtolog);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    public override void OnInitialize()
    {
        _mllog = LoggerInstance;
    }
    
    public static Shader? GetOutlineShader()
    {
        var superMonkey = GetVanillaAsset("Assets/Monkeys/DartMonkey/Graphics/SuperMonkey.prefab")?.Cast<GameObject>();
        if (superMonkey == null) return null;
        superMonkey.AddComponent<UnityDisplayNode>();
        var litOutlineShader = superMonkey.GetComponent<UnityDisplayNode>().GetMeshRenderer().material.shader;
        return litOutlineShader;
    }
    
    public static Object? GetVanillaAsset(string name)
    {
        foreach (var assetBundle in AssetBundle.GetAllLoadedAssetBundles().ToArray())
        {
            if (assetBundle.Contains(name))
            {
                return assetBundle.LoadAsset(name);
            }
        }
            
        Log("Could not find asset from vanilla asset bundles!", MessageType.Error);
        return null;
    }
}