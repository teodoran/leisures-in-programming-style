using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

var atLeastTwoLetters = new Regex(@"\w{2,}");

var stopWords = File
    .ReadLines("../stop-words.txt")
    .SelectMany(line => atLeastTwoLetters.Matches(line))
    .Select(match => match.Value.ToLower())
    .ToHashSet();

var inputFile = args.Any() ? args.First() : "../input.txt";

var top25Terms = File
    .ReadLines(inputFile)
    .SelectMany(line => atLeastTwoLetters.Matches(line))
    .Select(match => match.Value.ToLower())
    .Where(word => !stopWords.Contains(word))
    .GroupBy(word => word)
    .OrderByDescending(group => group.Count())
    .Select(group => $"{group.Key} - {group.Count()}")
    .Take(25);

Console.WriteLine(string.Join(Environment.NewLine, top25Terms));