using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine.UIElements;

namespace GameData
{
    public static class EnumUtil
    {
        public static T ParseEnum<T>(string str) where T : Enum
        {
            if (String.IsNullOrEmpty(str))
                return default(T);

            return (T)Enum.Parse(typeof(T), str, true);
        }

        public static T ParseEnum<T>(string[] strings) where T : Enum
        {
            if (!typeof(T).IsDefined(typeof(FlagsAttribute), inherit: false))
                throw new InvalidOperationException($"Type '{typeof(T).Name}' is not a Flags enum.");

            int combinedValue = 0;

            foreach (string str in strings)
            {
                if (String.IsNullOrEmpty(str))
                    continue;

                T parsedData = ParseEnum<T>(str);
                combinedValue |= Convert.ToInt32(parsedData);
            }

            return (T)Enum.ToObject(typeof(T), combinedValue);
        }
    }
    public enum UnitType
    {
        None = 0,
        Ground,
        Air,
        Building,
        Spell,
        End
    }
    public enum AttackType
    {
        None = 0,
        Melee,
        Range, 
        Splash,
        Area,
        End
    }

    [Flags]
    public enum AttackAbleType
    {
        None = 0,
        Ground = 1 << 0,
        Air = 1 << 1,
        Building = 1 << 2,
    }
    public enum SpeedType
    {
        None = 0,
        Slow,
        Medium,
        Fast,
        End
    }

}