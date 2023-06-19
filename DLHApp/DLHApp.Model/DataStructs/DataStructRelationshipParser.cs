using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHApp.Model.DataStructs
{
    internal class DataStructRelationshipParser
    {
        public DataStructRelationshipParser(string structText, DataStructRelationship relationship)
        {
            StructText = structText;
            Relationship = relationship;
        }

        string StructText { get; set; }

        DataStructRelationship Relationship { get; set; }

        public void Parse()
        {
            string sourceParam = GetParameterValue(StructText, "Source", ",");
            string joinsParam = GetParameterValue(StructText, "Joins", "]") + "]";
            string outputParam = GetParameterValue(StructText, "Output", ")");
            string[] joins = joinsParam.Replace("[", "").Replace("]", "").Replace(", ", ",").Replace(" ,", ",").Split("),");

            Relationship.SourceDataStruct = sourceParam;
            Relationship.OutputField = outputParam;
            Relationship.Joins = new DataStructRelationshipJoinCollection();

            foreach(string joinParam in joins)
            {
                Relationship.Joins.Add(ParseJoin(joinParam));
            }
        }

        string GetParameterValue(string text, string paramName, string endValue)
        {
            string output = string.Empty;

            text = text.Replace(" =", "=").Replace("= ", "=");
            paramName += paramName.Trim().EndsWith("=") ? "" : "=";

            int startPos = text.IndexOf(paramName);
            int endPos = text.IndexOf(endValue, startPos);
            endPos = endPos == -1 ? text.LastIndexOf(")", startPos) : endPos;

            output = text.Substring(startPos, endPos - startPos).Replace(paramName, "");

            return output;
        }

        DataStructRelationshipJoin ParseJoin(string joinText)
        {
            DataStructRelationshipJoin output = new DataStructRelationshipJoin();

            try
            {
                string[] joinParams = joinText.Replace("StructJoin", "").Replace("(", "").Replace(")", "").Split(",");
                output.SourceField = joinParams.Length >= 1 ? joinParams[0].Trim() : null;
                output.TargetField = joinParams.Length >= 2 ? joinParams[1].Trim() : null;
                output.IsCaseSensitive = joinParams.Length >= 3 ? Convert.ToBoolean(joinParams[2].Trim().ToLower()) : false;
            }
            catch { }

            return output;
        }
    }
}
