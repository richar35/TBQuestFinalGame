using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace TBQuestFinalGame.Models
{ 
public class Map
{
    #region FIELDS
    private Location[,] _mapLocations;
    private int _maxRows, _maxColumns;
    private GameMapCoordinates _currentLocationCoordinates;
    private List<GameItem> _standardGameItems;


    #endregion


    #region PROPERTIES
    public List<GameItem> StandardGameItems
    {
        get { return _standardGameItems; }
        set { _standardGameItems = value; }
    }

    public Location[,] MapLocations
    {
        get { return _mapLocations; }
        set { _mapLocations = value; }
    }

    public GameMapCoordinates CurrentLocationCoordinates
    {
        get { return _currentLocationCoordinates; }
        set { _currentLocationCoordinates = value; }
    }

    public Location CurrentLocation
    {
        get { return _mapLocations[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column]; }
    }

    #endregion

    #region CONSTRUCTORS
    public Map(int rows, int columns)
    {
        _maxRows = rows;
        _maxColumns = columns;
        _mapLocations = new Location[rows, columns];
    }

    #endregion

    #region METHODS

    public void MoveNorth()
    {

        if (_currentLocationCoordinates.Row > 0)
        {
            _currentLocationCoordinates.Row -= 1;
        }
    }

    public void MoveEast()
    {

        if (_currentLocationCoordinates.Column < _maxColumns - 1)
        {
            _currentLocationCoordinates.Column += 1;
        }
    }

    public void MoveSouth()
    {
        if (_currentLocationCoordinates.Row < _maxRows - 1)
        {
            _currentLocationCoordinates.Row += 1;
        }
    }

    public void MoveWest()
    {

        if (_currentLocationCoordinates.Column > 0)
        {
            _currentLocationCoordinates.Column -= 1;
        }
    }


    public Location NorthLocation(Player player)
    {
        Location northLocation = null;


        if (_currentLocationCoordinates.Row > 0)
        {
            Location nextNorthLocation = _mapLocations[_currentLocationCoordinates.Row - 1, _currentLocationCoordinates.Column];


            if (nextNorthLocation != null &&
                (nextNorthLocation.Accessible == true || nextNorthLocation.IsAccessibleByExperiencePoints(player.ExpierencePoints)))
                {
                northLocation = nextNorthLocation;
            }
        }

        return northLocation;
    }


    public Location EastLocation(Player player)
    {
        Location eastLocation = null;


        if (_currentLocationCoordinates.Column < _maxColumns - 1)
        {
            Location nextEastLocation = _mapLocations[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column + 1];

            if (nextEastLocation != null &&
                (nextEastLocation.Accessible == true || nextEastLocation.IsAccessibleByExperiencePoints(player.ExpierencePoints)))
            {
                eastLocation = nextEastLocation;
            }
        }

        return eastLocation;
    }


    public Location SouthLocation(Player player)
    {
        Location southLocation = null;


        if (_currentLocationCoordinates.Row < _maxRows - 1)
        {
            Location nextSouthLocation = _mapLocations[_currentLocationCoordinates.Row + 1, _currentLocationCoordinates.Column];

            if (nextSouthLocation != null &&
                (nextSouthLocation.Accessible == true || nextSouthLocation.IsAccessibleByExperiencePoints(player.ExpierencePoints)))
            {
                southLocation = nextSouthLocation;
            }
        }

        return southLocation;
    }


    public Location WestLocation(Player player)
    {
        Location westLocation = null;


        if (_currentLocationCoordinates.Column > 0)
        {
            Location nextWestLocation = _mapLocations[_currentLocationCoordinates.Row, _currentLocationCoordinates.Column - 1];


            if (nextWestLocation != null &&
                (nextWestLocation.Accessible == true || nextWestLocation.IsAccessibleByExperiencePoints(player.ExpierencePoints)))
            {
                westLocation = nextWestLocation;
            }
        }

        return westLocation;
    }
    #endregion
    #region ACTION METHODS

    public string OpenLocationsByClue(int requiredClueId)
    {
        string message = "The Clue  doesn't tell us much.";
        Location mapLocation = new Location();

        for (int row = 0; row < _maxRows; row++)
        {
            for (int column = 0; column < _maxColumns; column++)
            {
                mapLocation = _mapLocations[row, column];

                if (mapLocation != null && mapLocation.RequiredClueId == requiredClueId)
                {
                    mapLocation.Accessible = true;
                    message = $"{mapLocation.Name} is now accessible.";
                }
            }
        }

        return message;
    }
    public Location MovePlayerFromClue(int clueID)
    {
        Location mapLocation = new Location();
        for (int row = 0; row < _maxRows; row++)
        {
            for (int column = 0; column < _maxColumns; column++)
            {
                mapLocation = _mapLocations[row, column];
                if (mapLocation != null && mapLocation.RequiredClueId == clueID)
                {
                    mapLocation = _mapLocations[2, 3];
                };
            }

        }
        return mapLocation;
    }
    #endregion
}
}
