using Newtonsoft.Json;
using System.Data;
using Solver.Sudoku.Models;
using Solver.Sudoku.Helpers;

namespace Solver.Sudoku.Services
{
    public class SolverShape : ISolver
    {
        private readonly List<List<List<Coordinates>>> _shapeFields;
        private readonly List<List<Coordinates>>[,] _shapeFieldsMapping;
        private readonly List<List<List<Coordinates>>>[,] _shapeAreasMapping;
        private const int _prioritySmallAndMany = (int)Priority.Mid;
        private const int _priorityBigOrFew = (int)Priority.Low;

        public SolverShape(List<List<Coordinates>> shapeFields, int rowCnt, int colCnt)
        {
            _shapeFields = new List<List<List<Coordinates>>>();
            _shapeFieldsMapping = new List<List<Coordinates>>[rowCnt, colCnt];
            _shapeAreasMapping = new List<List<List<Coordinates>>>[rowCnt, colCnt];
            for (int i = 1; i <= rowCnt; i++)
            {
                for (int j = 1; j <= colCnt; j++)
                {
                    _shapeFieldsMapping[i - 1, j - 1] = new List<List<Coordinates>>();
                    _shapeAreasMapping[i - 1, j - 1] = new List<List<List<Coordinates>>>();
                }
            }
            _shapeFields = CategorazeShapes(shapeFields);
            foreach (List<List<Coordinates>> shapes in _shapeFields)
            {
                foreach (List<Coordinates> shape in shapes)
                {
                    foreach (Coordinates coordinates in shape)
                    {
                        _shapeFieldsMapping[coordinates.Row - 1, coordinates.Col - 1].Add(shape);
                        _shapeAreasMapping[coordinates.Row - 1, coordinates.Col - 1].Add(shapes);
                    }
                }
            }
        }

        public void SetPriorities(Grid grid, int rowCnt, int colCnt)
        {
            foreach (List<List<Coordinates>> areas in _shapeFields)
            {
                foreach (List<Coordinates> area in areas)
                {
                    if (area.Count <= areas.Count)
                    {
                        foreach (Coordinates coordinates in area)
                            grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Priority += _prioritySmallAndMany;
                    }
                    else
                    {
                        foreach (Coordinates coordinates in area)
                            grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Priority += _priorityBigOrFew;
                    }
                }
            }
        }

        public void BanInitial(Grid grid, int rowCnt, int colCnt, int numRange)
        {
        }

        public void Ban(Grid grid, int row, int rowCnt, int col, int colCnt, int number, int numRange)
        {
            int cnt;
            int num;
            Dictionary<int, int> numbersCurrentArea;
            List<Dictionary<int, int>> numbersRelatedAreas;
            List<List<int>> currentNumbers;
            Dictionary<int, int> numbersRelatedAreasUnionWithoutNumber;
            Dictionary<int, int> numbersRelatedAreasUnion;
            List<int> possibleNumbers;
            for (int k = 0; k < _shapeFieldsMapping[row - 1, col - 1].Count; k++)
            {
                cnt = _shapeFieldsMapping[row - 1, col - 1][0].Count;
                numbersCurrentArea = new Dictionary<int, int>();
                numbersRelatedAreas = new List<Dictionary<int, int>>();
                currentNumbers = new List<List<int>>();
                foreach (Coordinates coordinates in _shapeFieldsMapping[row - 1, col - 1][k])
                {
                    num = grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number;
                    if (numbersCurrentArea.ContainsKey(num))
                        numbersCurrentArea[num] += 1;
                    else
                        numbersCurrentArea[num] = 1;
                }
                foreach (List<Coordinates> area in _shapeAreasMapping[row - 1, col - 1][k])
                {
                    numbersRelatedAreas.Add(new Dictionary<int, int>());
                    currentNumbers.Add(new List<int>());
                    foreach (Coordinates coordinates in area)
                    {
                        num = grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number;
                        if (coordinates.Row == row && coordinates.Col == col)
                        {
                            currentNumbers[^1].Add(num);
                            continue;
                        }
                        currentNumbers[^1].Add(0);
                        if (numbersRelatedAreas[^1].ContainsKey(num))
                            numbersRelatedAreas[^1][num] += 1;
                        else
                            numbersRelatedAreas[^1][num] = 1;
                    }
                }
                numbersRelatedAreasUnionWithoutNumber = UnifyNumbers(numbersRelatedAreas);
                for (int kk = 0; kk < currentNumbers.Count; kk++)
                {
                    for (int kkk = 0; kkk < currentNumbers[kk].Count; kkk++)
                    {
                        if (currentNumbers[kk][kkk] != 0)
                        {
                            if (numbersRelatedAreas[kk].ContainsKey(currentNumbers[kk][kkk]))
                                numbersRelatedAreas[kk][currentNumbers[kk][kkk]] += 1;
                            else
                                numbersRelatedAreas[kk][currentNumbers[kk][kkk]] = 1;
                        }
                    }
                }
                numbersRelatedAreasUnion = UnifyNumbers(numbersRelatedAreas);
                if (numbersRelatedAreasUnion.Values.Sum() < cnt)
                    continue;
                if (numbersRelatedAreasUnionWithoutNumber.Values.Sum() + 1 != numbersRelatedAreasUnion.Values.Sum())
                    continue;
                for (int kk = 0; kk < numbersRelatedAreas.Count; kk++)
                {
                    possibleNumbers = GetPossibleNumbers(numbersRelatedAreasUnion, numbersRelatedAreas[kk]);
                    foreach (Coordinates coordinates in _shapeAreasMapping[row - 1, col - 1][k][kk])
                    {
                        if (grid.Fields[coordinates.Row - 1, coordinates.Col - 1].Number == 0)
                            HelperBan.BanListInverse(grid, coordinates.Row, coordinates.Col, possibleNumbers, numRange);
                    }
                }
            }
        }

        private static List<List<List<Coordinates>>> CategorazeShapes(List<List<Coordinates>> shapeFields)
        {
            bool templateFound;
            Shape shape;
            List<int> shapeTemplate;
            List<List<int>> shapeTemplates = new List<List<int>>();
            List<List<List<Coordinates>>> shapeCategorized = new List<List<List<Coordinates>>>();
            foreach (List<Coordinates> area in shapeFields)
            {
                shape = new Shape(area);
                shapeTemplate = shape.GetShapeTemplate();
                templateFound = false;
                for (int index = 0; index < shapeTemplates.Count; index++)
                {
                    if (JsonConvert.SerializeObject(shapeTemplate) == JsonConvert.SerializeObject(shapeTemplates[index]))
                    {
                        shapeCategorized[index].Add(area);
                        templateFound = true;
                        break;
                    }
                }
                if (!templateFound)
                {
                    shapeCategorized.Add(new List<List<Coordinates>> { area });
                    shapeTemplates.Add(shapeTemplate);
                }
            }
            return shapeCategorized;
        }

        private static Dictionary<int, int> UnifyNumbers(List<Dictionary<int, int>> numbersRelatedAreas)
        {
            Dictionary<int, int> numbersUnified = new Dictionary<int, int>();
            foreach (Dictionary<int, int> numbersRelated in numbersRelatedAreas)
            {
                foreach (int numberRelated in numbersRelated.Keys)
                {
                    if (numberRelated == 0)
                        continue;
                    if (numbersUnified.ContainsKey(numberRelated))
                        numbersUnified[numberRelated] = Math.Max(numbersUnified[numberRelated], numbersRelated[numberRelated]);
                    else
                        numbersUnified[numberRelated] = numbersRelated[numberRelated];
                }
            }
            return numbersUnified;
        }

        private static List<int> GetPossibleNumbers(Dictionary<int, int> allNumbers, Dictionary<int, int> numbersInArea)
        {
            List<int> possibleNumbers = new();
            foreach (int number in allNumbers.Keys)
            {
                if (numbersInArea.ContainsKey(number))
                {
                    if (allNumbers[number] - numbersInArea[number] > 0)
                        possibleNumbers.Add(number);
                }
                else
                    possibleNumbers.Add(number);
            }
            return possibleNumbers;
        }
    }

    internal class Shape
    {
        private List<Coordinates> Area;

        internal Shape(List<Coordinates> area)
        {
            Area = new List<Coordinates>();
            foreach (Coordinates coordinates in area)
                Area.Add(new Coordinates { Row = coordinates.Row, Col = coordinates.Col});
        }

        internal List<int> GetShapeTemplate()
        {
            List<List<Coordinates>> shapeTemplates = new();
            List<int> finalShapeTemplate = new();
            int maxShape = 0;
            Normalize();
            for (int a = 0; a < 2; a++)
            {
                FlipUpsideDown();
                for (int b = 0; b < 4; b++)
                {
                    TurnRight();
                    shapeTemplates.Add(new List<Coordinates>());
                    foreach (Coordinates coordinates in Area)
                        shapeTemplates[^1].Add(new Coordinates { Row = coordinates.Row, Col = coordinates.Col});
                }
            }
            for (int k = 0; k < shapeTemplates.Count; k++)
                shapeTemplates[k] = shapeTemplates[k].OrderBy(x => x.Row).ThenBy(x => x.Col).ToList();
            for (int a = 1; a < shapeTemplates.Count; a++)
            {
                for (int b = 0; b < shapeTemplates[a].Count; b++)
                {
                    if (shapeTemplates[a][b].Row < shapeTemplates[maxShape][b].Row)
                        break;
                    if (shapeTemplates[a][b].Row > shapeTemplates[maxShape][b].Row)
                    {
                        maxShape = a;
                        break;
                    }
                    if (shapeTemplates[a][b].Col < shapeTemplates[maxShape][b].Col)
                        break;
                    if (shapeTemplates[a][b].Col > shapeTemplates[maxShape][b].Col)
                    {
                        maxShape = a;
                        break;
                    }
                }
            }
            foreach (Coordinates coordinates in shapeTemplates[maxShape])
            {
                finalShapeTemplate.Add(coordinates.Row);
                finalShapeTemplate.Add(coordinates.Col);
            }
            return finalShapeTemplate;
        }

        private void Normalize()
        {
            Move(-Area.Min(x => x.Row), -Area.Min(x => x.Col));
        }

        private void TurnRight()
        {
            FlipRowCol();
            FlipUpsideDown();
        }

        private void Move(int row, int col)
        {
            foreach (Coordinates coordinates in Area)
            {
                coordinates.Row += row;
                coordinates.Col += col;
            }
        }

        private void FlipRowCol()
        {
            int ij;
            foreach (Coordinates coordinates in Area)
            {
                ij = coordinates.Row;
                coordinates.Row = coordinates.Col;
                coordinates.Col = ij;
            }
        }

        private void FlipUpsideDown()
        {
            int maxRow = Area.Max(x => x.Row);
            foreach (Coordinates coordinates in Area)
                coordinates.Row = maxRow - coordinates.Row;
        }
    }
}