namespace com.github.dergash.SM421LogParser
{
    public enum FeederRecordField
    {
        FeederName,
        FeederId,
        Name,                   // Название модели элементов, загруженных в питатель
        PickupAmount,           // Количество элементов, взятых из питателя
        PickupMissedAmount,     // Количество ошибок захвата элементов
        PlacedAmount,           // Количество элементов, размещенных на плате
        DumpedAmount,           // Количество элементов, сброшенных в коробку (?)
        Unknown1,               // ??? Дублирует logCPickMiss 
        PartNG                  // ???
    }
}
