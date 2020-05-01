using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TBQuestFinalGame.Models
{
    public class Enemy : NPC, ISpeak, IBattle
    {
        Random r = new Random();
        public List<string> Messages { get; set; }

        private const int DEFENDER_DAMAGE_ADJUSTMENT = 10;
        private const int MAXIMUM_RETREAT_DAMAGE = 10;

        public int SkillLevel { get; set; }
        public BattleModeName BattleMode { get; set; }
        public Weapon CurrentWeapon { get; set; }
        private int _health;
        public int MinimumDamage { get; set; }
        public int MaximumDamage { get; set; }
        public int RewardExp { get; private set; }
        public GameItem Loot { get; set; }
        public int Health
        {
            get { return _health; }
            set
            {
                _health = value;
                OnPropertyChanged(nameof(Health));
            }
        }


        protected override string InformationText()
        {
            return $"{Name} - {Description}";
        }
        public Enemy()
        {

        }

        public Enemy(
            int id,
            string name,
            int health,
            RaceType race,
            List<string> messages,
            int minimumDamage,
            int maximumDamage,
            int skillLevel,
            GameItem loot,
           int rewardExp,
            Weapon currentWeapon,
        string description)
         : base(id, name, race, description)
        {

            Messages = messages;
            SkillLevel = skillLevel;
            CurrentWeapon = currentWeapon;
            Health = health;
            MinimumDamage = minimumDamage;
            MaximumDamage = maximumDamage;
            RewardExp = rewardExp;
            Loot = loot;
        }


        public string Speak()
        {
            if (this.Messages != null)
            {
                return GetMessage();
            }
            else
            {
                return "";
            }
        }
        private string GetMessage()
        {
            int messageIndex = r.Next(0, Messages.Count());
            return Messages[messageIndex];
        }

        #region BATTLE METHODS
        public int Attack()
        {
            int hitPoints = random.Next(CurrentWeapon.MinimumDamage, CurrentWeapon.MaximumDamage) * SkillLevel;

            if (hitPoints <= 100)
            {
                return hitPoints;
            }
            else
            {
                return 100;
            }
        }
        public int Defend()
        {
            int hitPoints = (random.Next(CurrentWeapon.MinimumDamage, CurrentWeapon.MaximumDamage) * SkillLevel) - DEFENDER_DAMAGE_ADJUSTMENT;

            if (hitPoints >= 0 && hitPoints <= 100)
            {
                return hitPoints;
            }
            else if (hitPoints > 100)
            {
                return 100;
            }
            else
            {
                return 0;
            }
        }
        public int Retreat()
        {
            int hitPoints = SkillLevel * MAXIMUM_RETREAT_DAMAGE;

            if (hitPoints <= 100)
            {
                return hitPoints;
            }
            else
            {
                return 100;
            }
        }
        #endregion
    }
}
