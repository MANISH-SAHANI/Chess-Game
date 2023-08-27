using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Chess.Scripts.Core {
    public class ChessPlayerPlacementHandler : MonoBehaviour {
        [SerializeField] public int row, column;

        [SerializeField] public int type; //Black = 1 and White = 2

        private void Start() {
            transform.position = ChessBoardPlacementHandler.Instance.GetTile(row, column).transform.position;
            ChessBoardPlacementHandler.Instance.PlaceOccuied(row, column, type);
        }

        //Mouse Inputs Basesd on Piece Type
        private void OnMouseDown() {
            ChessBoardPlacementHandler.Instance.ClearHighlights();

            switch (this.gameObject.name) {
                case "Pawn":
                    PawnMoves();
                    break;

                case "Knight":
                    KnightMoves();
                    break;

                case "Rook":
                    RookMoves();
                    break;

                case "King":
                    KingMoves();
                    break;

                case "Queen":
                    QueenMoves();
                    break;

                case "Bishop":
                    BishopMoves();
                    break;
            }

        }

        private void PawnMoves() {
            if (ChessBoardPlacementHandler.Instance.ShowPlaceOccuied(row + 1, column) == false) {
                if (ChessBoardPlacementHandler.Instance.ShowPlaceOccuied(row + 2, column) == false) {
                    ChessBoardPlacementHandler.Instance.CheckValidMoves(row + 2, column, type);
                }
                ChessBoardPlacementHandler.Instance.CheckValidMoves(row + 1, column, type);

            }

            if (ChessBoardPlacementHandler.Instance.ShowPlaceOccuied(row + 1, column + 1) == true) {
                ChessBoardPlacementHandler.Instance.CheckValidMoves(row + 1, column + 1, type);
            }

            if (ChessBoardPlacementHandler.Instance.ShowPlaceOccuied(row + 1, column - 1) == true) {
                ChessBoardPlacementHandler.Instance.CheckValidMoves(row + 1, column - 1, type);
            }

        }

        private void KnightMoves() {
            ChessBoardPlacementHandler.Instance.CheckValidMoves(row + 1, column - 2, type);
            ChessBoardPlacementHandler.Instance.CheckValidMoves(row - 1, column - 2, type);
            ChessBoardPlacementHandler.Instance.CheckValidMoves(row + 2, column - 1, type);
            ChessBoardPlacementHandler.Instance.CheckValidMoves(row - 2, column - 1, type);
            ChessBoardPlacementHandler.Instance.CheckValidMoves(row + 2, column + 1, type);
            ChessBoardPlacementHandler.Instance.CheckValidMoves(row - 2, column + 1, type);
            ChessBoardPlacementHandler.Instance.CheckValidMoves(row + 1, column + 2, type);
            ChessBoardPlacementHandler.Instance.CheckValidMoves(row - 1, column + 2, type);
        }

        private void KingMoves() {
            ChessBoardPlacementHandler.Instance.CheckValidMoves(row + 1, column - 1, type);
            ChessBoardPlacementHandler.Instance.CheckValidMoves(row, column - 1, type);
            ChessBoardPlacementHandler.Instance.CheckValidMoves(row - 1, column - 1, type);
            ChessBoardPlacementHandler.Instance.CheckValidMoves(row + 1, column, type);
            ChessBoardPlacementHandler.Instance.CheckValidMoves(row - 1, column, type);
            ChessBoardPlacementHandler.Instance.CheckValidMoves(row + 1, column + 1, type);
            ChessBoardPlacementHandler.Instance.CheckValidMoves(row, column + 1, type);
            ChessBoardPlacementHandler.Instance.CheckValidMoves(row - 1, column + 1, type);

        }
        private void QueenMoves() {
            KingMoves();
            RookMoves();
            BishopMoves();
        }
        private void RookMoves() {

            bool RowDecrementor = false;
            bool RowIncrementor = false;
            bool ColIncrementor = false;
            bool ColDecrementor = false;

            int RowDecrementorCount = 0;
            int RowIncrementorCount = 0;
            int ColIncrementorCount = 0;
            int ColDecrementorCount = 0;

            for (int i = 1; i < 8; i++) {
                if (RowDecrementor == false) {
                    RowDecrementor = ChessBoardPlacementHandler.Instance.ShowPlaceOccuied(row - i, column);
                    RowDecrementorCount++;
                }

                if (RowIncrementor == false) {
                    RowIncrementor = ChessBoardPlacementHandler.Instance.ShowPlaceOccuied(row + i, column);
                    RowIncrementorCount++;
                }

                if (ColDecrementor == false) {
                    ColDecrementor = ChessBoardPlacementHandler.Instance.ShowPlaceOccuied(row, column - i);
                    ColDecrementorCount++;
                }

                if (ColIncrementor == false) {
                    ColIncrementor = ChessBoardPlacementHandler.Instance.ShowPlaceOccuied(row, column + i);
                    ColIncrementorCount++;
                }
            }

            for (int i = 1; i <= RowDecrementorCount; i++) {
                ChessBoardPlacementHandler.Instance.CheckValidMoves(row - i, column, type);
            }

            for (int i = 1; i <= RowIncrementorCount; i++) {
                ChessBoardPlacementHandler.Instance.CheckValidMoves(row + i, column, type);
            }

            for (int i = 1; i <= ColDecrementorCount; i++) {
                ChessBoardPlacementHandler.Instance.CheckValidMoves(row, column - i, type);
            }

            for (int i = 1; i <= ColIncrementorCount; i++) {
                ChessBoardPlacementHandler.Instance.CheckValidMoves(row, column + i, type);
            }

        }
        private void BishopMoves() {

            bool ColRowDecrementor = false;
            bool ColRowIncrementor = false;
            bool ColIncRowDec = false;
            bool ColDecRowInc = false;

            int ColRowDecrementorCount = 0;
            int ColRowIncrementorCount = 0;
            int ColIncRowDecCount = 0;
            int ColDecRowIncCount = 0;

            for (int i = 1; i < 8; i++) {
                if (ColRowDecrementor == false) {
                    ColRowDecrementor = ChessBoardPlacementHandler.Instance.ShowPlaceOccuied(row - i, column - i);
                    ColRowDecrementorCount++;
                }

                if (ColRowIncrementor == false) {
                    ColRowIncrementor = ChessBoardPlacementHandler.Instance.ShowPlaceOccuied(row + i, column + i);
                    ColRowIncrementorCount++;
                }

                if (ColIncRowDec == false) {
                    ColIncRowDec = ChessBoardPlacementHandler.Instance.ShowPlaceOccuied(row - i, column + i);
                    ColIncRowDecCount++;
                }

                if (ColDecRowInc == false) {
                    ColDecRowInc = ChessBoardPlacementHandler.Instance.ShowPlaceOccuied(row + i, column - i);
                    ColDecRowIncCount++;
                }
            }

            for (int i = 1; i <= ColRowDecrementorCount; i++) {
                ChessBoardPlacementHandler.Instance.CheckValidMoves(row - i, column - i, type);
            }

            for (int i = 1; i <= ColRowIncrementorCount; i++) {
                ChessBoardPlacementHandler.Instance.CheckValidMoves(row + i, column + i, type);
            }

            for (int i = 1; i <= ColIncRowDecCount; i++) {
                ChessBoardPlacementHandler.Instance.CheckValidMoves(row - i, column + i, type);
            }

            for (int i = 1; i <= ColDecRowIncCount; i++) {
                ChessBoardPlacementHandler.Instance.CheckValidMoves(row + i, column - i, type);
            }
        }
    }
}
