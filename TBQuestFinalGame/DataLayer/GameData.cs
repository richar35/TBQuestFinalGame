using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBQuestFinalGame.Models;
using System.Collections.ObjectModel;

namespace TBQuestFinalGame.DataLayer
{
    public class GameData
    {
        public static Player PlayerData()
        {
            return new Player()
            {
                Id = 1,
                Name = "Betty Smith",
                Age = 43,
                Race = Player.RaceType.Human,
                LocationId = 0,
                ExpierencePoints = 10,
                Lives = 3,
                Health = 100,
                SkillLevel = 5,
                Inventory = new ObservableCollection<GameItem>()
                {
                    GameItemById(2003),
                    GameItemById(1003)

                }

            };

        }

        public static List<string> InitialMessages()
        {
            return new List<string>()
            {
                "You awake to find yourself in a beautiful green forest with a throbbing headache. You are unsure where you are or how you arrived " +
                "at this spot. The last thing you remember is hiking trough the trails at a local park. As you stand and look around, you see nothing" +
                "nothin that looks forilar. After a few minutes you notice something in your pocket. Reaching into your pocket, you pull out a birght"+
                "red ruby. You start to remember you found this on the side of the trail next to an oak tree. After a ffew minutes, you decide to look"+
                "around. You notice a castle to the East on a hill. You begin walking towards the castle."

            };
        }


        private static GameItem GameItemById(int id)
        {
            return StandardGameItems().FirstOrDefault(i => i.Id == id);
        }

        private static NPC NpcById(int id)
        {
            return Npcs().FirstOrDefault(i => i.Id == id);
        }

        public static GameMapCoordinates InitialGameMapLocation()
        {
            return new GameMapCoordinates() { Row = 0, Column = 1 };
        }

        public static Map GameMap()
        {
            int rows = 3;
            int columns = 4;
            Map gameMap = new Map(rows, columns);

            gameMap.StandardGameItems = StandardGameItems();


            {
                gameMap.MapLocations[0, 1] = new Location()
                {
                    Id = 0,
                    Name = "Forest",
                    Description = "You awake to find yourself in a beautiful green forest with a throbbing headache.You are unsure where you are or how you arrived " +
                               "at this spot. The last thing you remember is hiking trough the trails at a local park. As you stand and look around, you see nothing" +
                                "nothin that looks forilar. After a few minutes you notice something in your pocket. Reaching into your pocket, you pull out a birght" +
                                "red ruby. You start to remember you found this on the side of the trail next to an oak tree. Where am I?",
                    Accessible = true,
                  
                    Message = "You awake to find yourself in a beautiful green forest with a throbbing headache.You are unsure where you are or how you arrived " +
                               "at this spot. The last thing you remember is hiking trough the trails at a local park. As you stand and look around, you see nothing" +
                                "nothin that looks forilar. After a few minutes you notice something in your pocket. Reaching into your pocket, you pull out a birght" +
                                "red ruby. You start to remember you found this on the side of the trail next to an oak tree. Where am I?",





                };

                gameMap.MapLocations[1, 1] = new Location()
                {
                    Id = 1,
                    Name = "Alchemist Cave",
                    Description = "The bustling city street and the heart of New Chicago. bustling hover cars, self-driving ubers, and many people walking around during the day. at night it's not so pretty.",
                    Accessible = false,
                    ModifiyExperiencePoints = 10,
                    RequiredExp = 10,
                    Message = "In the cave you find a jar that is light pink. You are so thirsty at this point. You look around to see other jars in the room. It appears to be an" +
                    "Alchemist Lab. You remember reading about these potion wizzards from a book when you were younger. You come across a note on one of the tables. The note explains" +
                    "why you are here. The Ruby had travled to another world and needed to be returned to its owner at Rockside Castle. You need to find Emerald Beach by a large" +
                    "Field. A boat awaits.The ride will cost 5 Gold Coins. Thip will take you to the CIty of Dell where Rockside Castle awaits.",
                    RequiredClueId = 1003,
                    GameItems = new ObservableCollection<GameItem>
                    {
                        GameItemById(1001)
                        
                    }



                };

                gameMap.MapLocations[1, 0] = new Location()
                {
                    Id = 2,
                    Name = "Emerald Beach",
                    Description = "You have made it to the beach.There are others there and they are running towards you with swords!",
                    Accessible = false,
                    RequiredExp = 30,
                    ModifiyExperiencePoints = 20,
                    ModifyHealth = -20,
                    RequiredClueId = 1001,
                    Message = "I need to board the ship!",
                    GameItems = new ObservableCollection<GameItem>
                    {
                        GameItemById(4001)
                    },
                        Npcs = new ObservableCollection<NPC>()
                    {
                        NpcById(7001),
                        NpcById(7002)
                    }

                };

                gameMap.MapLocations[2, 0] = new Location()
                {
                    Id = 3,
                    Name = "Ship",
                    Description = "Now where is this mysterious man Marty. ",
                    Accessible = false,
                    RequiredExp = 30,
                    ModifiyExperiencePoints = 20,
                    ModifyHealth = -20,
                    Message = "I think I see him. I think I sould go talk to him to figure out my next move.",
                    RequiredClueId = 1001,

                    GameItems = new ObservableCollection<GameItem>
                    {
                        GameItemById(3002),
                        GameItemById(3001),

                    },
                        Npcs = new ObservableCollection<NPC>()
                    {
                        NpcById(5010)
                       
                    }


                };

                gameMap.MapLocations[1, 2] = new Location()
                {
                    Id = 4,
                    Name = "Dell City",
                    Description = "You have made it to the city on the water. Rockside Castle is in sight. ",
                    Accessible = false,
                    ModifiyExperiencePoints = 10,
                    RequiredExp = 50,
                    Message = "Where to begin. I need to find the Blacksmith.",
                    GameItems = new ObservableCollection<GameItem>
                {
                   GameItemById(2001),
                   GameItemById(1002)

                }
                };
                gameMap.MapLocations[2, 1] = new Location()
                {
                    Id = 5,
                    Name = "Castle Tunnels",
                    Description = "You have arrived in the dark cold tunnels of the castle. The walls are rock and there is a musty smell.",
                    Accessible = false,
                    ModifiyExperiencePoints = 10,
                    ModifyHealth = -20,
                    Message = "On to find the door to the secret room. It sounds like someone is coming...",
                    RequiredClueId = 1002,
                    RequiredExp = 50,
                    GameItems = new ObservableCollection<GameItem>
                {
                    GameItemById(4001)
                },
                    Npcs = new ObservableCollection<NPC>()
                {
                    NpcById(5002),
                    NpcById(5005)
                }

                };
                gameMap.MapLocations[2, 3] = new Location()
                {
                    Id = 6,
                    Name = "King's Secret Room",
                    Description = "Secret room under the castle.",
                    Accessible = false,
                    ModifiyExperiencePoints = 60,
                    Message = "I have made it to the room and the mad King is in sight!",
                    RequiredClueId = 1002,
                    Npcs = new ObservableCollection<NPC>()
                {
                    NpcById(5004),
                    NpcById(5006),
                    NpcById(7006)
                }
                };


                return gameMap;
            };


        }

        public static List<GameItem> StandardGameItems()
        {

            return new List<GameItem>()
                {
                    new Weapon(2001, "Sword", 500, 1,20,5, "An ancient sword that can defeat the mad King",50),
                    new Weapon(2002, "Magic Sword", 750, 1, 30, 10, "Ultimate Sword", 50),
                    new Weapon (2003, "Pocket Knife", 10, 1,5,0,"small Pocket knife",0),
                    new Weapon(2004, "Axe", 20,1,15,0, "Axe made from wood and stone",10),
                    new Potion(3001, "Willow Potion",20, 25,0,"Potion made from the mysterious cave Alchemist.",0),
                    new Potion(3002, "Hardy Meal",10, 15,0,"Finally a good meal.",0),
                    new Coin(4001, "Gold",10,Coin.CoinType.Gold,"The highest value Coin",0),
                    new Clues(1001,"Alchemist Note",0,"A note in the Alchemist cave.",10,
                    "The note says that I need to find a ship to sail to Dell City from Emerald Beach. A man by the name of Tulum will tell me how to access the castle to kill the King."+
                    "\n\n{Emerald Beach Unlocked}",Clues.UseActionType.OPENLOCATION),
                    new Clues(1002, "Secret Room Key",100,"The key to the mad King's secret Room.",10,
                    "I have the key to the secret room. Now I just need to find it in this castle. Lets hope I don't run into trouble, " +
                    "\n\n{King's Secret Room Unlocked}",Clues.UseActionType.OPENLOCATION),
                    new Clues(1003, "Mysterious Note",100,"A mysterious note in my pocket. I should probably read it.",10,
                    "I have been sent to this world to kill the mad King so the Ruby can be given to rightful Ruler. I need to make my way to a cave that will have items needed for my journey."+
                     "\n\n{Alchemist Cave Unlocked}",Clues.UseActionType.OPENLOCATION),



                };
        }
        public static List<NPC> Npcs()
        {
            return new List<NPC>()
            {
                new Enemy()
                {
                    Id = 7001,
                    Name = "Thief",
                    Race = Character.RaceType.Human,
                    Health = 25,
                    Description = "Thief on the Beach",
                    Messages = new List<string>()
                    {
                        "Your gold is mine!",
                
                    },
                   SkillLevel = 1,
                   CurrentWeapon = GameItemById(2003) as Weapon,
                   Loot=GameItemById(4001)
                   
                },
                                
                
                    new Enemy()
                {
                    Id = 7002,
                    Name = "Thief",
                    Race = Character.RaceType.Human,
                    Health = 25,
                    Description = "Theif on the Beach.",
                    Messages = new List<string>()
                    {
                        "Lets see what you got!",
                        
                    },
                   SkillLevel = 1,
                   CurrentWeapon = GameItemById(2003) as Weapon,
                   Loot=GameItemById(3001)

                },
               
                new Enemy()
                {
                    Id=7004,
                    Name="Solider",
                    Health=50,


                    Race = Character.RaceType.Human,
                    Description="King's Solider",
                    Messages = new List<string>()
                    {
                        "You can't get past me!"

                    },
                    SkillLevel=2,
                    CurrentWeapon = GameItemById(2001) as Weapon,
                    Loot= GameItemById(3001),
                },

                         new Enemy()
                {
                    Id=7005,
                    Name="Solider",
                    Health=50,


                    Race = Character.RaceType.Human,
                    Description="King's Solider",
                    Messages = new List<string>()
                    {
                        "You think you can pass me?",
                        "Let me see you try!"

                    },
                    SkillLevel=3,
                    CurrentWeapon = GameItemById(2001) as Weapon
                },
                new Enemy()
                {
                    Id=7006,
                    Name="Mad King",
                    Health=60,
                    Race= Character.RaceType.Human,
                    Description="He seems like he knows a little too much",
                    Messages = new List<string>()
                    {
                        "What are you doing in here?",
                        "Don't come any closer. I mean it!",
                        "You will pay for this!"
                    },
                    SkillLevel=5,
                    CurrentWeapon = GameItemById(2001) as Weapon,
                    Loot=GameItemById(2001)

                },
                new Friend()
                {
                    Id = 5010,
                    Name = "Marty Stell",
                    Race = Character.RaceType.Human,
                    Description = "A mysterious man sitting on the edge of the ship",
                    Messages = new List<string>()
                    {
                        "I have been waiting for you.",
                        "WE are heading to Dell City. You need to find the Black Smith Roger Dell. He will give you a magic sword and a key. ",
                        "The Key will open the door to the Kings Secret room. He has been hiding out in there since he heard someone was here with the Ruby."
                    }
                },

                new Friend()
                {
                    Id = 5020,
                    Name = "Roger Dell",
                    Race = Character.RaceType.Human,
                    Description = "The Blacksmith that has the key and magic sword",
                    Messages = new List<string>()
                    {
                        "I heard that you would be arriving soon",
                        "Take this sword. It has a magical power that will kill the King. We have heard that the King may not be human. Many people hacve tried to kill him in the past.",
                        "It is now up to you. Good luck my friend."
                    }
                },

                new Friend()
                {
                   Id=5030,
                   Name="",
                   Race = Character.RaceType.Human,
                   Description = "A women who is walking in the woods.",
                   Messages = new List<string>()
                   {
                       "The mad king is crazy. Some say he cannot be killed with a normal blade. There has been rumor that there is one in Dell City",
                       "We all want him dead. The rightful King will be able to be King again. Our world return to normal."

                   }
                }
            };
        }
    }





}


