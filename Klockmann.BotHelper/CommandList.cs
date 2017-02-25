namespace Klockmann.BotHelper
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Klockmann.BotHelper.Extensions;
    using Klockmann.BotHelper.Types;

    using Microsoft.Bot.Connector;

    public class CommandList : IEnumerable
    {
        #region member vars

        private readonly IDictionary<string, ICollection<Parameters>> _data = new Dictionary<string, ICollection<Parameters>>();

        #endregion

        #region explicit interfaces

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_data).GetEnumerator();
        }

        #endregion

        #region methods

        public void Add(string command, Parameters parameters)
        {
            if (_data.ContainsKey(command))
            {
                _data[command].Add(parameters);
                return;
            }

            _data.Add(
                command,
                new List<Parameters>()
                {
                    parameters
                });
        }

        public Task<bool> InvokeAsync(Activity activity)
        {
            return Task.Run(
                () =>
                {
                    var com = activity.GetCommand();
                    if (com == null || !ContainsCommand(com))
                    {
                        return false;
                    }

                    foreach (var parameterList in this[com])
                    {
                        object[] values;
                        if (parameterList.Types.Length == 1 && parameterList.Types[0] == ParameterType.Any)
                        {
                            values = activity.GetParameters().Cast<object>().ToArray();
                        }
                        else if (!parameterList.TryConvert(activity.GetParameters(), out values))
                        {
                            continue;
                        }

                        parameterList.Callback?.Invoke(
                            new CommandCallback
                            {
                                Command = com,
                                Activity = activity,
                                ParameterListId = parameterList.Id,
                                Parameters = values
                            });
                        return true;
                    }

                    return false;
                });
        }

        public void Clear()
        {
            _data.Clear();
        }

        public bool ContainsCommand(string command)
        {
            return _data.ContainsKey(command);
        }

        public bool Remove(string command)
        {
            return _data.Remove(command);
        }

        #endregion

        #region properties

        public int Count => _data.Count;

        public IEnumerable<Parameters> this[string command] => _data[command];

        public IEnumerable<string> Keys => _data.Keys;

        public IEnumerable<IEnumerable<Parameters>> Values => _data.Values;

        #endregion
    }
}