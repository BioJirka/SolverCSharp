namespace Solver.Sudoku.Models
{
    public class SudokuInput
    {
        public int RowCnt;
        public int ColCnt;
        public int NumRange;
        public List<string> Types = new();
        public List<Coordinates> InputNumbers = new();
        public List<List<Coordinates>> AreaFields = new();
        public List<List<Coordinates>> AntiAreaFields = new();
        public List<List<List<Coordinates>>> CloneFields = new();
        public List<List<Coordinates>> ShapeFields = new();
        public List<Coordinates> EvenOddFields = new();
        public List<List<Coordinates>> EvenOddAreaFields = new();
        public List<Coordinates> LowMidHighFields = new();
        public List<Coordinates> FortressFields = new();
        public List<Coordinates> ConsecutiveEdgeFields = new();
        public bool AreAllConsecutiveEdgesMarked;
        public List<Coordinates> RomanFields = new();
        public bool AreAllRomansMarked;
        public List<Coordinates> DifferenceFields = new();
        public List<List<Coordinates>> MinDifferenceFields = new();
        public List<Coordinates> SumEdgeFields = new();
        public List<Coordinates> InequalityFields = new();
        public List<List<Coordinates>> ThermometerFields = new();
        public List<Coordinates> WhereIsXFields = new();
        public List<Coordinates> DynamicSumOrProductFields = new();
        public List<List<Coordinates>> KillerFields = new();
        public List<List<Coordinates>> SumWeightedFields = new();
        public List<Coordinates> SumCornerFields = new();
        public List<Coordinates> EvenOddDotFields = new();
        public bool AreAllEvenOddDotsMarked;
        public List<Coordinates> SumDotFields = new();
        public List<List<Coordinates>> SumArrowFields = new();
        public bool AreAllSumDotsMarked;
        public List<Coordinates> ConsecutiveDotFields = new();
        public bool AreAllConsecutiveDotsMarked;
        public List<Coordinates> ClockFields = new();
        public bool AreAllClocksMarked;
        public List<List<Coordinates>> ChildSumFields = new();
        public List<Coordinates> MagicSquareFields = new();
        public List<Coordinates> ProductResultFields = new();

        public List<string> SudokuInputCheck()
        {
            int index;
            List<string> messages = new();
            if (RowCnt < 1)
                messages.Add("Error, number of rows must be positive number.");
            if (ColCnt < 1)
                messages.Add("Error, number of columns must be positive number.");
            if (NumRange < 1)
                messages.Add("Error, number range must be positive number.");
            
            CheckRowCol(messages, "Numbers", InputNumbers);
            CheckNum(messages, "Numbers", InputNumbers, 1, NumRange);

            foreach (List<Coordinates> area in AreaFields)
                CheckRowCol(messages, "Area", area);

            foreach (List<Coordinates> area in AntiAreaFields)
            {
                CheckRowCol(messages, "Anti area", area);
                CheckNum(messages, "Anti area", area.GetRange(0, 1), 1, null);
            }

            foreach (List<List<Coordinates>> areas in CloneFields)
            {
                foreach (List<Coordinates> area in areas)
                    CheckRowCol(messages, "Clone", area);
                CheckEqualLength(messages, "Clone", areas);
            }

            foreach (List<Coordinates> area in ShapeFields)
                CheckRowCol(messages, "Shape", area);

            CheckRowCol(messages, "Even odd", EvenOddFields);
            CheckText(messages, "Even odd", EvenOddFields, new List<string> { "Even", "Odd" });

            foreach (List<Coordinates> area in EvenOddAreaFields)
                CheckRowCol(messages, "EvenOddArea", area);

            CheckRowCol(messages, "Low mid high", LowMidHighFields);
            CheckText(messages, "Low mid high", LowMidHighFields, new List<string> { "Low", "Mid", "High" });

            CheckRowCol(messages, "Fortress", FortressFields);

            CheckRowCol(messages, "Consecutive edge", ConsecutiveEdgeFields);
            CheckRowColDecrease(messages, "Consecutive edge", ConsecutiveEdgeFields, "S", "E", false, false);
            CheckDirection(messages, "Consecutive edge", ConsecutiveEdgeFields, new List<string> { "S", "E" });
            CheckText(messages, "Consecutive edge", ConsecutiveEdgeFields, new List<string> { "X", "O" });

            CheckRowCol(messages, "Roman", RomanFields);
            CheckRowColDecrease(messages, "Roman", RomanFields, "S", "E", false, false);
            CheckDirection(messages, "Roman", RomanFields, new List<string> { "S", "E" });
            CheckText(messages, "Roman", RomanFields, new List<string> { "X", "V", "O" });

            CheckRowCol(messages, "Difference", DifferenceFields);
            CheckRowColDecrease(messages, "Difference", DifferenceFields, "S", "E", false, false);
            CheckNum(messages, "Difference", DifferenceFields, 0, null);
            CheckDirection(messages, "Difference", DifferenceFields, new List<string> { "S", "E" });

            foreach (List<Coordinates> area in MinDifferenceFields)
            {
                CheckRowCol(messages, "Min difference", area);
                CheckNum(messages, "Min difference", area.GetRange(0, 1), 1, null);
            }

            CheckRowCol(messages, "Sum edge", SumEdgeFields);
            CheckRowColDecrease(messages, "Sum edge", SumEdgeFields, "S", "E", false, false);
            CheckNum(messages, "Sum edge", SumEdgeFields, 1, null);
            CheckDirection(messages, "Sum edge", SumEdgeFields, new List<string> { "S", "E" });

            CheckRowCol(messages, "Inequality", InequalityFields);
            CheckRowColDecrease(messages, "Inequality", InequalityFields, "S", "E", false, false);
            CheckDirection(messages, "Inequality", InequalityFields, new List<string> { "S", "E" });
            CheckText(messages, "Inequality", InequalityFields, new List<string> { "Higher", "Lower" });

            foreach (List<Coordinates> area in ThermometerFields)
                CheckRowCol(messages, "Thermometer", area);

            CheckRowCol(messages, "Where is X", WhereIsXFields);
            CheckNum(messages, "Where is X", WhereIsXFields, 1, NumRange);
            CheckDirection(messages, "Where is X", WhereIsXFields, new List<string> { "N", "W", "S", "E", "NW", "NE", "SW", "SE" });

            CheckRowCol(messages, "Dynamic sum or product", DynamicSumOrProductFields);
            CheckNum(messages, "Dynamic sum or product", DynamicSumOrProductFields, 1, null);
            CheckDirection(messages, "Dynamic sum or product", DynamicSumOrProductFields, new List<string> { "N", "W", "S", "E" });

            foreach (List<Coordinates> area in KillerFields)
                CheckRowCol(messages, "Killer", area);
            foreach (List<Coordinates> area in KillerFields)
                CheckNum(messages, "Killer", area.GetRange(0, 1), 1, null);

            foreach (List<Coordinates> area in SumWeightedFields)
                CheckRowCol(messages, "Sum weighted", area.GetRange(1, area.Count - 1));
            foreach (List<Coordinates> area in SumWeightedFields)
                CheckNum(messages, "Sum weighted", area, 1, area[0].Number);

            CheckRowCol(messages, "Sum corner", SumCornerFields);
            CheckRowColDecrease(messages, "Sum corner", SumCornerFields, string.Empty, string.Empty, true, true);
            CheckNum(messages, "Sum corner", SumCornerFields, 1, null);

            CheckRowCol(messages, "Even odd dot", EvenOddDotFields);
            CheckRowColDecrease(messages, "Even odd dot", EvenOddDotFields, string.Empty, string.Empty, true, true);
            CheckText(messages, "Even odd dot", EvenOddDotFields, new List<string> { "Black", "White", "None" });

            CheckRowCol(messages, "Sum dot", SumDotFields);
            CheckRowColDecrease(messages, "Sum dot", SumDotFields, string.Empty, string.Empty, true, true);
            CheckText(messages, "Sum dot", SumDotFields, new List<string> { "Black", "None" });

            foreach (List<Coordinates> area in SumArrowFields)
            {
                CheckRowCol(messages, "Sum arrow", area);
                CheckMinLength(messages, "Sum arrow", area, 2);
            }

            CheckRowCol(messages, "Consecutive dot", ConsecutiveDotFields);
            CheckRowColDecrease(messages, "Consecutive dot", ConsecutiveDotFields, string.Empty, string.Empty, true, true);
            CheckText(messages, "Consecutive dot", ConsecutiveDotFields, new List<string> { "Black", "White", "None" });

            CheckRowCol(messages, "Clock", ClockFields);
            CheckRowColDecrease(messages, "Clock", ClockFields, string.Empty, string.Empty, true, true);
            CheckText(messages, "Clock", ClockFields, new List<string> { "Black", "White", "None" });

            foreach (List<Coordinates> area in ChildSumFields)
            {
                index = area.TakeWhile(x => x.Text != "Coordinates").Count();
                if (index == 0 || index == 1)
                {
                    messages.Add("Error, no numbers or no coordinates in Child sum fields.");
                    continue;
                }
                CheckRowCol(messages, "Child sum", area.GetRange(index, area.Count - index));
                CheckNum(messages, "Child sum", area.GetRange(0, 1), 1, null);
                CheckNum(messages, "Child sum", area.GetRange(1, index - 1), 1, area[0].Number);
                CheckText(messages, "Child sum", area.GetRange(0, 1), new List<string> { "Max sum" });
                CheckText(messages, "Child sum", area.GetRange(1, index - 1), new List<string> { "Sum" });
                CheckText(messages, "Child sum", area.GetRange(index, area.Count - index), new List<string> { "Coordinates" });
            }

            CheckRowCol(messages, "Magic square", MagicSquareFields);
            CheckRowColDecrease(messages, "Magic square", MagicSquareFields, string.Empty, string.Empty, true, true, 2, 2);
            
            CheckRowCol(messages, "Product result", ProductResultFields);
            CheckRowColDecrease(messages, "Product result", ProductResultFields, string.Empty, string.Empty, true, true);
            
            return messages;
        }

        private void CheckRowCol(List<string> messages, string solverName, List<Coordinates> area)
        {
            foreach (Coordinates coordinates in area)
            {
                if (coordinates.Row < 1 || coordinates.Row > RowCnt)
                    messages.Add($"Error, row {coordinates.Row} in '{solverName}' fields is out of range.");
                if (coordinates.Col < 1 || coordinates.Col > ColCnt)
                    messages.Add($"Error, column {coordinates.Col} in '{solverName}' fields is out of range.");
            }
        }

        private void CheckRowColDecrease(List<string> messages, string solverName, List<Coordinates> area, string directionRowDecrease, string directionColDecrease, bool rowDecreaseAll, bool colDecreaseAll, int rowDecrease = 1, int colDecrease = 1)
        {
            foreach (Coordinates coordinates in area)
            {
                if (coordinates.Row <= RowCnt && coordinates.Row > RowCnt - rowDecrease && (coordinates.Direction == directionRowDecrease || rowDecreaseAll))
                    messages.Add($"Error, row {coordinates.Row} in '{solverName}' fields is out of range.");
                if (coordinates.Col <= ColCnt && coordinates.Col > ColCnt - colDecrease && (coordinates.Direction == directionColDecrease || colDecreaseAll))
                    messages.Add($"Error, column {coordinates.Col} in '{solverName}' fields is out of range.");
            }
        }

        private static void CheckNum(List<string> messages, string solverName, List<Coordinates> area, int? minNumber, int? maxNumber)
        {
            foreach (Coordinates coordinates in area)
            {
                if ((coordinates.Number < minNumber && minNumber != null) || (coordinates.Number > maxNumber && maxNumber != null))
                    messages.Add($"Error, number {coordinates.Number} in '{solverName}' fields is out of range.");
            }
        }

        private static void CheckDirection(List<string> messages, string solverName, List<Coordinates> area, List<string> possibleValues)
        {
            foreach (Coordinates coordinates in area)
            {
                if (!possibleValues.Contains(coordinates.Direction))
                    messages.Add($"Error, direction value '{coordinates.Direction}' in '{solverName}' fields is an invalid value.");
            }
        }

        private static void CheckText(List<string> messages, string solverName, List<Coordinates> area, List<string> possibleValues)
        {
            foreach (Coordinates coordinates in area)
            {
                if (!possibleValues.Contains(coordinates.Text))
                    messages.Add($"Error, text value '{coordinates.Text}' in '{solverName}' fields is an invalid value.");
            }
        }

        private static void CheckEqualLength(List<string> messages, string solverName, List<List<Coordinates>> areas)
        {
            foreach (List<Coordinates> area in areas)
            {
                if (area.Count != areas[0].Count)
                    messages.Add($"Error, lengths of areas in '{solverName}' are different.");
            }
        }

        private static void CheckMinLength(List<string> messages, string solverName, List<Coordinates> areas, int minLength)
        {
            if (areas.Count < minLength)
                messages.Add($"Error, length of area in '{solverName}' is lower than required.");
        }
    }
}
