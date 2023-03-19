using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Unity.Display;
using UnityEngine;

// ReSharper disable ClassNeverInstantiated.Global

namespace TSGBreakdance
{
    public class TrueSunGodBreakdance : ModCustomDisplay
    {
        public override string AssetBundleName => "breakdance";
        
        public override string PrefabName => "TSGBreakdance";
        
        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            // Outline shader
            var litOutlineShader = Main.GetOutlineShader();

            // Set shader
            node.GetMeshRenderer().material.shader = litOutlineShader;
            node.GetMeshRenderer().SetOutlineColor(new Color(0.64f, 0.31f, 0f));
        }
    }
}