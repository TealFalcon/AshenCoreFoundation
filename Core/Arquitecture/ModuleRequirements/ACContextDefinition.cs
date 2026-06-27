using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace AshenCore.Core
{

    [CreateAssetMenu(fileName = "ACContextDefinition", menuName = "AshenCore/Input/Context Definition")]
    public class ACContextDefinition : ScriptableObject
    {
        public int id;
        public string contextName;
        public List<ACInputProfile> profiles = new List<ACInputProfile>();

    }


}