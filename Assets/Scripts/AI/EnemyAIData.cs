using SerializeReferenceEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/AI")]
public class EnemyAIData : ScriptableObject
{
    [field: SerializeReference, SR] 
    public List<EnemyIntend> IntendTable { get; private set; }
}
