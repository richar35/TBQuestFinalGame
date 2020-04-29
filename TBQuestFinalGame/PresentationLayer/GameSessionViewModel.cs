using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBQuestFinalGame.Models;
using TBQuestFinalGame;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;

namespace TBQuestFinalGame.PresentationLayer
{
    public class GameSessionViewModel : ObservableObject
    {
        #region FIELDS

        private DateTime _gameStartTime;
        private Player _player;
        private Map _gameMap;
        private Location _currentLocation;
        private Location _northLocation, _eastLocation, _southLocation, _westLocation;
        private GameItem _currentGameItem;
        private string _currentLocationInformation;
        protected Random random = new Random();
        private NPC _currentNpc;

        #endregion

        #region PROPERTIES 
        public GameItem CurrentGameItem
        {
            get { return _currentGameItem; }
            set
            {
                _currentGameItem = value;
                OnPropertyChanged(nameof(CurrentGameItem));
                if (_currentGameItem != null && _currentGameItem.GameItems is Weapon)
                {
                    _player.CurrentWeapon = _currentGameItem.GameItems as Weapon;
                }
            }

        }

        public NPC CurrentNpc
        {
            get { return _currentNpc; }
            set
            {
                _currentNpc = value;
                OnPropertyChanged(nameof(CurrentNpc));
            }
        }

        public Player Player
        {
            get { return _player; }
            set { _player = value; }
        }

        public string MessageDisplay
        {
            get { return _currentLocation.Message; }
        }


        public Map GameMap
        {
            get { return _gameMap; }
            set { _gameMap = value; }
        }

        public Location CurrentLocation
        {
            get { return _currentLocation; }
            set
            {
                _currentLocation = value;
                OnPropertyChanged(nameof(CurrentLocation));
                _currentLocationInformation = _currentLocation.Message;
                OnPropertyChanged(nameof(CurrentLocationInformation));
            }
        }


        public Location NorthLocation
        {
            get { return _northLocation; }
            set
            {
                _northLocation = value;
                OnPropertyChanged(nameof(NorthLocation));
                OnPropertyChanged(nameof(HasNorthLocation));
            }
        }

        public Location EastLocation
        {
            get { return _eastLocation; }
            set
            {
                _eastLocation = value;
                OnPropertyChanged(nameof(EastLocation));
                OnPropertyChanged(nameof(HasEastLocation));
            }
        }

        public Location SouthLocation
        {
            get { return _southLocation; }
            set
            {
                _southLocation = value;
                OnPropertyChanged(nameof(SouthLocation));
                OnPropertyChanged(nameof(HasSouthLocation));
            }
        }

        public Location WestLocation
        {
            get { return _westLocation; }
            set
            {
                _westLocation = value;
                OnPropertyChanged(nameof(WestLocation));
                OnPropertyChanged(nameof(HasWestLocation));
            }
        }

        public string CurrentLocationInformation
        {
            get { return _currentLocationInformation; }
            set
            {
                _currentLocationInformation = value;
                OnPropertyChanged(nameof(CurrentLocationInformation));
            }
        }

        public bool HasNorthLocation
        {
            get
            {
                if (NorthLocation != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool HasEastLocation { get { return EastLocation != null; } }

        public bool HasSouthLocation { get { return SouthLocation != null; } }

        public bool HasWestLocation { get { return WestLocation != null; } }

        #endregion

        #region CONSTRUCTORS

        public GameSessionViewModel()
        {

        }

        public GameSessionViewModel(Player player, Map gameMap, GameMapCoordinates currentLocationCoordinates)
        {
            _player = player;
            _gameMap = gameMap;
            _gameMap.CurrentLocationCoordinates = currentLocationCoordinates;
            _currentLocation = _gameMap.CurrentLocation;

            InitializeView();
        }

        #endregion

        #region METHODS

        private void InitializeView()
        {
            _gameStartTime = DateTime.Now;
            UpdateAvailableTravelPoints();
            _player.UpdateInventoryCategories();
            _player.CalculateWealth();
            _currentLocationInformation = CurrentLocation.Description;

        }

        private TimeSpan GameTime()
        {
            return DateTime.Now - _gameStartTime;
        }

        private void UpdateAvailableTravelPoints()
        {

            NorthLocation = null;
            EastLocation = null;
            SouthLocation = null;
            WestLocation = null;

            if (_gameMap.NorthLocation(_player) != null)
            {
                NorthLocation = _gameMap.NorthLocation(_player);
            }

            if (_gameMap.EastLocation(_player) != null)
            {
                EastLocation = _gameMap.EastLocation(_player);
            }

            if (_gameMap.SouthLocation(_player) != null)
            {
                SouthLocation = _gameMap.SouthLocation(_player);
            }

            if (_gameMap.WestLocation(_player) != null)
            {
                WestLocation = _gameMap.WestLocation(_player);
            }
        }

        private void OnPlayerMove()
        {

            if (!_player.HasVisited(_currentLocation))
            {
                _player.LocationsVisited.Add(_currentLocation);


                _player.ExpierencePoints += _currentLocation.ModifiyExperiencePoints;


                if (_currentLocation.ModifyHealth != 0)
                {
                    _player.Health += _currentLocation.ModifyHealth;
                    if (_player.Health > 100)
                    {
                        _player.Health = 100;
                        _player.Lives++;
                    }
                }


                if (_currentLocation.ModifyLives != 0) _player.Lives += _currentLocation.ModifyLives;

                OnPropertyChanged(nameof(MessageDisplay));
            }
        }

        public void MoveNorth()
        {
            if (HasNorthLocation)
            {
                _gameMap.MoveNorth();
                CurrentLocation = _gameMap.CurrentLocation;
                UpdateAvailableTravelPoints();
                OnPlayerMove();
            }
        }

        public void MoveEast()
        {
            if (HasEastLocation)
            {
                _gameMap.MoveEast();
                CurrentLocation = _gameMap.CurrentLocation;
                UpdateAvailableTravelPoints();
                OnPlayerMove();
            }
        }

        public void MoveSouth()
        {
            if (HasSouthLocation)
            {
                _gameMap.MoveSouth();
                CurrentLocation = _gameMap.CurrentLocation;
                UpdateAvailableTravelPoints();
                OnPlayerMove();
            }
        }

        public void MoveWest()
        {
            if (HasWestLocation)
            {
                _gameMap.MoveWest();
                CurrentLocation = _gameMap.CurrentLocation;
                UpdateAvailableTravelPoints();
                OnPlayerMove();
            }
        }

        #endregion
        #region ACTION METHODS
        public void AddItemToInventory()
        {
            //
            // confirm a game item selected and is in current location
            // subtract from location and add to inventory
            //
            if (_currentGameItem != null && _currentLocation.GameItems.Contains(_currentGameItem))
            {
                //
                // cast selected game item 
                //
                GameItem selectedGameItem = _currentGameItem as GameItem;

                _currentLocation.RemoveGameItemFromLocation(selectedGameItem);
                _player.AddGameItemToInventory(selectedGameItem);

                OnPlayerPickUp(selectedGameItem);
            }
        }

        public void RemoveItemFromInventory()
        {
            //
            // confirm a game item selected and is in inventory
            // subtract from inventory and add to location
            //
            if (_currentGameItem != null)
            {
                //
                // cast selected game item 
                //
                GameItem selectedGameItem = _currentGameItem as GameItem;

                _currentLocation.AddGameItemToLocation(selectedGameItem);
                _player.RemoveGameItemFromInventory(selectedGameItem);

                OnPlayerPutDown(selectedGameItem);
            }
        }

        private void OnPlayerPickUp(GameItem gameItem)
        {
            _player.ExpierencePoints += gameItem.ExperiencePoints;
            _player.Wealth += gameItem.Value;
        }

        private void OnPlayerPutDown(GameItem gameItem)
        {
            _player.Wealth -= gameItem.Value;
        }

        public void OnPlayerTalkTo()
        {
            if (CurrentNpc != null && CurrentNpc is ISpeak)
            {
                ISpeak speakingNpc = CurrentNpc as ISpeak;
                CurrentLocationInformation = speakingNpc.Speak();
            }
        }

        public void OnUseGameItem()
        {
            switch (_currentGameItem)
            {
                case Potion mediPack:
                    ProcessMediPackUse(mediPack);
                    break;
                case Clues clues:
                    ProcessClueUse(clues);
                    break;
                case Weapon weapon:
                    ProcessWeaponUse(weapon);
                    break;
                default:
                    break;
            }
        }

        private void ProcessWeaponUse(Weapon weapon)
        {
            if (_currentGameItem != null && _currentGameItem is Weapon)
            {
                _player.CurrentWeapon = _currentGameItem as Weapon;
            }
        }

        private void ProcessClueUse(Clues clues)
        {
            string message;

            switch (clues.UseAction)
            {
                case Clues.UseActionType.OPENLOCATION:
                    message = _gameMap.OpenLocationsByClue(clues.Id);
                    CurrentLocationInformation = clues.UseMessage;
                    break;
                case Clues.UseActionType.MOVEPLAYER:
                    CurrentLocation = _gameMap.MovePlayerFromClue(clues.Id);
                    CurrentLocationInformation = clues.UseMessage;
                    OnPlayerMove();
                    break;
                default:
                    break;
            }
        }


        private void ProcessMediPackUse(Potion mediPack)
        {
            _player.Health += mediPack.HealthChange;
            _player.Lives += mediPack.LivesChange;
            _player.RemoveGameItemFromInventory(_currentGameItem);
        }

        private void OnPlayerDies(string message)
        {
            string messagetext = message +
                "\n\nWould you like to play again?";

            string titleText = "Death";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(messagetext, titleText, button);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    ResetPlayer();
                    break;
                case MessageBoxResult.No:
                    QuiteApplication();
                    break;
            }
        }

        private void OnPlayerWin()
        {
            string messagetext = "\n\nCongratulations!! You won! \nWould you like to play again?";

            string titleText = "Victory";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(messagetext, titleText, button);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    ResetPlayer();
                    break;
                case MessageBoxResult.No:
                    QuiteApplication();
                    break;
            }
        }

        private void QuiteApplication()
        {
            Environment.Exit(0);
        }


        private void ResetPlayer()
        {

            System.Diagnostics.Process.Start(Environment.GetCommandLineArgs()[0]);

        }

        private BattleModeName NpcBattleResponse()
        {
            BattleModeName npcBattleResponse = BattleModeName.RETREAT;

            switch (DieRoll(3))
            {
                case 1:
                    npcBattleResponse = BattleModeName.ATTACK;
                    break;
                case 2:
                    npcBattleResponse = BattleModeName.DEFEND;
                    break;
                case 3:
                    npcBattleResponse = BattleModeName.RETREAT;
                    break;
            }
            return npcBattleResponse;
        }

        private int CalculatePlayerHitPoints()
        {
            int playerHitPoints = 0;

            switch (_player.BattleMode)
            {
                case BattleModeName.ATTACK:
                    playerHitPoints = _player.Attack();

                    break;
                case BattleModeName.DEFEND:
                    playerHitPoints = _player.Defend();

                    break;
                case BattleModeName.RETREAT:
                    playerHitPoints = _player.Retreat();
                    break;

            }

            return playerHitPoints;
        }

        private int CalculateNpcHitPoints(IBattle battleNpc)
        {
            int battleNpcHitPoints = 0;
            if (battleNpc is Enemy /*playerHitPoints >= battleNpcHitPoints*/)
            {
                Enemy battlingNpc = _currentNpc as Enemy;


                switch (NpcBattleResponse())
                {
                    case BattleModeName.ATTACK:
                        battleNpcHitPoints = battleNpc.Attack();

                        break;
                    case BattleModeName.DEFEND:
                        battleNpcHitPoints = battleNpc.Defend();

                        break;
                    case BattleModeName.RETREAT:
                        battleNpcHitPoints = battleNpc.Retreat();
                        break;
                }

            }
            return battleNpcHitPoints;

        }

        private void Battle()
        {

            if (_currentNpc is IBattle)
            {
                IBattle battleNpc = _currentNpc as IBattle;
                int playerHitPoints = 0;
                int battleNpcHitPoints = 0;
                string battleInformation = "";
                if (battleNpc is Enemy)
                {

                    Enemy battlingNpc = _currentNpc as Enemy;
                    if (_player.CurrentWeapon != null)
                    {
                        playerHitPoints = CalculatePlayerHitPoints();
                        battlingNpc.Health -= playerHitPoints;
                        battleInformation = $"you are using {_player.CurrentWeapon.Name} as your weapon." + Environment.NewLine;
                    }
                    else
                    {
                        playerHitPoints = 2;
                        battleInformation = "It appears you are entering into battle without a weapon." + Environment.NewLine;

                    }




                    if (battleNpc.CurrentWeapon != null)
                    {
                        battleNpcHitPoints = CalculateNpcHitPoints(battleNpc);
                        _player.Health -= battleNpcHitPoints;

                    }
                    else
                    {
                        battleInformation = $"It appears you are entering into battle with {_currentNpc.Name} who has no weapon." + Environment.NewLine;
                    }


                    battleInformation +=
                        $"Player: {_player.BattleMode}     Hit Points: {playerHitPoints}" + Environment.NewLine +
                        $"Payer Health: {_player.Health}" + Environment.NewLine +
                    $"NPC: {battleNpc.BattleMode}     Hit Points: {battleNpcHitPoints}" + Environment.NewLine +
                    $"Enemy Health: {battlingNpc.Health}";




                    if (battlingNpc.Health <= 0)
                    {

                        {
                            battleInformation += $"\n\nYou have destroyed {_currentNpc.Name}.";

                            if (_currentNpc is Enemy)
                            {
                                if (battlingNpc.Loot != null)
                                {
                                    _player.Inventory.Add(battlingNpc.Loot);
                                }

                            }
                            _currentLocation.Npcs.Remove(_currentNpc);
                        }
                        if (battlingNpc.Id == 7006 )
                        {

                            OnPlayerWin();

                        }



                    }
                }
                if (_player.Health <= 0)
                {
                    battleInformation += $"\n\nYou have been killed by {_currentNpc.Name}.";
                    _player.Lives--;

                    _player.Health = 100;
                }

                CurrentLocationInformation = battleInformation;
                if (_player.Lives <= 0) OnPlayerDies("You have died. Game Over");
            }
            else
            {
                CurrentLocationInformation = "Don't hit your Friends! \n{-10 XP}";
                _player.ExpierencePoints -= 10;
            }



        }

        public void OnPlayerAttack()
        {
            _player.BattleMode = BattleModeName.ATTACK;
            Battle();
        }

        public void OnPlayerDefend()
        {
            _player.BattleMode = BattleModeName.DEFEND;
            Battle();
        }

        public void OnPlayerRetreat()
        {
            _player.BattleMode = BattleModeName.RETREAT;
            Battle();
        }
        #endregion

        #region HELPER METHODS

        private int DieRoll(int sides)
        {
            return random.Next(1, sides + 1);
        }
        #endregion

        #region EVENTS

        #endregion
    }
}

