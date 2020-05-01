using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TBQuestFinalGame.Models;
using TBQuestFinalGame.PresentationLayer;
using TBQuestFinalGame.DataLayer;

namespace TBQuestFinalGame.BusinessLayer
{
    public class GameBusiness
    {
        GameSessionViewModel _gameSessionViewModel;
        Player _player = new Player();
        List<string> _messages;
        Map _gameMap;
        GameMapCoordinates _initialLocationCoordinates;

        public GameBusiness()
        {
            InitializeDataSet();
            InstantiateAndShowView();
        }
        private void InitializeDataSet()
        {
            _player = GameData.PlayerData();
            _messages = GameData.InitialMessages();
            _gameMap = GameData.GameMap();
            _initialLocationCoordinates = GameData.InitialGameMapLocation();
        }

        private void InstantiateAndShowView()
        {
            _gameSessionViewModel = new GameSessionViewModel(
               _player,
               _gameMap,
               _initialLocationCoordinates
               );
            GameSessionView gameSessionView = new GameSessionView(_gameSessionViewModel);

            gameSessionView.DataContext = _gameSessionViewModel;

            gameSessionView.Show();
        }
    }

}