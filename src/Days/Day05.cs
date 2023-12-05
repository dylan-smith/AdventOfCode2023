namespace AdventOfCode.Days;

[Day(2023, 5)]
public class Day05 : BaseDay
{
    public override string PartOne(string input)
    {
        var paragraphs = input.Paragraphs().ToList();

        var seeds = ParseSeeds(paragraphs[0]);
        var seedToSoilMap = ParseMap(paragraphs[1]);
        var soilToFertilizerMap = ParseMap(paragraphs[2]);
        var fertilizerToWaterMap = ParseMap(paragraphs[3]);
        var waterToLightMap = ParseMap(paragraphs[4]);
        var lightToTemperatureMap = ParseMap(paragraphs[5]);
        var temperatureToHumidityMap = ParseMap(paragraphs[6]);
        var humidityToLocationMap = ParseMap(paragraphs[7]);

        var locations = seeds.Select(seed => seed.ApplyMap(seedToSoilMap)
                                                 .ApplyMap(soilToFertilizerMap)
                                                 .ApplyMap(fertilizerToWaterMap)
                                                 .ApplyMap(waterToLightMap)
                                                 .ApplyMap(lightToTemperatureMap)
                                                 .ApplyMap(temperatureToHumidityMap)
                                                 .ApplyMap(humidityToLocationMap));

        return locations.Min().ToString();
    }

    private List<(long dest, long source, long length)> ParseMap(string input)
    {
        var lines = input.Lines().Skip(1);
        var map = new List<(long dest, long source, long length)>();

        foreach (var line in lines)
        {
            var parts = line.Longs().ToList();
            map.Add((parts[0], parts[1], parts[2]));
        }

        return map;
    }

    private List<long> ParseSeeds(string input)
    {
        return input.Split(":")[1].Longs().ToList();
    }


    public override string PartTwo(string input)
    {
        return string.Empty;
    }
}

public static class Day05Extensions
{
    public static long ApplyMap(this long seed, List<(long dest, long source, long length)> map)
    {
        foreach (var range in map)
        {
            if (seed >= range.source && seed < range.source + range.length)
            {
                return seed + (range.dest - range.source);
            }
        }

        return seed;
    }
}