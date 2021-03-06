﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utility
{
    public class PlayerInput : MonoBehaviour
    {
        private static GameObject _self;

        private static readonly Dictionary<Player.Player, bool> Resetted = new Dictionary<Player.Player, bool>
        {
            {Player.Player.P1, true},
            {Player.Player.P2, true},
            {Player.Player.P3, true},
            {Player.Player.P4, true}
        };

        private static readonly Dictionary<Player.Player, string> LastAxis = new Dictionary<Player.Player, string>
        {
            {Player.Player.P1, ""},
            {Player.Player.P2, ""},
            {Player.Player.P3, ""},
            {Player.Player.P4, ""}
        };

        private const float MoveThreshold = 0.5f;

        private void Awake()
        {
            if (_self == null)
            {
                _self = gameObject;
                DontDestroyOnLoad(_self);
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }

        public static float GetAxis(string axis, Player.Player player)
        {
            var p = (int) player;
            var value = Input.GetAxisRaw(axis + "_" + p);

            return value;
        }

        public static bool GetButton(string keyName, Player.Player player)
        {
            var p = player.ToString().Substring(1, 1);
            return Input.GetButton(keyName + "_" + p);
        }

        public static bool GetButtonUp(string keyName, Player.Player player)
        {
            var p = player.ToString().Substring(1, 1);
            return Input.GetButtonUp(keyName + "_" + p);
        }

        public static bool GetButtonDown(string keyName, Player.Player player)
        {
            var p = player.ToString().Substring(1, 1);
            return Input.GetButtonDown(keyName + "_" + p);
        }

        public static bool Direction(MoveDirection direction, Player.Player playerController)
        {
            var axis = "Horizontal";
            if (direction == MoveDirection.Down || direction == MoveDirection.Up)
            {
                axis = "Vertical";
            }

            var value = GetAxis(axis, playerController);

            if (Resetted[playerController])
            {
                if (value > MoveThreshold)
                {
                    if (direction == MoveDirection.Up || direction == MoveDirection.Right)
                    {
                        LastAxis[playerController] = axis;
                        Resetted[playerController] = false;
                        return true;
                    }
                }
                else if (value < -MoveThreshold)
                {
                    if (direction == MoveDirection.Down || direction == MoveDirection.Left)
                    {
                        LastAxis[playerController] = axis;
                        Resetted[playerController] = false;
                        return true;
                    }
                }
            }

            if (Resetted[playerController] == false && Math.Abs(value) < MoveThreshold &&
                LastAxis[playerController] == axis)
            {
                Resetted[playerController] = true;
            }

            return false;
        }
    }
}