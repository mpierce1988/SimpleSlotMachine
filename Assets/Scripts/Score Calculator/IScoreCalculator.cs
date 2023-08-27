using System.Collections.Generic;

public interface ICalculateScore
{
    int CalculatePrize(List<Row> rowsToCalculate);
}