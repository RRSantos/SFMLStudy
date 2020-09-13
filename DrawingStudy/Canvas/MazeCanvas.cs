using SFML.Graphics;
using System;
using System.Collections.Generic;
using DrawingStudy.Draws;
using SFMLCanvas;

namespace DrawingStudy.Canvas
{
    public class MazeCanvas : AbstractCanvas
    {
        
        private const uint SCREEN_WIDTH = 880;
        private const uint SCREEN_HEIGHT = 880;
        private const string WINDOW_TITLE = "Maze Canvas";
        private const uint CELL_WIDTH = 88;

        private readonly uint Cols;
        private readonly Random random = new Random();
        private readonly uint Rows;
        private readonly List<MazeCell> mazeCells;
        private readonly Stack<int> cellsIndexStack; 
        private MazeCell currentCell;
        private int indexCurrentCell;


        private void visitNeighbor()
        {
            int unvistedNeighbourIndex = chooseUnvistedNeighbourIndex();
            if (unvistedNeighbourIndex > -1)
            {
                int indexDiff = indexCurrentCell - unvistedNeighbourIndex;
                if (indexDiff == 1)
                {
                    mazeCells[indexCurrentCell].RemoveCellSide(CellSide.Left);
                    mazeCells[unvistedNeighbourIndex].RemoveCellSide(CellSide.Right);
                }
                else if (indexDiff == -1)
                {
                    mazeCells[indexCurrentCell].RemoveCellSide(CellSide.Right);
                    mazeCells[unvistedNeighbourIndex].RemoveCellSide(CellSide.Left);
                }
                else if (indexDiff > 1)
                {
                    mazeCells[indexCurrentCell].RemoveCellSide(CellSide.Top);
                    mazeCells[unvistedNeighbourIndex].RemoveCellSide(CellSide.Bottom);
                }
                else if (indexDiff < -1)
                {
                    mazeCells[indexCurrentCell].RemoveCellSide(CellSide.Bottom);
                    mazeCells[unvistedNeighbourIndex].RemoveCellSide(CellSide.Top);
                }

                indexCurrentCell = unvistedNeighbourIndex;
            }
            else if (cellsIndexStack.Count > 0)
            {
                indexCurrentCell = cellsIndexStack.Pop();
            }
        }

        private Tuple<int, int> getRandomNeighbor(Tuple<int, int> originalBidimensionalIndex)
        {
            int direction = random.Next(4);

            Tuple<int, int> newBidimensionalIndex;
            if (direction == 0)
            {
                newBidimensionalIndex = new Tuple<int, int>(originalBidimensionalIndex.Item1, originalBidimensionalIndex.Item2 - 1);
            }
            else if (direction == 1)
            {
                newBidimensionalIndex = new Tuple<int, int>(originalBidimensionalIndex.Item1 + 1, originalBidimensionalIndex.Item2);
            }
            else if (direction == 2)
            {
                newBidimensionalIndex = new Tuple<int, int>(originalBidimensionalIndex.Item1, originalBidimensionalIndex.Item2 + 1);
            }
            else
            {
                newBidimensionalIndex = new Tuple<int, int>(originalBidimensionalIndex.Item1 - 1, originalBidimensionalIndex.Item2);
            }

            return newBidimensionalIndex;
        }

        private int chooseUnvistedNeighbourIndex()
        {

            Tuple<int, int> bidimensionalIndex = getBidimensionalIndexFromUnidimensionalList();
            Tuple<int, int> randomNeighbor;
            do
            {
                randomNeighbor = getRandomNeighbor(bidimensionalIndex);

            } while (isOutsideBounds(randomNeighbor));

            int newIndex = transformToUnidimensionalIndex(randomNeighbor);
            if (!mazeCells[newIndex].WasVisited)
                return newIndex;

            randomNeighbor = new Tuple<int, int>(bidimensionalIndex.Item1, bidimensionalIndex.Item2 + 1);
            if (!isOutsideBounds(randomNeighbor))
            {
                newIndex = transformToUnidimensionalIndex(randomNeighbor);
                if (!mazeCells[newIndex].WasVisited)
                    return newIndex;
            }

            randomNeighbor = new Tuple<int, int>(bidimensionalIndex.Item1 - 1, bidimensionalIndex.Item2);
            if (!isOutsideBounds(randomNeighbor))
            {
                newIndex = transformToUnidimensionalIndex(randomNeighbor);
                if (!mazeCells[newIndex].WasVisited)
                    return newIndex;
            }

            randomNeighbor = new Tuple<int, int>(bidimensionalIndex.Item1, bidimensionalIndex.Item2 - 1);
            if (!isOutsideBounds(randomNeighbor))
            {
                newIndex = transformToUnidimensionalIndex(randomNeighbor);
                if (!mazeCells[newIndex].WasVisited)
                    return newIndex;
            }

            randomNeighbor = new Tuple<int, int>(bidimensionalIndex.Item1 + 1, bidimensionalIndex.Item2);
            if (!isOutsideBounds(randomNeighbor))
            {
                newIndex = transformToUnidimensionalIndex(randomNeighbor);
                if (!mazeCells[newIndex].WasVisited)
                    return newIndex;
            }

            return -1;
        }

        private bool isOutsideBounds(Tuple<int, int> randomNeighbor)
        {
            return (randomNeighbor.Item1 >= Rows || randomNeighbor.Item1 < 0 || randomNeighbor.Item2 >= Cols || randomNeighbor.Item2 < 0);
        }

        private int transformToUnidimensionalIndex(Tuple<int, int> bidimensionalIndex)
        {
            return bidimensionalIndex.Item1 + bidimensionalIndex.Item2 * (int)Cols;
        }

        private Tuple<int, int> getBidimensionalIndexFromUnidimensionalList()
        {
            int i = indexCurrentCell % (int)Cols;
            int j = indexCurrentCell / (int)Cols;
            Tuple<int, int> bidimensionalIndex = new Tuple<int, int>(i, j);

            return bidimensionalIndex;
        }

        private void disableHighlightCurrentCell()
        {
            if (currentCell != null)
            {
                currentCell.IsHighlighted = false;
            }
        }


        public MazeCanvas() : base(SCREEN_WIDTH, SCREEN_HEIGHT, WINDOW_TITLE)
        {
            Cols = SCREEN_WIDTH / CELL_WIDTH;
            Rows = SCREEN_HEIGHT / CELL_WIDTH;
            mazeCells = new List<MazeCell>();     
            cellsIndexStack = new Stack<int>();
        }
        protected override void Draw()
        {
            foreach (MazeCell cell in mazeCells)
            {
                cell.Draw(window);
            }
            
        }

        protected override void Update()
        {

            disableHighlightCurrentCell();
            currentCell = mazeCells[indexCurrentCell];
            currentCell.IsHighlighted = true;
            if (!currentCell.WasVisited)
            {
                cellsIndexStack.Push(indexCurrentCell);
                currentCell.WasVisited = true;                
            }

            visitNeighbor();

        }        

        protected override void Setup()
        {   
            this.backgroundColor = new Color(0, 0, 180);
            window.SetFramerateLimit(10);

            for (uint j = 0; j < Cols; j++) 
            {
                for (uint i = 0; i < Rows; i++)
                {
                    MazeCell cell = new MazeCell(i, j, CELL_WIDTH);
                    mazeCells.Add(cell);
                }
            }
            indexCurrentCell = 0;            

        }
        
    }
}
