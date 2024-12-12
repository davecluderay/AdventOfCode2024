namespace Aoc2024_Day09;

internal static class DiskMap
{
    public static List<DiskMapSegment> Read()
    {
        var mapText = InputFile.ReadAllText();
        List<DiskMapSegment> segments = new(mapText.Length);
        for (var i = 0; i < mapText.Length; i++)
        {
            if (mapText[i] < '0' || mapText[i] > '9') continue;

            int length = mapText[i] - '0';
            int? fileId = (i % 2 == 0) ? i / 2 : null;
            segments.Add(new DiskMapSegment(length, fileId));
        }
        return segments;
    }

    public static long CalculateChecksum(this List<DiskMapSegment> segments)
    {
        var physicalIndex = 0;
        var checksum = 0L;
        foreach (var segment in segments)
        {
            for (var i = 0; i < segment.Length; i++)
            {
                checksum += (segment.FileId ?? 0) * physicalIndex++;
            }
        }
        return checksum;
    }
}

internal readonly record struct DiskMapSegment(int Length, int? FileId);
