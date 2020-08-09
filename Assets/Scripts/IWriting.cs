using System.Collections.Generic;

public interface IWriting
{
    SortedDictionary<int, IPen> GetDistribution();
    void SetAmountWritten(float amount);
    void ChangePen(IPen pen);
}