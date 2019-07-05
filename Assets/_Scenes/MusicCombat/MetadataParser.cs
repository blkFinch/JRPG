using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MetadataParser{

    private List<string> loopData;
    public List<string> LoopData
    {
        get
        {
            return loopData;
        }
    }

    public MetadataParser(string filePath){
        var mdFile = File.ReadAllLines(filePath);
        loopData = StripArrayToList(mdFile);
    }

    private List<string> StripArrayToList(string[] _array)
    {
        List<string> _listToReturn = new List<string>();

        for (int i = 0; i < _array.Length; i++)
        {

            //only adds string if it does not contain comma
            if (_array[i] != ",")
            {
                _listToReturn.Add(_array[i]);
            }
        }

        return _listToReturn;
    }
}