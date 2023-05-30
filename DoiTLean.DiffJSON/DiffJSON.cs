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
using Newtonsoft.Json;
using System.IO;

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

        public string JSON_Listify(string JSONIn, string Path)
        {
            string JSONOut = "";

            string[] path = Path.Trim().Split('.');
            JToken root = Inner_Listify(JToken.Parse(JSONIn), path, 0);

            StringBuilder sb = new StringBuilder();

            using (JsonWriter json = new JsonTextWriter(new StringWriter(sb)))
            {
                json.Formatting = Formatting.None;
                json.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                root.WriteTo(json);
            }

            JSONOut = sb.ToString();
            return JSONOut;
        } // MssJSON_Listify

        private JToken Inner_Listify(JToken root, string[] path, int index)
        {

            if (root.Type == JTokenType.Array)
            {
                // if we're at an array, simply apply to all elements
                JArray arr = (JArray)root;
                for (int i = 0; i < arr.Count; i++)
                {
                    arr[i] = Inner_Listify(arr[i], path, index);
                }
                return arr;
            }

            if (path.Length == index)
            {
                // nothing to do if we're not at an object
                if (root.Type != JTokenType.Object)
                    return root;

                // do the listification
                JObject obj = (JObject)root;
                JArray res = new JArray();
                foreach (JProperty p in obj.Properties())
                {
                    JObject tmp = new JObject();
                    tmp["key"] = p.Name;
                    tmp["value"] = p.Value;
                    res.Add(tmp);
                }
                return res;

            }
            else
            {
                if (path[index].Equals(""))
                    return Inner_Listify(root, path, index + 1);

                // path[index] != ""
                if (root.Type == JTokenType.Object)
                {
                    JObject obj = (JObject)root;
                    JToken r = obj[path[index]];

                    if (r == null || r.Type == JTokenType.Null)
                        return root;

                    obj[path[index]] = Inner_Listify(r, path, index + 1);
                    return root;
                }

                return root;
            }
        }

    }
}
