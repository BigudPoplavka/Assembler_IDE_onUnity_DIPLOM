using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class EngineCoreDataTransfer: MonoBehaviour
{
    public EngineCoreDataTransfer()
    {

    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public List<string> GetCodeList(string code)
    {
        Debug.Log("CORE_DATA_TRANSFER: Code to list convert");
        List<string> codeList = new List<string>();

        if(code != null || code != string.Empty)
        {
            foreach (string line in code.Split(new char[] { '\n' }))
            {
                if(line != "" && line != "\n")
                    codeList.Add(line);
            }
            Debug.Log($"CORE_DATA_TRANSFER: List length: {codeList.Count}");
            return codeList;
        }
        else
        {
            EventsBase.onEmptyCode?.Invoke();
            Debug.Log("CORE_DATA_TRANSFER: Empty code");
            return null;
        }
    }
}
