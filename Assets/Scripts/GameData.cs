using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/GameData", order = 1)]
public class GameData : ScriptableObject {
    //Attack
    public string bindingJson;
    public SymbolSkills blast_class;
    public SymbolSkills discharge_class;
    public Skills resonance_class;
    public Triple_Impact tripleimpact_class;
    public ComboAssult comboAssult_default_class;
    public ComboAssult comboassult_discharge_class;
    public ComboAssult comboassult_blast_class;
    public ComboAssult comboassult_transition_class;
    public Transition transition;
    public Astra astra_default_class;
    public Astra astra_discharge_class;
    public Astra astra_blast_class;
    public Astra astra_transition_class;

    //Stat
    public float hp;
    public float mp;
    public int deathcount;
    public int time;
    public bool isGuidance;

    //Gage
    public float Relicgage;
    public float rizegage;

    public List<BindingInfo> bindings; // 바인딩 정보를 저장할 리스트

    [System.Serializable]
    public class BindingInfo
    {
        public string actionName; // 액션 이름
        public string bindingPath; // 바인딩된 키 경로
        public string controlName; // 바인딩 이름 (선택 사항)
    }
}
