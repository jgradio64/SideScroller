using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public static class PlayerAttributes
    {
        public static int strength { get; set; }
        public static int agility { get; set; }
        public static int dexterity { get; set; }

        public static void ResetAttributes()
        {
            PlayerAttributes.strength = 5;
            PlayerAttributes.agility = 5;
            PlayerAttributes.dexterity = 5;
        }
    }
}
