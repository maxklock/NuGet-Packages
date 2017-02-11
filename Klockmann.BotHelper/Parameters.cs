namespace Klockmann.BotHelper
{
    using System;
    using System.Linq;

    using Klockmann.BotHelper.Types;

    public class Parameters
    {
        #region constructors and destructors

        public Parameters() : this("parameters", null)
        {
            
        }

        public Parameters(string id, Action<CommandCallback> callback, params ParameterType[] types)
        {
            Id = id;
            Types = types;
            Callback = callback;
        }

        #endregion

        #region methods

        public bool TryConvert(string[] parameters, out object[] values)
        {
            if (parameters.Length != Types.Length)
            {
                values = null;
                return false;
            }

            values = parameters.Select<string, object>(
                    (v, index) =>
                    {
                        switch (Types[index])
                        {
                            case ParameterType.Any:
                            case ParameterType.String:
                                return v;
                            case ParameterType.Boolean:
                                bool b;
                                if (bool.TryParse(v, out b)) return b;
                                return null;
                            case ParameterType.Integer:
                                int i;
                                if (int.TryParse(v, out i)) return i;
                                return null;
                            case ParameterType.Double:
                                double d;
                                if (double.TryParse(v, out d)) return d;
                                return null;
                            case ParameterType.Float:
                                float f;
                                if (float.TryParse(v, out f)) return f;
                                return null;
                            case ParameterType.DateTime:
                                return Convert.ToDateTime(v);
                            default:
                                return v;
                        }
                    }).ToArray();
            return values.All(v => v != null);
        }

        #endregion

        #region properties

        public Action<CommandCallback> Callback { get; }

        public string Id { get; set; }

        public ParameterType[] Types { get; }

        #endregion
    }
}