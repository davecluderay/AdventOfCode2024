namespace Aoc2024_Day09;

internal static class DiskCompacter
{
    public static void CompactWithFragmentation(List<DiskMapSegment> diskMap)
    {
        var nextSpaceIndex = 1;
        for (var fileIndex = diskMap.Count - 1; fileIndex > nextSpaceIndex; fileIndex--)
        {
            var file = diskMap[fileIndex];
            if (file.FileId is null) continue;

            var blocksRemaining = file.Length;
            while (blocksRemaining > 0)
            {
                // Look for the first segment with space of any length.
                while (nextSpaceIndex < fileIndex)
                {
                    var space = diskMap[nextSpaceIndex];
                    if (space.FileId is null && space.Length > 0)
                    {
                        break;
                    }
                    nextSpaceIndex++;
                }

                // If no space was found, no more blocks can be moved.
                if (nextSpaceIndex >= fileIndex) break;

                var spaceLength = diskMap[nextSpaceIndex].Length;
                var moveLength = Math.Min(blocksRemaining, spaceLength);

                diskMap[nextSpaceIndex] = file with { Length = moveLength };

                if (moveLength < spaceLength)
                {
                    // The file was moved, with some space still unfilled.
                    blocksRemaining = 0;
                    diskMap.RemoveAt(fileIndex);
                    diskMap.Insert(++nextSpaceIndex, new DiskMapSegment(spaceLength - moveLength, null));
                    fileIndex++;
                }
                else if (moveLength < blocksRemaining)
                {
                    // The file was partially moved.
                    blocksRemaining -= moveLength;
                    diskMap[fileIndex] = file with { Length = blocksRemaining };
                }
                else
                {
                    // The file was completely, with exactly enough space.
                    blocksRemaining = 0;
                    diskMap.RemoveAt(fileIndex);
                }
            }
        }
    }

    public static void CompactWithoutFragmentation(List<DiskMapSegment> diskMap)
    {
        for (var fileIndex = diskMap.Count - 1; fileIndex > 1; fileIndex--)
        {
            var file = diskMap[fileIndex];
            if (file.FileId is null) continue;

            // Look for a space to move the file into.
            for (var spaceIndex = 1; spaceIndex < fileIndex; spaceIndex++)
            {
                var space = diskMap[spaceIndex];
                if (space.FileId is not null) continue;
                if (space.Length < file.Length) continue;

                // A segment with sufficient space was found, so move the file.
                var excess = space.Length - file.Length;
                diskMap[spaceIndex] = file;
                diskMap[fileIndex] = file with { FileId = null};
                if (excess > 0)
                {
                    diskMap.Insert(spaceIndex + 1, new DiskMapSegment(excess, null));
                    fileIndex++;
                }
                break;
            }
        }
    }
}
