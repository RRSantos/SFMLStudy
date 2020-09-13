using SFML.Graphics;
using SFML.System;
using System;

namespace DrawingStudy.Draws
{
    
    enum CellSide:uint {Top = 0, Right = 1, Bottom = 2, Left = 3};
    class MazeCell
    {
        private readonly Color EDGE_COLOR = new Color(170, 150, 170);

        private readonly uint line;
        private readonly uint column;
        private readonly uint cellWidth;
        private readonly VertexArray cellShape;
        private readonly RectangleShape hightlightedCell;        
        
        private VertexArray[] allCellSides;
        private bool[] cellSidesMap;

        public bool IsHighlighted { get; set; }

        public bool WasVisited { get; set; }

        private void initializeAllCellSides()
        {
            allCellSides = new VertexArray[4];

            VertexArray topSide = new VertexArray(PrimitiveType.Points, 2);
            topSide[0] = new Vertex(new Vector2f(line * cellWidth, column * cellWidth), EDGE_COLOR);
            topSide[1] = new Vertex(new Vector2f(line * cellWidth + cellWidth, column * cellWidth), EDGE_COLOR);
            allCellSides[(uint)CellSide.Top] = topSide;


            VertexArray rightSide = new VertexArray(PrimitiveType.Points, 2);
            rightSide[0] = new Vertex(new Vector2f(line * cellWidth + cellWidth, column * cellWidth), EDGE_COLOR);
            rightSide[1] = new Vertex(new Vector2f(line * cellWidth + cellWidth, column * cellWidth + cellWidth), EDGE_COLOR);
            allCellSides[(uint)CellSide.Right] = rightSide;

            VertexArray bottomSide = new VertexArray(PrimitiveType.Points, 2);
            bottomSide[0] = new Vertex(new Vector2f(line * cellWidth + cellWidth, column * cellWidth + cellWidth), EDGE_COLOR);
            bottomSide[1] = new Vertex(new Vector2f(line * cellWidth, column * cellWidth + cellWidth), EDGE_COLOR);
            allCellSides[(uint)CellSide.Bottom] = bottomSide;

            VertexArray leftSide = new VertexArray(PrimitiveType.Points, 2);
            leftSide[0] = new Vertex(new Vector2f(line * cellWidth, column * cellWidth + cellWidth), EDGE_COLOR);
            leftSide[1] = new Vertex(new Vector2f(line * cellWidth, column * cellWidth), EDGE_COLOR);
            allCellSides[(uint)CellSide.Left] = leftSide;
        }

        private void initializeCellSidesMap()
        {
            cellSidesMap = new bool[4];
            cellSidesMap[(uint)CellSide.Top] = true;
            cellSidesMap[(uint)CellSide.Right] = true;
            cellSidesMap[(uint)CellSide.Bottom] = true;
            cellSidesMap[(uint)CellSide.Left] = true;
        }

        public MazeCell(uint line, uint column, uint cellWidth)
        {   
            this.line = line;
            this.column = column;
            this.cellWidth = cellWidth;
            IsHighlighted = false;
            WasVisited  = false;
            hightlightedCell = new RectangleShape(new Vector2f(cellWidth, cellWidth))
            {
                Position = new Vector2f(line * cellWidth, column * cellWidth),
                FillColor = Color.Green,
                OutlineColor = Color.Transparent
            };
            initializeCellSidesMap();
            initializeAllCellSides();
            cellShape = new VertexArray(PrimitiveType.Lines);
            updateCellShape();
            
        }

        

        public void RemoveCellSide(CellSide cellSide)
        {
            cellSidesMap[(uint)cellSide] = false;
            updateCellShape();
        }

        private void updateCellShape()
        {
            cellShape.Clear();            
            
            for (int i = 0; i < cellSidesMap.Length; i++)
            {
                if (cellSidesMap[i])
                {
                    for (uint j = 0; j < allCellSides[i].VertexCount; j++)
                    {
                        cellShape.Append(allCellSides[i][j]);
                    }
                }
            }
        }

        public void Draw(RenderWindow window)
        {   
            if (IsHighlighted)
            {
                hightlightedCell.FillColor = Color.Green;
                window.Draw(hightlightedCell);
            }
            else if (WasVisited)
            {
                hightlightedCell.FillColor = new Color(40, 0, 40);
                window.Draw(hightlightedCell);
            }

            window.Draw(cellShape);

        }
    }
}
