using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestFinalGame.Models
{
    public class Coin : GameItem
    {
        public enum CoinType
        {
            Gold,
            Silver

        }

        public CoinType Type { get; set; }

        public Coin(int id, string name, int value, CoinType type, string description, int experiencePoints)
            : base(id, name, value, description, experiencePoints)
        {
            Type = type;
        }

        public override string InformationString()
        {
            return $"{Name}: {Description}\nValue: {Value}";
        }
    }
}

