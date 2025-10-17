using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MelonLoader;
using HarmonyLib;
using UnityEngine;

namespace AutomaticVaultRedirection
{
    public class AutomaticVaultRedirection : MelonMod
    {
        public static float rotation = 0f;

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                rotation = 270f;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                rotation = 90f;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                rotation = 180f;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                rotation = 0f;
            }
        }
    }

    [HarmonyPatch(typeof(PlayerLerpMantle), "LerpPlayer")]
    public static class Patch
    {
        private static void Prefix()
        {
            GameManager.GM.player.GetComponent<CharacterMotor>().transform.Rotate(new Vector3(0f, AutomaticVaultRedirection.rotation, 0f));
        }
    }
}