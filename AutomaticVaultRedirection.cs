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
        public static bool relative = true;
        private static readonly Dictionary<KeyCode, float> defaultRotations = new Dictionary<KeyCode, float>
        {
            {KeyCode.Alpha0, 0f},
            {KeyCode.Alpha1, 270f},
            {KeyCode.Alpha2, 90f},
            {KeyCode.Alpha3, 180f}
        };

        public override void OnUpdate()
        {
            if (GameManager.GM.player == null)
            {
                return;
            }

            foreach (var entry in defaultRotations)
            {
                if (Input.GetKeyDown(entry.Key))
                {
                    rotation = entry.Value;
                    relative = true;
                    return;
                }
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                rotation = GameManager.GM.player.GetComponentInChildren<Camera>().transform.rotation.eulerAngles.y;
                relative = false;
            }
        }
    }

    [HarmonyPatch(typeof(PlayerLerpMantle), "LerpPlayer")]
    public static class Patch
    {
        private static void Prefix()
        {
            CharacterMotor motor = GameManager.GM.player.GetComponent<CharacterMotor>();
            Vector3 rotationVector = new Vector3(0f, AutomaticVaultRedirection.rotation, 0f);
            if (AutomaticVaultRedirection.relative)
            {
                motor.transform.Rotate(rotationVector);
            }
            else
            {
                motor.transform.eulerAngles = rotationVector;
            }
        }
    }
}
