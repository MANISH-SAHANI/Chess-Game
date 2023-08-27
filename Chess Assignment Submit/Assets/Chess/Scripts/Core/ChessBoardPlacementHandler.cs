using System;
using UnityEngine;
using System.Diagnostics.CodeAnalysis;
using Chess.Scripts.Core;
using System.Security.Cryptography;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public sealed class ChessBoardPlacementHandler : MonoBehaviour {
    [SerializeField] private GameObject[] _rowsArray;
    [SerializeField] private GameObject _enemyhighlightPrefab;
    [SerializeField] private GameObject _highlightPrefab;
    private GameObject[,] _chessBoard;

    [SerializeField] public int[,] piecePosition = new int[8, 8]; //Pieces Positions
    [SerializeField] public int[,] piecePositionType = new int[8, 8]; //Piece Type ,i.e, White Black

    internal static ChessBoardPlacementHandler Instance;

    private void Awake() {
        Instance = this;
        GenerateArray();
    }

    private void GenerateArray() {
        _chessBoard = new GameObject[8, 8];
        for (var i = 0; i < 8; i++) {
            for (var j = 0; j < 8; j++) {
                _chessBoard[i, j] = _rowsArray[i].transform.GetChild(j).gameObject;
            }
        }
    }

    //Marking Occupied Places
    internal void PlaceOccuied(int row, int column,int type) {
        piecePosition[row, column] = 1;
        piecePositionType[row, column] = type;
    }

    //Checking for enemy pieces
    public int EnemyCheck(int row, int column, int type) {
        int enemyType = 0;
        if(type == 1) {
            enemyType = 2;
        }
        else if(type == 2) {
            enemyType = 1;
        }

       if(piecePositionType[row, column] == 0) {
            return 0; //null position
       }
       else if (piecePositionType[row, column] == enemyType) {
            return 2; //enemy
       }
       else {
            return 1; //friend
       }
    }

    //Highligiting moves according to enemy and our piece 
    internal void HighlightMove(int value, int row, int column) {
        if(value == 2) {
            EnemyHighlight(row, column);
        }
        else if(value == 0) {
            Highlight(row, column);
        }
    }
    
    //Checking for occupied space
    internal bool ShowPlaceOccuied(int row, int column) {
        if ((row <= 7 && column <= 7) && (row >= 0 && column >= 0)) {
            if (piecePosition[row, column] == 1) {
                return true; //1 true then occuied
            }
            else {
                return false; //0 flase then not occuied
            }
        }
        else {
            return false;
        }
    }

    //Validating Moves and Higlighting Moves
    public void CheckValidMoves(int row, int coloumn, int type) {
        if((row <= 7 && coloumn <= 7) && (row >= 0 && coloumn >= 0)) {
            HighlightMove(EnemyCheck(row,coloumn,type),row,coloumn);
        }
    }

    internal GameObject GetTile(int i, int j) {
        try {
            return _chessBoard[i, j];
        } catch (Exception) {
            Debug.LogError("Invalid row or column.");
            return null;
        }
    }

    internal void Highlight(int row, int col) {
        var tile = GetTile(row, col).transform;
        if (tile == null) {
            Debug.LogError("Invalid row or column.");
            return;
        }

        Instantiate(_highlightPrefab, tile.transform.position, Quaternion.identity, tile.transform);

    }
    
    //Enemy Highlight Prefab (RedColor)
    internal void EnemyHighlight(int row, int col) {
        var tile = GetTile(row, col).transform;
        if (tile == null) {
            Debug.LogError("Invalid row or column.");
            return;
        }

        Instantiate(_enemyhighlightPrefab, tile.transform.position, Quaternion.identity, tile.transform);
        
    }

    internal void ClearHighlights() {
        for (var i = 0; i < 8; i++) {
            for (var j = 0; j < 8; j++) {
                var tile = GetTile(i, j);
                if (tile.transform.childCount <= 0) continue;
                foreach (Transform childTransform in tile.transform) {
                    Destroy(childTransform.gameObject);
                }
            }
        }
    }


    #region Highlight Testing

    // private void Start() {
    //     StartCoroutine(Testing());
    // }

    // private IEnumerator Testing() {
    //     Highlight(2, 7);
    //     yield return new WaitForSeconds(1f);
    //
    //     ClearHighlights();
    //     Highlight(2, 7);
    //     Highlight(2, 6);
    //     Highlight(2, 5);
    //     Highlight(2, 4);
    //     yield return new WaitForSeconds(1f);
    //
    //     ClearHighlights();
    //     Highlight(7, 7);
    //     Highlight(2, 7);
    //     yield return new WaitForSeconds(1f);
    // }

    #endregion
}