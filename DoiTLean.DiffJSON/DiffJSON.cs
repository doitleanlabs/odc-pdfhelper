using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;
using JsonDiffPatchDotNet;
using System.Text;
using DoiTLean.DiffJSON.Structures;

namespace DoiTLean.DiffJSON {
    /// <summary>
    ///  The DiffJSON interface defines the methods for parsing two json and returns a list of JSONPairs with the previous and new values for each difference found.
    /// </summary>
    public class DiffJSON : IDiffJSON {


        /// <summary>
        ///  Parses Left and Right JSON and returns a list of JSONPairs with the previous and new values for each difference found
        /// </summary>
        public List<JSONPair> Diff(string leftJSON, string rightJSON)
        {
            List<JSONPair> _resultList = new List<JSONPair>();
            var jdp = new JsonDiffPatch();

            var left = JToken.Parse(leftJSON);
            var right = JToken.Parse(rightJSON);

            //LEFT
            JToken DiffLeft = jdp.Diff(right, left);


            foreach (JProperty x in left)
            {
                var key = ((JProperty)(x)).Name;
                string jvalue = ((JProperty)(x)).Value.ToString();
            }

            foreach (JProperty child in DiffLeft)
            {
                JSONPair _result = new JSONPair(child.Name, GetValueFromJTOKEN(left, child.Name), GetValueFromJTOKEN(right, child.Name));
                _resultList.Add(_result);
            }

            return _resultList;
        }



        /// <summary>
        /// Extract value from JTOKEN
        /// </summary>
        private string GetValueFromJTOKEN(JToken Jtokenobj, string key)
        {

            foreach (JProperty x in Jtokenobj)
            {
                if (((JProperty)(x)).Name == key)
                {
                    return ((JProperty)(x)).Value.ToString();
                }
            }
            return "";

        }

    }
}
