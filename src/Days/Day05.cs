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

    private List<(long start, long length)> ParseSeeds2(string input)
    {
        var numbers = input.Split(":")[1].Longs().ToList();

        var result = new List<(long start, long length)>();

        for (var i = 0; i < numbers.Count; i += 2)
        {
            result.Add((numbers[i], numbers[i + 1]));
        }

        return result;
    }

    private List<(long start, long length)> _seeds;
    private List<(long dest, long source, long length)> _seedToSoilMap;
    private List<(long dest, long source, long length)> _soilToFertilizerMap;
    private List<(long dest, long source, long length)> _fertilizerToWaterMap;
    private List<(long dest, long source, long length)> _waterToLightMap;
    private List<(long dest, long source, long length)> _lightToTemperatureMap;
    private List<(long dest, long source, long length)> _temperatureToHumidityMap;
    private List<(long dest, long source, long length)> _humidityToLocationMap;

    public override string PartTwo(string input)
    {
        var paragraphs = input.Paragraphs().ToList();

        _seeds = ParseSeeds2(paragraphs[0]);
        _seedToSoilMap = ParseMap(paragraphs[1]);
        _soilToFertilizerMap = ParseMap(paragraphs[2]);
        _fertilizerToWaterMap = ParseMap(paragraphs[3]);
        _waterToLightMap = ParseMap(paragraphs[4]);
        _lightToTemperatureMap = ParseMap(paragraphs[5]);
        _temperatureToHumidityMap = ParseMap(paragraphs[6]);
        _humidityToLocationMap = ParseMap(paragraphs[7]);

        var location = 0L;

        while (!LocationInSeeds(location))
        {
            if (location % 10000000 == 0)
            {
                Log(location.ToString());
            }
            location++;
        }

        return location.ToString();
    }

    private bool LocationInSeeds(long location)
    {
        var seed = location.ApplyMapReverse(_humidityToLocationMap)
                           .ApplyMapReverse(_temperatureToHumidityMap)
                           .ApplyMapReverse(_lightToTemperatureMap)
                           .ApplyMapReverse(_waterToLightMap)
                           .ApplyMapReverse(_fertilizerToWaterMap)
                           .ApplyMapReverse(_soilToFertilizerMap)
                           .ApplyMapReverse(_seedToSoilMap);

        foreach (var range in _seeds)
        {
            if (seed >= range.start && seed < range.start + range.length)
            {
                return true;
            }
        }

        return false;
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

    public static long ApplyMapReverse(this long target, List<(long dest, long source, long length)> map)
    {
        foreach (var range in map)
        {
            if (target >= range.dest && target < range.dest + range.length)
            {
                return target + (range.source - range.dest);
            }
        }

        return target;
    }
}