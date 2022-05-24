using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emulator : MonoBehaviour, IExecutable
{
    [SerializeField] private GameObject _instructionsListPanel;
    [SerializeField] private GameObject _codeInputPanel;

    private Instruction _currInstruction;
    private ProcState _currState;

    public ProcState CurrState { get => _currState; set => _currState = value; }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ExecuteInstruction(Instruction instruction)
    {
  
    }

    public void EmulateInstruction()
    {

    }

    public void LoadInstructions()
    {

    }

    public void StartEmulation()
    {
        _instructionsListPanel.SetActive(true);
        _codeInputPanel.SetActive(false);

        LoadInstructions();
    }

    public void NextStep()
    {

    }

    public void PrevStep()
    {

    }

    public void Stop()
    {

    }
}
