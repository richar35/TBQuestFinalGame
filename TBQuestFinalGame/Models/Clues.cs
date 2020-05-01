using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBQuestFinalGame.Models
{
    public class Clues : GameItem
    {
        public enum UseActionType
        {
            OPENLOCATION,
            KILLPLAYER,
            MOVEPLAYER


        }

        public UseActionType UseAction { get; set; }

        public Clues(int id, string name, int value, string description, int experiencePoints, string useMessage, UseActionType useAction)
            : base(id, name, value, description, experiencePoints, useMessage)
        {
            UseAction = useAction;
        }

        public override string InformationString()
        {
            return $"{Name}: {Description}\nValue: {Value}";
        }


    }
}