using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media.TextFormatting;

namespace SpellChecker
{
    enum prevEdit { none, insert, delete };
    class Spelling
    {        
        private Dictionary<String, int> _dictionary = new Dictionary<string, int>();
        private static Regex _wordRegex = new Regex("[a-z]+", RegexOptions.Compiled);

        public Spelling(string inputDictionary )
        {
            InitDictionary(inputDictionary);            
        }

        public string Start(string inputText)
        {
            return CorrectText(inputText);           
        }
        private string CorrectWord(string word)
        {
            if (string.IsNullOrEmpty(word))
                return word;
            word = word.ToLower();
            if (_dictionary.ContainsKey(word))
                return word;
            //search depth 1
            List<Tuple<string, prevEdit, int>> listFromEdits = WordVariations(word);
            string result = WordCorrectionResults(listFromEdits);
            if (result.Any()) return result;
            //search depth 2
            listFromEdits = MultipleWordsVariations(listFromEdits);
            result = WordCorrectionResults(listFromEdits);
            if (result.Any()) return result;
            //word not found
            return "{" + word + "?}";
                
        }

        private List<Tuple<string, prevEdit, int>> WordVariations(string word, prevEdit prevE = 0, int editPos = -1)
        {
            List<Tuple<string, string>> splits = new List<Tuple<string, string>>();
            List<Tuple<string, prevEdit, int>> edits = new List<Tuple<string, prevEdit, int>>();
            for(int i = 0; i < word.Length; i++)
            {
                var tuple = new Tuple<string, string>(word.Substring(0, i), word.Substring(i));
                splits.Add(tuple);
            }
            //insert
            for(int i = 0; i < splits.Count; i++)
            {
                if (prevE == (prevEdit)1 && (i == editPos || i == editPos + 1))//adjacent char check
                    break;
                string a = splits[i].Item1;
                string b = splits[i].Item2;
                for(char c = 'a'; c <='z'; c++)
                {
                    var tuple = new Tuple<string, prevEdit, int>(a+c+b,(prevEdit)1, a.Length);
                    edits.Add(tuple);
                }  
            }

            //delete
            for(int i = 0; i < splits.Count; i++)
            {
                if (prevE == (prevEdit)2 && (i == editPos || i == editPos - 1))//adjacent char check
                    break;
                string a = splits[i].Item1;
                string b = splits[i].Item2;
                if (!string.IsNullOrEmpty(b))
                {
                    var tuple = new Tuple<string, prevEdit, int>(a + b.Substring(1), (prevEdit)2, a.Length);
                    edits.Add(tuple);
                }
            }
            return edits;

        }

        private List<Tuple<string, prevEdit, int>> MultipleWordsVariations(List<Tuple<string, prevEdit, int>> inputWordList)
        {
            List<Tuple<string, prevEdit, int>> edits = new List<Tuple<string, prevEdit, int>>();
            foreach(var word in inputWordList)
                edits.AddRange(WordVariations(word.Item1, word.Item2, word.Item3));
            return edits;

        }
        private void InitDictionary(string inputDictionary)
        {
            inputDictionary = inputDictionary.Replace(" ", "\n");
            List<string> dictList = inputDictionary.Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();
            _dictionary.Clear();
            for (int i = 0; i < dictList.Count; i++)
            {
                string trimmedWord = dictList[i].Trim().ToLower();
                if (_wordRegex.IsMatch(trimmedWord))
                {
                    if (!_dictionary.ContainsKey(trimmedWord))
                        _dictionary.Add(trimmedWord, i);
                }
            }
        }

        private string CorrectText(string inputText)
        {
            string output;
            List<string> wordList = inputText.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
            output = "";
            foreach (string word in wordList)
            {
                if (word.Contains('\n'))
                    output += CorrectWord(word.Substring(0, word.LastIndexOf('\n')))
                        + '\n' + CorrectWord(word.Substring(word.LastIndexOf('\n') + 1)) + " ";
                else
                    output += CorrectWord(word) + " ";
            }
            if(output.Contains(' ')) output = output.Remove(output.LastIndexOf(' '));
            return output;
        }
        
        private string WordCorrectionResults(List<Tuple<string, prevEdit, int>> listFromEdits)
        {
            Dictionary<string, int> candidates = new Dictionary<string, int>();

            for (int i = 0; i < listFromEdits.Count; i++)
            {
                if (_dictionary.ContainsKey(listFromEdits[i].Item1) && !candidates.ContainsKey(listFromEdits[i].Item1))
                    candidates.Add(listFromEdits[i].Item1, _dictionary[listFromEdits[i].Item1]);
            }
            if (candidates.Count == 1)
                return candidates.First().Key;
            if (candidates.Count > 1)
            {
                string result = "{";
                foreach (var candidate in candidates.OrderBy(x => x.Value))
                    result += candidate.Key + " ";
                result = result.Remove(result.Length - 1) + "}";
                return result;
            }
            return "";
        }
    }
}
